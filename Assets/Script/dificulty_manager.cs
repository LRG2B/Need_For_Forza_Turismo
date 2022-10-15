using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dificulty_manager : MonoBehaviour
{

    public int pctg_straight_road_hard = 50;
    public int pctg_straight_road_easy = 5;
    public float min_speed_hard = 5 ;
    public float min_speed_easy = 0.4f;


    int current_pctg_straight_road;
    float current_min_speed;

    public static dificulty_manager instance;


    //--------------------------- Tuto changer la dificulter du jeu -----------------------
    // Tu seras par default en facile
    // si tu veux en dificile il faut juste lancer la fonction Hard_mode() avec cette ligne : dificulty_manager.instance.Hard_mode()
    // si tu veux remettre en facile il faut juste lancer la fonction Easy_mode() avec cette : dificulty_manager.instance.Easy_mode()
    // Les ligne suivante fonctionne partout et tout le temps (même en cour de parti si il faut).

    void Start()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("dificulty_manager deja existant ");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        //Hard_mode();  //ta juste a décomenter ça pour teste rapidement en mode dificile par default en facile

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
        Easy_mode();
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
