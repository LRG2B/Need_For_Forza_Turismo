using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour //Script de contr�le du v�hicule
{
    //D�finition de la vitesse
    
    private float speed = 20; //Vitesse utilis� pour avancer
    private float SaveSpeed; //On recup�re la vitesse de mani�re constante

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

    private void Start()
    {
        SaveSpeed = speed; //On r�cup�re la bonne vitesse
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //d�claration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // d�claration de l'input vertical
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed ); //D�placement du v�hicule en fonction du temps. Note: le v�hicule avance tout le temps, il ne peut pas reculer

        //Si le v�hicule route, il peut tourn�, sinon, il ne peut pas
        Debug.Log(speed);
        if(speed > 0)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //on fait tourn� le v�hicule quand on donne un input horizontal

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
                speed -= frein * speedMin.Evaluate(-forwardInput)    ;
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
        if (transform.position.y < -10)
            SceneManager.LoadScene("Loose"); //Chargement de la sc�ne Loose

        //Si le v�hicule est en l'air, il ne peut plus avanc�
        //if (transform.position.y > 2 || transform.position.y < -1)
        //    speed = 0;
        //else
        //    speed = SaveSpeed;

    }
}
