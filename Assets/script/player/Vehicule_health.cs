using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicule_health : MonoBehaviour
{
    public int health;
    public int domage;

    public healthbar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        health = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("obstacle"))
        {
            health -= domage;
            healthbar.set_health(health);
        }

    }
}
