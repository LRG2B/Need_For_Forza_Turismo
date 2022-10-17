using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour //script qui permet la destructiuon de l'object generant le son du klaxon valeur rentrer dans l'inspecteur de 5 seconde
{
    public float aliveTime;
    

    void Start()
    {
        Destroy(gameObject, aliveTime);
    }
}
