using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeze_rot : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = GameObject.Find("Bus").transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            
    }
}
