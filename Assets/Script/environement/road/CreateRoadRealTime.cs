using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoadRealTime : MonoBehaviour //Script de génération de la route en temps réel
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject[] car; //Liste des véhicules disponibles
    public GameObject normalRoad; //Route sans barrière
    public GameObject barrierRoad; //Route avec Barrière

    private int depart = 8; //Défini le nombre de route dans obstacle à la génération
    private int deb = 0; //constante qui sert de point de début dans la liste des obstacles

    //Les valeurs qui suivent sont initialisées, récupérées ou calculées dans la fonction Start.
    private int carSection; //Séléction aléatoire du véhicule
    private int randomObstacle; //Séléction aléatoire de l'obstacle à chaque frame

    private GameObject player; //Récupertaion du Player et donc de sa position
    private GameObject road; // définition de la route générée

    private Vector3 largRoad; //Contient les dimension x y z d'un pavé de route
    
    private float larg; //Répresente la largeur de la route
    private float longu; //Réprésente la longeur d'un pavé de route
    private float zt; //Récupère la position du dernier morceau de route
    

    Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //Rotation de la route pour qu'elle soit dans le bon sens
    Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //Rotation des obstacles pour qu'il soit face au joueur

    void Start()
    {
        largRoad = normalRoad.GetComponent<Renderer>().bounds.size; //Récupération de la taille de la route
        larg = (largRoad.x / 2) - 2; // calcule de la largeur depuis le centre
        longu = largRoad.z; //Calcule de la longeur
        road = barrierRoad; //définition des premières routes générées


        zt = transform.position.z - 2 * longu; //récupération de la position initiale du GameManager et donc de la route et du player

        //Génération des morceaus de route sans obstacle
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //définition de la position du pavé
            Instantiate(road, spawnPosition, spawnRotation); // génération de la route
            zt += longu; // on ajoute la longeur d'un pavé au zt pour que la route suivant se génère après la précédente
        }

        Quaternion carRotation = Quaternion.identity; //défition de la rotation de la voiture
        carSection = Random.Range(0, car.Length); //Sélection aléatoire du véhicule
        Instantiate(car[carSection], transform.position, carRotation); //Génération du véhicule sur la route

        player = GameObject.FindGameObjectWithTag("Player"); //récupartion des informations sur le player
    }

    void Update()
    {
        //On vérifie la distance parcourue, si elle est assez grande, on ne mets plus de barrière et on ne gère plus d'obstacle vide
        if (zt > longu * 75)
        {
            deb = 1;
            road = normalRoad;
        }

        //Génération de la route en temps réel, On récupère la position de la voiture, si elle est loin on ne génère pas, sinon on génère un morceau de rote et un obstacle
        if (player.transform.position.z >= (zt - 25 * longu)) 
        {
            
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //définition de la position de la route
            Instantiate(road, spawnPosition, spawnRotation); //Génération de la route

            randomObstacle = Random.Range(deb, obstacles.Length); //sélection aléatoire d'un obstacle dans la liste donnée
            
            //Si l'obstacle est un caisse qui bouge, elle spawn au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //génartion de la "MoveCrate"
            }
            
            //Sinon on selectionne une ostion aléatoire sur la route et on génère l'objet
            else
            {
                float defX = Random.Range(-larg, larg); //sélection de position
                spawnPosition = transform.position + new Vector3(defX, 1, zt + 10); //défintion de l'emplacement
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //génération de l'obstacle
            }
            zt += longu; //on passe au pavé suivant
        }
    }
}
