using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class truck_control : MonoBehaviour
{
    //
    // Script gerant le véhicule :
    //      - Ces déplacement
    //      - ceux des roue 
    //      - et ceux du volant
    //      - Verifie aussi si le véhicule sort de la zone de jeu
    //


        //D�finition de la vitesse
    private float speed; //Vitesse utilise pour avancer
    public float MaxSpeed;
    private float MinSpeed;
    private float acc = 0.4f;
    public AnimationCurve speedAcc;
    private float frein = 0.4f;
    public AnimationCurve speedMin;

        //definition de parametre pour l'UI
    public Text Odo;
    private float odometer = 0;
    private int bonus = 0;


    bool is_infinite; //boolean qui dit si l'on est en infinite ou pas


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
    GameObject Steering_wheel; //Volant

    public AudioSource horn; //ref au prefab horn

    private GameManager gameManager; //ref au Game manager

    public static truck_control instance;

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


            //recup de la reference des 4 roues
        FLwheel = GameObject.Find("WHEEL_LF_1");
        RLwheel = GameObject.Find("WHEEL_LR_1");
        FRwheel = GameObject.Find("WHEEL_RF_1");
        RRwheel = GameObject.Find("WHEEL_RR_1");

        Steering_wheel = GameObject.Find("STEER_HR"); // recup de la reférence du volant
        gameManager = GameManager.instance;
        MinSpeed = gameManager.get_min_speed(); //recup de la valeur de min_speed dans le game manager
        speed = MinSpeed; //On r�cup�re la bonne vitesse

        if (SceneManager.GetActiveScene().name == "Infinite") //regarde si l'on est dans le mode infinite
            is_infinite = true;
        else
            is_infinite = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //d�claration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // d�claration de l'input vertical

        Steering_wheel.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed * 10 * -horizontalInput); //rotation du volant

        if (horizontalInput == 0) // si l'on ne tourne pas le volant reviens au centre
        {
            if (Steering_wheel.transform.rotation.z >= 0.1f)
            {
                Steering_wheel.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -(turnSpeed + 5) * 10);
            }
            else if (Steering_wheel.transform.rotation.z <= -0.1f)
            {
                Steering_wheel.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * (turnSpeed + 5) * 10);
            }
        }


        if (Input.GetKeyDown(KeyCode.H)) // input pour le klaxon
        {
            Instantiate(horn); // tuuut !!! 
        }
               
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed); //D�placement du v�hicule en fonction du temps. Note: le v�hicule avance tout le temps, il ne peut pas reculer

            //rotation des 4 roues
        FLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        FRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        
            //calcul et envoie de la distance
        odometer += transform.position.z/2 - odometer; 
        Odo.text = (odometer + bonus ).ToString("F2");

        //Si le v�hicule roule, il peut tourn�, sinon, il ne peut pas
        if (speed > 0)
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //on fait tourn� le v�hicule quand on donne un input horizontal
        }

        // gestion de l'accélération et du freinage avec les courbes
        if (forwardInput > 0) //si la touche haut et presser l'on augmente la vitesse sauf si l'on dépasse le maximum
        {
            if (speed < MaxSpeed)
            {
                speed += acc * speedAcc.Evaluate(forwardInput);
            }
        }
        if (forwardInput < 0)  //si la touche bas et presser l'on reduit la vitesse sauf si l'on dépasse le minimum on l'on passe a zeros
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
        else //sinon l'on reduit lentement la vitesse jusqu'a la valeur de min_speed
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

        if (is_infinite) //si l'on est en mode infini on incremente la vitesse
            MaxSpeed += 0.01f;
    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route ou vole trop haut , il perd
        //ebsuite en revoie dans le menu correspondant au mode de jeu
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
    public void Add_Km(int value) // incrémenteur sur la valeur du bonus pour les jericannes
    {
        bonus += value;
    }

}
