using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto_destroy : MonoBehaviour
{
    GameObject bus;
    float distance;
    bool bus_exist;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Bus") != null)
        {
            bus = GameObject.Find("Bus");
            bus_exist = true;
        }
        else
            bus_exist = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bus_exist)
        {
            distance = transform.position.z - bus.transform.position.z;
            if (distance < -20)
            {
                Destroy(gameObject);
            }
        }
    }
}
