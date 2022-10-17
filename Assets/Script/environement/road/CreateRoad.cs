using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    //
    // Script qui g�re la cr�ation de la route, des obstacle et des pickable en fonction des valeur donn�e par le Game Manager 
    //


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
    private int road_choise; //index de la route choisie
    private int compteur; // compte le nombre de route instentier
    private int pctg_straith_road; // pourcentage route �troite


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
        road_choise = 0; // intialisation par default avec la route large
        pctg_straith_road = GameManager.instance.get_pctg_straight_road(); //recup de la valeur depuis le Game manager
    }

    void Update()
    {
        //G�n�ration de la route en temps r�el, On r�cup�re la position de la voiture, si elle est loin on ne g�n�re pas, sinon on g�n�re un morceau de route et un obstacles

        if (compteur > 20 ) //test si on a creer plus de 20 morceau et si c'est le cas on relance le tirage de variant de route
        {
            compteur = 0; //remise a zeros
            if (Random.Range(0, 100) < pctg_straith_road ) // tirage au sort et test si on restre dans le pourcentage de route �troite
            {
                road_choise = 1;
            }

            largRoad = road[road_choise].GetComponent<Renderer>().bounds.size; //R�cup�ration de la taille de la route
            larg = (largRoad.x / 2) - 1; // calcule de la largeur depuis le centre
            longu = largRoad.z; //Calcule de la longeur
        }
        if (player.transform.position.z >= (zt - 25 * longu)) // si trop pr�s du bout de la route on en reg�nere
        {

            Vector3 spawnPosition = transform.position + new Vector3(0, 2, zt + longu); //d�finition de la position de la route
            Vector3 roadPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position de la route
            Instantiate(road[road_choise], roadPosition, spawnRotation); //G�n�ration de la route

            randomObstacle = Random.Range(deb, obstacles.Length); //s�lection al�atoire d'un obstacle dans la liste donn�e

            //Si l'obstacle est un jericanne d'essence
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
            compteur++; // incrementation
        }
    }
}
