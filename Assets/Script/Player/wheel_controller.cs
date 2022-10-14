using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wheel_controller : MonoBehaviour
{
    //Définition de la vitesse

    private float speed; //Vitesse utilisé pour avancer

    public float MaxSpeed = 40f;
    private float MinSpeed = 5f;
    private float acc = 0.4f;
    public AnimationCurve speedAcc;
    private float frein = 0.4f;
    public AnimationCurve speedMin;

    public Text Odo;
    private float odometer = 0;

    [SerializeField]
    private float turnSpeed; //Vitesse de la voiture quand elle tourne

    //Déclaration des input du clavier
    private float horizontalInput;
    private float forwardInput;

    //Déclaration des roues
    GameObject FLwheel; //Passager avant
    GameObject RLwheel; //Passager arriere
    GameObject FRwheel; //Conducteur Avant
    GameObject RRwheel; //Conducteur arriere
    GameObject SW; //Volant

    public AudioSource horn;

    private GameManager gameManager;

    private void Start()
    {
        speed = MinSpeed; //On récupère la bonne vitesse
        FLwheel = GameObject.Find("WHEEL_LF_1");
        RLwheel = GameObject.Find("WHEEL_LR_1");
        FRwheel = GameObject.Find("WHEEL_RF_1");
        RRwheel = GameObject.Find("WHEEL_RR_1");
        SW = GameObject.Find("STEER_HR");
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //déclaration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // déclaration de l'input vertical
        SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed * 10 * -horizontalInput);

        if(horizontalInput == 0)
        {
            if (SW.transform.rotation.z >= 0.5)
            { 
                SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -turnSpeed * 10);
            }
            else if(SW.transform.rotation.z <= 0.5)
            {
                SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed * 10);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(horn);
        }
               
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed); //Déplacement du véhicule en fonction du temps. Note: le véhicule avance tout le temps, il ne peut pas reculer
        FLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        FRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        
        odometer += transform.position.z/2 - odometer;
        Odo.text = odometer.ToString();

        //Si le véhicule route, il peut tourné, sinon, il ne peut pas
        //Debug.Log(speed);
        if (speed > 0)
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //on fait tourné le véhicule quand on donne un input horizontal
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
    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route, il perd
        if ((transform.position.y < -10 || transform.position.y > 5) && SceneManager.GetActiveScene().name == "Infinite")
        {
            gameManager.km = odometer;
            SceneManager.LoadScene("LooseInfinite"); //Chargement de la scène Loose
        }


        else if ((transform.position.y < -10 || transform.position.y > 5) && SceneManager.GetActiveScene().name == "Chrono")
        {
            gameManager.km = odometer;
            SceneManager.LoadScene("LooseChrono"); //Chargement de la scène Loose
        }

        //Si le véhicule est en l'air, il ne peut plus avancé
        /*if (transform.position.y > 2 || transform.position.y < -1)
            speed = 0;
        else
            speed = SaveSpeed;*/

    }
}
