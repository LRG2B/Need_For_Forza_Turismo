using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Physique13 : MonoBehaviour //Script de contrôle du véhicule en utilisant le RigidBody
{
    private float speed = 1f; //Vitesse utilisé pour avancer
    private float SaveSpeed; //On recupère la vitesse de manière constante

    private float turnSpeed = 20f;  //Vitesse de la voiture quand elle tourne

    //Déclaration des input du clavier
    private float horizontalInput;
    private float forwardInput;

    private Rigidbody Rb; //définition du RigidBody du player


    private void Start()
    {
        Rb = GetComponent<Rigidbody>(); //On récupère le rigidbody du véhicule
        SaveSpeed = speed; //On récupère la bonne vitesse
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //déclaration de l'input horizontal
        forwardInput = Input.GetAxis("Vertical"); // déclaration de l'input vertical
    }

    private void FixedUpdate()
    {
        //Déplacement du rigidbody en fonction du temps. Note: le véhicule avance tout le temps, il ne peut pas reculer
        Rb.MovePosition(Rb.transform.position + new Vector3(0, 0, speed) * (forwardInput + 1));

        //Roation du rigidbody sur l'axe y, ne fontionne pas
        if (forwardInput >= 0)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.fixedDeltaTime);

    }

    private void LateUpdate()
    {
        //Si le joueur tombe de la route, il perd
        if (transform.position.y < -10)
            SceneManager.LoadScene("Loose"); //Chargement de la scène Loose

        //Si le véhicule est en l'air, il ne peut plus avancé
        if (transform.position.y > 2 || transform.position.y < -1)
            speed = 0;
        else
            speed = SaveSpeed;

    }
}
