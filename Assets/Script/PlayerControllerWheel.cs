using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerWheel : MonoBehaviour
{
    //D�finition de la vitesse

    private float speed; //Vitesse utilis� pour avancer

    public float MaxSpeed = 40f;
    private float MinSpeed = 5f;
    private float acc = 0.4f;
    public AnimationCurve speedAcc;
    private float frein = 0.4f;
    public AnimationCurve speedMin;

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

    private void Start()
    {
        speed = MinSpeed; //On r�cup�re la bonne vitesse
        FLwheel = GameObject.Find("WHEEL_LF_1");
        RLwheel = GameObject.Find("WHEEL_LR_1");
        FRwheel = GameObject.Find("WHEEL_RF_1");
        RRwheel = GameObject.Find("WHEEL_RR_1");
        SW = GameObject.Find("STEER_HR");
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //d�claration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // d�claration de l'input vertical
        SW.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed * 10 * -horizontalInput);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed); //D�placement du v�hicule en fonction du temps. Note: le v�hicule avance tout le temps, il ne peut pas reculer
        FLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RLwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        FRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);
        RRwheel.transform.Rotate(new Vector3(1,0,0) * Time.fixedDeltaTime * speed*10);

        //Si le v�hicule route, il peut tourn�, sinon, il ne peut pas
        //Debug.Log(speed);
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
    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route, il perd
        /*if (transform.position.y < -10)
            SceneManager.LoadScene("Loose"); //Chargement de la sc�ne Loose*/

    }
}
