using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour //Script de management des Scène
{
    public static SceneControl instance;

    private void Awake()
    {

        //If there is an instance, and it's not me, delete myself.

        if (instance != null || instance != this)
        {
            Debug.LogWarning("instance de SceneControl deja existant ");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); //Chargement du menu
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); //Chargement du jeu
    }

    public void QuitGame()
    {
        Application.Quit(); //on quitte le logiciel
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadMenu();
    }
}
