using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour //Script de contrôle du véhicule
{
    //Définition de la vitesse
    
    private float speed = 20; //Vitesse utilisé pour avancer
    private float SaveSpeed; //On recupère la vitesse de manière constante

    public float MaxSpeed = 40f;
    private float MinSpeed = 5f;
    private float acc = 0.4f;
    public AnimationCurve speedAcc;
    private float frein = 0.4f;
    public AnimationCurve speedMin;

    [SerializeField]
    private float turnSpeed; //Vitesse de la voiture quand elle tourne

    //Déclaration des input du clavier
    private float horizontalInput;
    private float forwardInput;

    private void Start()
    {
        SaveSpeed = speed; //On récupère la bonne vitesse
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //déclaration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // déclaration de l'input vertical
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed ); //Déplacement du véhicule en fonction du temps. Note: le véhicule avance tout le temps, il ne peut pas reculer

        //Si le véhicule route, il peut tourné, sinon, il ne peut pas
        Debug.Log(speed);
        if(speed > 0)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //on fait tourné le véhicule quand on donne un input horizontal

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
            SceneManager.LoadScene("Loose"); //Chargement de la scène Loose

        //Si le véhicule est en l'air, il ne peut plus avancé
        //if (transform.position.y > 2 || transform.position.y < -1)
        //    speed = 0;
        //else
        //    speed = SaveSpeed;

    }
}
