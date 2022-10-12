using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    public Text Timer;

    public float timeRemaining = 0;
    public bool timerIsRunning = false;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
       if(Input.GetKeyDown("o"))
           GameObject.Find("test").SetActive(false);

        if (Input.GetKeyDown("p"))

            GameObject.Find("test").SetActive(true);


        if (timerIsRunning)
        {

            timeRemaining += Time.deltaTime;
            //Timer.text = timeRemaining.ToString();
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            /**if (timeRemaining > 0)
            {
                timeRemaining += Time.deltaTime;
                Timer.text = timeRemaining.ToString();
            }

            else
            {
                Debug.Log("Time has run out !");
                timeRemaining = 0;
                timerIsRunning = false;
            }*/
        }
    }
}
