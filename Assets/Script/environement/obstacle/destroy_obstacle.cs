using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script pour détruire les obstacles.
public class destroy_obstacle : MonoBehaviour
{
    void LateUpdate()
    {
        //Si l'obstacle tombe, il se détruit
        if (this.gameObject.transform.position.y < -1)
            Destroy(this.gameObject);
    }
}
