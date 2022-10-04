using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject normalRoad; //Route sans barrière

    private GameObject player; //Récupertaion du Player et donc de sa position

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


        zt = transform.position.z - 2 * longu; //récupération de la position initiale du GameManager et donc de la route et du player

        player = GameObject.FindGameObjectWithTag("Player"); //récupartion des informations sur le player
    }

    void Update()
    {

        //Génération de la route en temps réel, On récupère la position de la voiture, si elle est loin on ne génère pas, sinon on génère un morceau de rote et un obstacle
        if (player.transform.position.z >= (zt - 25 * longu))
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //définition de la position de la route
            Instantiate(normalRoad, spawnPosition, spawnRotation); //Génération de la route

            zt += longu; //on passe au pavé suivant
        }
    }
}
