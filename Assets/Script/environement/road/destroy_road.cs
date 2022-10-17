using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_road : MonoBehaviour //Script de destruction de la route
{
    private GameObject car; //On r�cup�re la voiture du player
    
    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player"); //On trouve le player sur la sc�ne
    }
    private void LateUpdate()
    {
        //Si le joueur est 30 unit�s plus loin que l'objet, ce dernier ce d�truit
        if (car.transform.position.z > this.gameObject.transform.position.z+30)
        {
            Destroy(this.gameObject);
        }
    }
}
