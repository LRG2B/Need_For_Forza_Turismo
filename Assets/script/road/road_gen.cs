using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road_gen : MonoBehaviour
{

    GameObject start_road;
    Vector3 pos_next_road;
    Vector3 pos_last_road;
    GameObject parent_road;
    GameObject Bus;
    public GameObject[] ground_road;
    public int prct_obstacle;
    public int prct_barrier;
    float larg_ground_road;
    float long_ground_road;
    int alea;
    int choice;


    void Start()
    {
        start_road = GameObject.Find("Ground_road_start");
        parent_road = GameObject.Find("Road");
        Bus = GameObject.Find("Bus");
        long_ground_road = start_road.GetComponent<Collider>().bounds.size.z;
        print(start_road.GetComponent<Collider>().bounds.size);
        pos_next_road = start_road.transform.position;
        Generate_road_of_long(100);
    }


    void Update()
    {
        if (Bus.transform.position.z - pos_last_road.z < 100 )
        {
            Generate_road_of_long(5);
        }
    }

    void Generate_road_of_long(int long_road)
    {

        for (int i = 0; i < long_road; i++)
        {
            //choix si il y a des obstacle ou non
            alea = Random.Range(0, 100);
            if (alea < prct_obstacle && alea > 0)
                choice = 1;
            //if (alea > prct_barrier + prct_obstacle && alea < 100)
             //   choice = 2;
            else
                choice = 0;

              //creation de la route
            GameObject.Instantiate(ground_road[choice], pos_next_road , Quaternion.Euler(0, 90, 0), parent_road.transform );
            pos_next_road = new Vector3(0, 0, pos_next_road.z + long_ground_road);
        }
        pos_last_road = new Vector3(0, 0, pos_next_road.z - long_ground_road);
    }
}
