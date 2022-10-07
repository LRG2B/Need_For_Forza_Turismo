using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FollowPlayer : MonoBehaviour //Script de suivit du joueur
{
    private GameObject player; //D�claration du player
    public GameObject[] tab_waypoint;//initialisation de la position de la cam�ra derri�re le player
    Transform pos_cam;
    int target;


    private void Start()
    {
        pos_cam = tab_waypoint[0].transform;
    }

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //On r�cup�re position du player

        if (Input.GetKeyDown(KeyCode.C))
        {
            target = (target + 1) % tab_waypoint.Length;
            pos_cam = tab_waypoint[target].transform;
        }
        transform.position =  pos_cam.position;
        transform.rotation =  pos_cam.rotation;
    }
}
