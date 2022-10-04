using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour //Script de déplacement des obstacles
{
    public GameObject road; //On récuère la route pour avoir ses dimensions

    public float MinSpeed; //On défini une vitesse minimum
    public float MaxSpeed; //On défini une vitesse maximum

    private float movingSpeed; //On séléction une vitesse comprise entre la vitesse max et la minimum
    private Vector3 largRoad; //définition des dimensions de la route
    private Vector3 pos; //définition de la postion de l'obstacle

    private void Start()
    {
        largRoad = road.GetComponent<Renderer>().bounds.size; //On récupère les dimension de la route
        pos = transform.position; //On récupère la position de génération de l'obstacle
        movingSpeed = Random.Range(MinSpeed, MaxSpeed); //On défini une vitesse aléatiore
    }

    void Update()
    {
        
         transform.Translate(Vector3.right * movingSpeed * Time.deltaTime); //Déplacement de l'obstacle

        //On vérifie la position de l'obstacle sur la route, il fait demi tour si il arrive au bout
        if (transform.position.x < pos.x + (largRoad.x / 2) - 2)
            movingSpeed = -movingSpeed;
        if (transform.position.x > pos.x - ((largRoad.x / 2) - 2))
            movingSpeed = -movingSpeed;

    }

    //Si l'obstacle touche le joueur, il s'arrète
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            movingSpeed = 0;
        }
    }
}
