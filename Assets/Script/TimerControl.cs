using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerControl : MonoBehaviour
{
    public Text Timer;
    public AudioSource music;

    private float timeRemaining = 0;
    private float time = 0;
    public bool timerIsRunning = false;

    private GameManager gameManager;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining = music.clip.length;
        Instantiate(music);
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (timerIsRunning)
        {

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                time += Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            else
            {
                timeRemaining = 0;
                time = music.clip.length;
                timerIsRunning = false;
                SceneManager.LoadScene("Win");
            }
            gameManager.time = time;
        }
    }
}
