using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class oil_tank : MonoBehaviour
{

    public int value = 100;

    private void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 200);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Acty")
        {
            wheel_controller.instance.Add_Km(value);
            Destroy(gameObject.transform.root.gameObject);
        }
    }
}
