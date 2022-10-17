using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Switch_cam : MonoBehaviour //Script permettant le changement de camera
{
    public GameObject[] tab_waypoint;//initialisation de la position de la caméra derrière le player
    Transform pos_cam; //position de la camera courante
    int target; //indice de la camera selectionné


    private void Start()
    {
        pos_cam = tab_waypoint[0].transform; //valeur par default a la camera exterieur

    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C)) //si la touche c est presser on change la valeur de target a la suivante dans le tableau
        {
            target = (target + 1) % tab_waypoint.Length;
            pos_cam = tab_waypoint[target].transform;
        }
        transform.position =  pos_cam.position; //recup de la position de la cam
        transform.rotation =  pos_cam.rotation; //recup de la rotation de la cam
    }
}
