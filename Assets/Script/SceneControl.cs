using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour //Script de management des Scène
{

    public void LoadMenu()
    {
        Destroy(GameObject.FindObjectOfType<GameManager>());
        SceneManager.LoadScene("Menu"); //Chargement du menu
    }

    public void LoadChrono()
    {
        Destroy(GameObject.FindObjectOfType<GameManager>());
        SceneManager.LoadScene("Chrono"); //Chargement du jeu
    }

    public void LoadInfinite()
    {
        Destroy(GameObject.FindObjectOfType<GameManager>());
        SceneManager.LoadScene("Infinite"); //Chargement du jeu
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
