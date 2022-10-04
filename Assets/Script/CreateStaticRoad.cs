using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class CreateStaticRoad : MonoBehaviour //Script de génération aléatiore d'une route fini
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject[] car; //Liste des véhicules
    public GameObject normalRoad; //pavé de route

    public int LenghtMinRoad; //Longueur minimal de la route
    public int LenghtMaxRoad; //Longueur maximal de la route

    private int carSection; //Selection aléatoire de la voiture
    private int nbRoad; //Selection aléatoire de la longueur de la route
    private int depart = 3; //Nombre de route sans obstacles
    private Vector3 largRoad; //Dimension de 
    private float larg; //largeur de la route
    private float zt; // definition de la position initiale

    private void Start()
    {
        nbRoad = Random.Range(LenghtMinRoad, LenghtMaxRoad); //Séléction de la longueur de la route compris entre la valeur max et minimum
        largRoad = normalRoad.GetComponent<Renderer>().bounds.size; //récuperation des dimension d'un pavé
        larg = (largRoad.x / 2) - 2; //calcul de la largeur

        zt = transform.position.z; //définition de la position initiale

        Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //définition de la rotation de la route
        Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //défintion de la rotation des obstacles

        //Génération des route sans obstacles
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + j * 10); //définition de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //Génération du pavé
        }

        //Génération du reste de la route et des obstacles
        for(int i = depart; i < nbRoad; ++i)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + i*10); //définition de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //génération du pavé

            int randomObstacle = Random.Range(0, obstacles.Length); //sélection aléatoire d'un osbtacle

            //si l'objet choisi est un objet mobile, il se génère au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //génération d'une "MoveCrate"
            }
            //Sinon, on choisi un position aléatoire sur la route
            else
            {
                float defX = Random.Range(-larg, larg); //Définition de la postion en x
                spawnPosition = transform.position + new Vector3(defX, 1, zt + i * 10); //définition des coordonées
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //généartion de l'obstacle
            }
        }

        //Généartion des routes de fins pour l'arrivé
        for (int k = nbRoad; k < nbRoad + 5; ++k)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + k * 10); //calcul de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //génération du pavé
        }

        Quaternion carRotation = Quaternion.identity; //Définition de la roation du véhicule
        carSection = Random.Range(0, car.Length); //Séléction aléatoire de la voiture
        Instantiate(car[carSection], transform.position, carRotation); //génération de la voiture
    }

    private void LateUpdate()
    {
        //Si le joueur atteint la fin de la route, il gagne
        if (FindObjectOfType<PlayerController>().transform.position.z >= nbRoad*10)
            SceneManager.LoadScene("Win");
    }
}