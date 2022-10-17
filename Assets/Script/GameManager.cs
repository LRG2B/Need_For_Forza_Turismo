using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    //
    // Game manager gerant les paramêtre relatif au gameplay :
    //     - Envoie des paramètres de la génération de la route
    //     - Evoie des score a l'IU

    public int pctg_straight_road_hard = 50; //pourcentage de route étroite en mode difficile
    public int pctg_straight_road_easy = 5; //pourcentage de route étroite en mode facile
    public float min_speed = 0.4f; //vitesse min


    int current_pctg_straight_road; // valeur actuelle du pourcentage de route étroite
    float current_min_speed; // valeur actuelle de la vitesse min

    public float time = 0; //valeur de temps en chronos
    public float km = 0; //valeur de distance en infini

    // ref au texte de l'ui
    Text timer;
    Text odo;

    public static GameManager instance;

    private void Start()
    {
        DontDestroyOnLoad(instance);
    }

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    private void LateUpdate()
    {

        if (GameObject.FindGameObjectWithTag("Chrono")) // si un object avec le tag Chromnos existe alors on stocke la valeur de temps 
        {
            timer = GameObject.FindGameObjectWithTag("Chrono").GetComponent<Text>(); ;
             
                //formatage du temps
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (GameObject.FindGameObjectWithTag("Odometer"))  // si un object avec le tag Chromnos existe alors on stocke la valeur de temps 
        {
            odo = GameObject.FindGameObjectWithTag("Odometer").GetComponent<Text>();
            odo.text = km.ToString("F2");
        }
    }

    public void Hard_mode() //permet de passer les valeur pour le mode difficile 
    {
        current_pctg_straight_road = pctg_straight_road_hard;
        current_min_speed = min_speed;
    }

    public void Easy_mode() //permet de passer les valeur pour le mode facile
    {
        current_pctg_straight_road = pctg_straight_road_easy;
        current_min_speed = min_speed;
    }

    public void Infinite_mode() //permet de passer les valeur pour le mode infini 
    {
        // fait comme le easy juste avec un autre nom pour plus de logique dans le reste du code
        current_pctg_straight_road = pctg_straight_road_easy;
        current_min_speed = min_speed;
    }

    public float get_min_speed() //getteur de la min_speed
    {
        return current_min_speed;
    }

    public int get_pctg_straight_road() //getteur du pourcentage de route étroite
    {
        return current_pctg_straight_road;
    }
}
