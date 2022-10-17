using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class oil_tank : MonoBehaviour
{

    public int value = 100; //valeur a ajouter au score si il est recuperer

    private void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 200); //permet la rotation du jerricane sur lui même
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Acty") // si l'object toucher a pour nom acty alors  : 
        {
            truck_control.instance.Add_Km(value); // on envoie la valeur au script ... 
            Destroy(gameObject.transform.root.gameObject); //  et on detruit le jerricane.
        }
    }
}
