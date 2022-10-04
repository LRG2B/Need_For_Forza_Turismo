using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoadRealTime : MonoBehaviour //Script de g�n�ration de la route en temps r�el
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject[] car; //Liste des v�hicules disponibles
    public GameObject normalRoad; //Route sans barri�re
    public GameObject barrierRoad; //Route avec Barri�re

    private int depart = 8; //D�fini le nombre de route dans obstacle � la g�n�ration
    private int deb = 0; //constante qui sert de point de d�but dans la liste des obstacles

    //Les valeurs qui suivent sont initialis�es, r�cup�r�es ou calcul�es dans la fonction Start.
    private int carSection; //S�l�ction al�atoire du v�hicule
    private int randomObstacle; //S�l�ction al�atoire de l'obstacle � chaque frame

    private GameObject player; //R�cupertaion du Player et donc de sa position
    private GameObject road; // d�finition de la route g�n�r�e

    private Vector3 largRoad; //Contient les dimension x y z d'un pav� de route
    
    private float larg; //R�presente la largeur de la route
    private float longu; //R�pr�sente la longeur d'un pav� de route
    private float zt; //R�cup�re la position du dernier morceau de route
    

    Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //Rotation de la route pour qu'elle soit dans le bon sens
    Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //Rotation des obstacles pour qu'il soit face au joueur

    void Start()
    {
        largRoad = normalRoad.GetComponent<Renderer>().bounds.size; //R�cup�ration de la taille de la route
        larg = (largRoad.x / 2) - 2; // calcule de la largeur depuis le centre
        longu = largRoad.z; //Calcule de la longeur
        road = barrierRoad; //d�finition des premi�res routes g�n�r�es


        zt = transform.position.z - 2 * longu; //r�cup�ration de la position initiale du GameManager et donc de la route et du player

        //G�n�ration des morceaus de route sans obstacle
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position du pav�
            Instantiate(road, spawnPosition, spawnRotation); // g�n�ration de la route
            zt += longu; // on ajoute la longeur d'un pav� au zt pour que la route suivant se g�n�re apr�s la pr�c�dente
        }

        Quaternion carRotation = Quaternion.identity; //d�fition de la rotation de la voiture
        carSection = Random.Range(0, car.Length); //S�lection al�atoire du v�hicule
        Instantiate(car[carSection], transform.position, carRotation); //G�n�ration du v�hicule sur la route

        player = GameObject.FindGameObjectWithTag("Player"); //r�cupartion des informations sur le player
    }

    void Update()
    {
        //On v�rifie la distance parcourue, si elle est assez grande, on ne mets plus de barri�re et on ne g�re plus d'obstacle vide
        if (zt > longu * 75)
        {
            deb = 1;
            road = normalRoad;
        }

        //G�n�ration de la route en temps r�el, On r�cup�re la position de la voiture, si elle est loin on ne g�n�re pas, sinon on g�n�re un morceau de rote et un obstacle
        if (player.transform.position.z >= (zt - 25 * longu)) 
        {
            
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position de la route
            Instantiate(road, spawnPosition, spawnRotation); //G�n�ration de la route

            randomObstacle = Random.Range(deb, obstacles.Length); //s�lection al�atoire d'un obstacle dans la liste donn�e
            
            //Si l'obstacle est un caisse qui bouge, elle spawn au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�nartion de la "MoveCrate"
            }
            
            //Sinon on selectionne une ostion al�atoire sur la route et on g�n�re l'objet
            else
            {
                float defX = Random.Range(-larg, larg); //s�lection de position
                spawnPosition = transform.position + new Vector3(defX, 1, zt + 10); //d�fintion de l'emplacement
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�n�ration de l'obstacle
            }
            zt += longu; //on passe au pav� suivant
        }
    }
}
