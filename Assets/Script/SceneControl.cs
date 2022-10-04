using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour //Script de management des Scène
{
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
