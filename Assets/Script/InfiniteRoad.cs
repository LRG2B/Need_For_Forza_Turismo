using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject normalRoad; //Route sans barri�re

    private GameObject player; //R�cupertaion du Player et donc de sa position

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


        zt = transform.position.z - 2 * longu; //r�cup�ration de la position initiale du GameManager et donc de la route et du player

        player = GameObject.FindGameObjectWithTag("Player"); //r�cupartion des informations sur le player
    }

    void Update()
    {

        //G�n�ration de la route en temps r�el, On r�cup�re la position de la voiture, si elle est loin on ne g�n�re pas, sinon on g�n�re un morceau de rote et un obstacle
        if (player.transform.position.z >= (zt - 25 * longu))
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + longu); //d�finition de la position de la route
            Instantiate(normalRoad, spawnPosition, spawnRotation); //G�n�ration de la route

            zt += longu; //on passe au pav� suivant
        }
    }
}
