using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour //Script de suivit du joueur
{
    private GameObject player; //D�claration du player
    private Vector3 offset = new Vector3(3.3f, 2.04f, -4.04f); //initialisation de la position de la cam�ra derri�re le player
    
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //On r�cup�re position du player
        transform.position = player.transform.position + offset; // On daplace la cam�ra en fonction de la position du player
    }
}
