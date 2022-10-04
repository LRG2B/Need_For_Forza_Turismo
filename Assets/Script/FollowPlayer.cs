using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour //Script de suivit du joueur
{
    private GameObject player; //Déclaration du player
    private Vector3 offset = new Vector3(3.3f, 2.04f, -4.04f); //initialisation de la position de la caméra derrière le player
    
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //On récupère position du player
        transform.position = player.transform.position + offset; // On daplace la caméra en fonction de la position du player
    }
}
