using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alea_spawn : MonoBehaviour
{
    float larg_road;
    float long_road;
    float pos_road;
    float new_pos_x;
    float new_pos_y;
    Vector3 pos_obstacle;
    public GameObject Road;
    public GameObject[] obstacle;
    GameObject selected_obstacle;
    // Start is called before the first frame update
    void Start()
    {
        //selection de l'obstacle
        int i = Random.Range(0, obstacle.Length);      
        selected_obstacle = obstacle[i];

        //generation de ça position
        larg_road = Road.GetComponent<Renderer>().bounds.size.x ;
        long_road = Road.GetComponent<Renderer>().bounds.size.z ;
        pos_road = Road.transform.position.x ;
        new_pos_x = Random.Range(-larg_road / 2 , larg_road / 2);
        new_pos_x += pos_road;
        pos_obstacle = new Vector3(new_pos_x, Road.transform.position.y + 1, Road.transform.position.z + long_road / 2);

        //generation de l'obsatacle
        GameObject.Instantiate(selected_obstacle, pos_obstacle , Quaternion.Euler(0, 0, 0), Road.transform);
    }

}
