using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject car; //Liste des véhicules disponibles
    public GameObject[] road; //Routes

    private int depart = 8; //Défini le nombre de route dans obstacle à la génération
    private int deb = 0; //constante qui sert de point de début dans la liste des obstacles

    //Les valeurs qui suivent sont initialisées, récupérées ou calculées dans la fonction Start.
    private int randomObstacle; //Séléction aléatoire de l'obstacle à chaque frame

    private GameObject player; //Récupertaion du Player et donc de sa position

    private Vector3 largRoad; //Contient les dimension x y z d'un pavé de route

    private float larg; //Répresente la largeur de la route
    private float longu; //Réprésente la longeur d'un pavé de route
    private float zt; //Récupère la position du dernier morceau de route
    private int road_choise;
    private int compteur;
    private int pctg_straith_road;


    Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //Rotation de la route pour qu'elle soit dans le bon sens
    Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //Rotation des obstacles pour qu'il soit face au joueur
    Quaternion obstacleRotation_oil_tank = Quaternion.Euler(0, 90, -90); //Rotation des oil_tank

    void Start()
    {
        largRoad = road[0].GetComponent<Renderer>().bounds.size; //Récupération de la taille de la route
        larg = (largRoad.x / 2) - 1; // calcule de la largeur depuis le centre
        longu = largRoad.z; //Calcule de la longeur

        zt = transform.position.z - 2 * longu; //récupération de la position initiale du GameManager et donc de la route et du player

        //Génération des morceaus de route sans obstacle
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //définition de la position du pavé
            Instantiate(road[0], spawnPosition, spawnRotation); // génération de la route
            zt += longu; // on ajoute la longeur d'un pavé au zt pour que la route suivant se génère après la précédente
        }

        //Quaternion carRotation = Quaternion.identity; //défition de la rotation de la voiture
        //Instantiate(car, transform.position, carRotation); //Génération du véhicule sur la route

        player = GameObject.FindGameObjectWithTag("Player"); //récupartion des informations sur le player
        road_choise = 0;
        pctg_straith_road = dificulty_manager.instance.get_pctg_straight_road();
    }

    void Update()
    {
        //Génération de la route en temps réel, On récupère la position de la voiture, si elle est loin on ne génère pas, sinon on génère un morceau de rote et un obstacles
        if (compteur > 20 )
        {
            compteur = 0;
            if (Random.Range(0, 100) < pctg_straith_road )
            {
                road_choise = 1;
            }

            largRoad = road[road_choise].GetComponent<Renderer>().bounds.size; //Récupération de la taille de la route
            larg = (largRoad.x / 2) - 1; // calcule de la largeur depuis le centre
            longu = largRoad.z; //Calcule de la longeur
        }
        if (player.transform.position.z >= (zt - 25 * longu))
        {

            Vector3 spawnPosition = transform.position + new Vector3(0, 2, zt + longu); //définition de la position de la route
            Vector3 roadPosition = transform.position + new Vector3(0, 0, zt + longu); //définition de la position de la route
            Instantiate(road[road_choise], roadPosition, spawnRotation); //Génération de la route

            randomObstacle = Random.Range(deb, obstacles.Length); //sélection aléatoire d'un obstacle dans la liste donnée

            //Si l'obstacle est un caisse qui bouge, elle spawn au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //génartion de la "MoveCrate"
            }
            if (obstacles[randomObstacle].tag == "oil_tank")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation_oil_tank); //génartion de la "MoveCrate"
            }

            //Sinon on selectionne une ostion aléatoire sur la route et on génère l'objet
            else
            {
                float defX = Random.Range(-larg, larg); //sélection de position
                spawnPosition = transform.position + new Vector3(defX, 1, zt + 10); //défintion de l'emplacement
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //génération de l'obstacle
            }
            zt += longu; //on passe au pavé suivant
            compteur++;
        }
    }
}
