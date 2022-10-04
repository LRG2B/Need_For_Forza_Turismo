using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour //Script de destruction de la route
{
    private GameObject car; //On récupère la voiture du player
    
    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player"); //On trouve le player sur la scène
    }
    private void LateUpdate()
    {
        //Si le joueur est 30 unités plus loin que l'objet, ce dernier ce détruit
        if (car.transform.position.z > this.transform.position.z+30)
        {
            Destroy(this.gameObject);
        }
    }
}
