using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;

    //public GameObject pauseMenuUI;
    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        animator.SetBool("IsOpen", false);
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause()
    {
        animator.SetBool("IsOpen", true);
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
}
