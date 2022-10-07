using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environemment : MonoBehaviour
{
    private GameObject player; //D�claration du player
    private Vector3 offset = new Vector3(0, 5, -10); //initialisation de la position de la cam�ra derri�re le player

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //On r�cup�re position du player
        transform.position = player.transform.position + offset; // On daplace la cam�ra en fonction de la position du player
    }
}
