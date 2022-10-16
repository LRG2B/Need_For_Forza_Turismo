using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject car; //Liste des v�hicules disponibles
    public GameObject[] road; //Routes

    private int depart = 8; //D�fini le nombre de route dans obstacle � la g�n�ration
    private int deb = 0; //constante qui sert de point de d�but dans la liste des obstacles

    //Les valeurs qui suivent sont initialis�es, r�cup�r�es ou calcul�es dans la fonction Start.
    private int randomObstacle; //S�l�ction al�atoire de l'obstacle � chaque frame

    private GameObject player; //R�cupertaion du Player et donc de sa position

    private Vector3 largRoad; //Contient les dimension x y z d'un pav� de route

    private float larg; //R�presente la largeur de la route
    private float longu; //R�pr�sente la longeur d'un pav� de route
    private float zt; //R�cup�re la position du dernier morceau de route
    private int road_choise;
    private int compteur;
    private int pctg_straith_road;


    Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //Rotation de la route pour qu'elle soit dans le bon sens
    Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //Rotation des obstacles pour qu'il soit face au joueur
    Quaternion obstacleRotation_oil_tank = Quaternion.Euler(0, 90, -90); //Rotation des oil_tank

    void Start()
    {
        largRoad = road[0].GetComponent<Renderer>().bounds.size; //R�cup�ration de la taille de la route
        larg = (largRoad.x / 2) - 1; // calcule de la largeur depuis le centre
        longu = largRoad.z; //Calcule de la longeur

        zt = transform.position.z - 2 * longu; //r�cup�ration de la position initiale du GameManager et donc de la route et du player

        //G�n�ration des morceaus de route sans obstacle
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position du pav�
            Instantiate(road[0], spawnPosition, spawnRotation); // g�n�ration de la route
            zt += longu; // on ajoute la longeur d'un pav� au zt pour que la route suivant se g�n�re apr�s la pr�c�dente
        }

        //Quaternion carRotation = Quaternion.identity; //d�fition de la rotation de la voiture
        //Instantiate(car, transform.position, carRotation); //G�n�ration du v�hicule sur la route

        player = GameObject.FindGameObjectWithTag("Player"); //r�cupartion des informations sur le player
        road_choise = 0;
        pctg_straith_road = dificulty_manager.instance.get_pctg_straight_road();
    }

    void Update()
    {
        //G�n�ration de la route en temps r�el, On r�cup�re la position de la voiture, si elle est loin on ne g�n�re pas, sinon on g�n�re un morceau de rote et un obstacles
        if (compteur > 20 )
        {
            compteur = 0;
            if (Random.Range(0, 100) < pctg_straith_road )
            {
                road_choise = 1;
            }

            largRoad = road[road_choise].GetComponent<Renderer>().bounds.size; //R�cup�ration de la taille de la route
            larg = (largRoad.x / 2) - 1; // calcule de la largeur depuis le centre
            longu = largRoad.z; //Calcule de la longeur
        }
        if (player.transform.position.z >= (zt - 25 * longu))
        {

            Vector3 spawnPosition = transform.position + new Vector3(0, 2, zt + longu); //d�finition de la position de la route
            Vector3 roadPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position de la route
            Instantiate(road[road_choise], roadPosition, spawnRotation); //G�n�ration de la route

            randomObstacle = Random.Range(deb, obstacles.Length); //s�lection al�atoire d'un obstacle dans la liste donn�e

            //Si l'obstacle est un caisse qui bouge, elle spawn au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�nartion de la "MoveCrate"
            }
            if (obstacles[randomObstacle].tag == "oil_tank")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation_oil_tank); //g�nartion de la "MoveCrate"
            }

            //Sinon on selectionne une ostion al�atoire sur la route et on g�n�re l'objet
            else
            {
                float defX = Random.Range(-larg, larg); //s�lection de position
                spawnPosition = transform.position + new Vector3(defX, 1, zt + 10); //d�fintion de l'emplacement
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�n�ration de l'obstacle
            }
            zt += longu; //on passe au pav� suivant
            compteur++;
        }
    }
}
