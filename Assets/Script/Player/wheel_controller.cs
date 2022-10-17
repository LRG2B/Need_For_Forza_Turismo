using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wheel_controller : MonoBehaviour
{
    //D�finition de la vitesse

    private float speed; //Vitesse utilise pour avancer

    public float MaxSpeed;
    private float MinSpeed;
    private float acc = 0.4f;
    public AnimationCurve speedAcc;
    private float frein = 0.4f;
    public AnimationCurve speedMin;

    public Text Odo;
    private float odometer = 0;
    private int bonus = 0;
    bool is_infinite;

    [SerializeField]
    private float turnSpeed; //Vitesse de la voiture quand elle tourne

    //D�claration des input du clavier
    private float horizontalInput;
    private float forwardInput;

    //D�claration des roues
    GameObject FLwheel; //Passager avant
    GameObject RLwheel; //Passager arriere
    GameObject FRwheel; //Conducteur Avant
    GameObject RRwheel; //Conducteur arriere
    GameObject SW; //Volant

    public AudioSource horn;

    private GameManager gameManager;

    public static wheel_controller instance;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        speed = MinSpeed; //On r�cup�re la bonne vitesse
        FLwheel = GameObject.Find("WHEEL_LF_1");
        RLwheel = GameObject.Find("WHEEL_LR_1");
        FRwheel = GameObject.Find("WHEEL_RF_1");
        RRwheel = GameObject.Find("WHEEL_RR_1");
        SW = GameObject.Find("STEER_HR");
        gameManager = GameManager.instance;
        MinSpeed = gameManager.get_min_speed();
        speed = MinSpeed; //On r�cup�re la bonne vitesse
        if (SceneManager.GetActiveScene().name == "Infinite")
            is_infinite = true;
    }

    void Update()
    {
        MinSpeed = gameManager.get_max_speed();

        horizontalInput = Input.GetAxis("Horizontal"); //d�claration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // d�claration de l'input vertical
        SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed * 10 * -horizontalInput);
        if (horizontalInput == 0)
        {
            if (SW.transform.rotation.z >= 0.1f)
            {
                SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -(turnSpeed + 5) * 10);
            }
            else if (SW.transform.rotation.z <= -0.1f)
            {
                SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * (turnSpeed + 5) * 10);
            }
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(horn);
        }
               
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed); //D�placement du v�hicule en fonction du temps. Note: le v�hicule avance tout le temps, il ne peut pas reculer
        FLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        FRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        
        odometer += transform.position.z/2 - odometer;
        Odo.text = (odometer + bonus ).ToString("F2");

        //Si le v�hicule roule, il peut tourn�, sinon, il ne peut pas
        if (speed > 0)
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //on fait tourn� le v�hicule quand on donne un input horizontal
        }


        if (forwardInput > 0)
        {
            if (speed < MaxSpeed)
            {
                speed += acc * speedAcc.Evaluate(forwardInput);
            }
        }
        if (forwardInput < 0)
        {
            if (speed > MinSpeed)
            {
                speed -= frein * speedMin.Evaluate(-forwardInput);
            }
            if (speed < MinSpeed)
            {
                speed = 0;
            }
        }
        if (forwardInput == 0)
        {
            if (speed > MinSpeed)
            {
                speed -= 0.2f;
            }
            if (speed < MinSpeed)
            {
                speed = MinSpeed;
            }
        }
        if (is_infinite)
            MaxSpeed += 0.5f;
    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route, il perd
        if ((transform.position.y < -10 || transform.position.y > 5) && SceneManager.GetActiveScene().name == "Infinite")
        {
            gameManager.km = odometer+bonus;
            SceneManager.LoadScene("LooseInfinite"); //Chargement de la sc�ne Loose
        }


        else if ((transform.position.y < -10 || transform.position.y > 5) && SceneManager.GetActiveScene().name == "Chrono")
        {
            gameManager.km = odometer;
            SceneManager.LoadScene("LooseChrono"); //Chargement de la sc�ne Loose
        }


    }
    public void Add_Km(int value)
    {
        bonus += value;
    }

}
