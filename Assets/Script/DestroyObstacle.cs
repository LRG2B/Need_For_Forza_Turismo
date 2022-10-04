using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script pour d�truire les obstacles.
public class DestroyObstacle : MonoBehaviour
{
    void LateUpdate()
    {
        //Si l'obstacle tombe, il se d�truit
        if (this.gameObject.transform.position.y < -1)
            Destroy(this.gameObject);
    }
}
