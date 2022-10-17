using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int pctg_straight_road_hard = 50;
    public int pctg_straight_road_easy = 5;
    public float min_speed_hard = 5;
    public float min_speed_easy = 0.4f;


    int current_pctg_straight_road;
    float current_min_speed;

    public float time = 0;
    public float km = 0;
    Text timer;
    Text odo;
    float infinte;

    public static GameManager instance;

    //--------------------------- Tuto changer la dificulter du jeu -----------------------
    // Tu seras par default en facile
    // si tu veux en difficile il faut juste lancer la fonction Hard_mode() avec cette ligne : dificulty_manager.instance.Hard_mode()
    // si tu veux remettre en facile il faut juste lancer la fonction Easy_mode() avec cette : dificulty_manager.instance.Easy_mode()
    // Les ligne suivante fonctionne partout et tout le temps (même en cour de parti si il faut).

    private void Start()
    {
        infinte = 0;
        DontDestroyOnLoad(instance);
    }

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Game Manager deja existant");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    private void LateUpdate()
    {

        if (GameObject.FindGameObjectWithTag("Chrono"))
        {
            timer = GameObject.FindGameObjectWithTag("Chrono").GetComponent<Text>(); ;
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (GameObject.FindGameObjectWithTag("Odometer"))
        {
            odo = GameObject.FindGameObjectWithTag("Odometer").GetComponent<Text>();
            odo.text = km.ToString();
        }
        infinte += 0.01f;
    }

    public void Hard_mode()
    {
        current_pctg_straight_road = pctg_straight_road_hard;
        current_min_speed = min_speed_hard;
    }

    public void Easy_mode()
    {
        current_pctg_straight_road = pctg_straight_road_easy;
        current_min_speed = min_speed_easy;
    }

    public void Infinite_mode() // fait comme le easy juste avec un autre nom pour plus de logique dans le reste du code
    {
        current_pctg_straight_road = pctg_straight_road_easy;
        current_min_speed = min_speed_easy + infinte;
    }

    public float get_min_speed()
    {
        return current_min_speed;
    }

    public int get_pctg_straight_road()
    {
        return current_pctg_straight_road;
    }
}
