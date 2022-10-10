using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_env_follow : MonoBehaviour
{
    private GameObject player; //Déclaration du player
    private Vector3 offset = new Vector3(0, 2.5f, -4.5f); //initialisation de la position de la caméra derrière le player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //On récupère position du player
    }

    void LateUpdate()
    {     
        transform.position = player.transform.position + offset; // On daplace la caméra en fonction de la position du player
    }
}
