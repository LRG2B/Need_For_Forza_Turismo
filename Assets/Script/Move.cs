using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour //Script de d�placement des obstacles
{
    public GameObject road; //On r�cu�re la route pour avoir ses dimensions

    public float MinSpeed; //On d�fini une vitesse minimum
    public float MaxSpeed; //On d�fini une vitesse maximum

    private float movingSpeed; //On s�l�ction une vitesse comprise entre la vitesse max et la minimum
    private Vector3 largRoad; //d�finition des dimensions de la route
    private Vector3 pos; //d�finition de la postion de l'obstacle

    private void Start()
    {
        largRoad = road.GetComponent<Renderer>().bounds.size; //On r�cup�re les dimension de la route
        pos = transform.position; //On r�cup�re la position de g�n�ration de l'obstacle
        movingSpeed = Random.Range(MinSpeed, MaxSpeed); //On d�fini une vitesse al�atiore
    }

    void Update()
    {
        
         transform.Translate(Vector3.right * movingSpeed * Time.deltaTime); //D�placement de l'obstacle

        //On v�rifie la position de l'obstacle sur la route, il fait demi tour si il arrive au bout
        if (transform.position.x < pos.x + (largRoad.x / 2) - 2)
            movingSpeed = -movingSpeed;
        if (transform.position.x > pos.x - ((largRoad.x / 2) - 2))
            movingSpeed = -movingSpeed;

    }

    //Si l'obstacle touche le joueur, il s'arr�te
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            movingSpeed = 0;
        }
    }
}
