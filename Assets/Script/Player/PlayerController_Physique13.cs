using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Physique13 : MonoBehaviour //Script de contr�le du v�hicule en utilisant le RigidBody
{
    private float speed = 1f; //Vitesse utilis� pour avancer
    private float SaveSpeed; //On recup�re la vitesse de mani�re constante

    private float turnSpeed = 20f;  //Vitesse de la voiture quand elle tourne

    //D�claration des input du clavier
    private float horizontalInput;
    private float forwardInput;

    private Rigidbody Rb; //d�finition du RigidBody du player


    private void Start()
    {
        Rb = GetComponent<Rigidbody>(); //On r�cup�re le rigidbody du v�hicule
        SaveSpeed = speed; //On r�cup�re la bonne vitesse
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //d�claration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // d�claration de l'input vertical
    }

    private void FixedUpdate()
    {
        //D�placement du rigidbody en fonction du temps. Note: le v�hicule avance tout le temps, il ne peut pas reculer
        Rb.MovePosition(Rb.transform.position + new Vector3(0, 0, speed) * (forwardInput + 1));

        //Roation du rigidbody sur l'axe y, ne fontionne pas
        if (forwardInput >= 0)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.fixedDeltaTime);

    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route, il perd
        if (transform.position.y < -10)
            SceneManager.LoadScene("Loose"); //Chargement de la sc�ne Loose

        //Si le v�hicule est en l'air, il ne peut plus avanc�
        if (transform.position.y > 2 || transform.position.y < -1)
            speed = 0;
        else
            speed = SaveSpeed;

    }
}
