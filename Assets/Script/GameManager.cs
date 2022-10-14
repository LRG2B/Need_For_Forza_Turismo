using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float time = 0;
    public float km = 0;
    Text timer;
    Text odo;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
            DontDestroyOnLoad(this.gameObject);
        else 
            Destroy(this.gameObject);
        
    }

    private void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Pierre"))
        {
            timer = GameObject.FindGameObjectWithTag("Pierre").GetComponent<Text>(); ;
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (GameObject.FindGameObjectWithTag("MoveCrate"))
        {
            odo = GameObject.FindGameObjectWithTag("MoveCrate").GetComponent<Text>();
            odo.text = km.ToString();
        }
    }
}
