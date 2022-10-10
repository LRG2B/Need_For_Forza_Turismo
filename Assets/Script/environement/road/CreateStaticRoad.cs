using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class CreateStaticRoad : MonoBehaviour //Script de g�n�ration al�atiore d'une route fini
{
    public GameObject[] obstacles; //Liste des obstacles
    public GameObject[] car; //Liste des v�hicules
    public GameObject normalRoad; //pav� de route

    public int LenghtMinRoad; //Longueur minimal de la route
    public int LenghtMaxRoad; //Longueur maximal de la route

    private int carSection; //Selection al�atoire de la voiture
    private int nbRoad; //Selection al�atoire de la longueur de la route
    private int depart = 3; //Nombre de route sans obstacles
    private Vector3 largRoad; //Dimension de 
    private float larg; //largeur de la route
    private float zt; // definition de la position initiale

    private void Start()
    {
        nbRoad = Random.Range(LenghtMinRoad, LenghtMaxRoad); //S�l�ction de la longueur de la route compris entre la valeur max et minimum
        largRoad = normalRoad.GetComponent<Renderer>().bounds.size; //r�cuperation des dimension d'un pav�
        larg = (largRoad.x / 2) - 2; //calcul de la largeur

        zt = transform.position.z; //d�finition de la position initiale

        Quaternion spawnRotation = Quaternion.Euler(0, 90, 0); //d�finition de la rotation de la route
        Quaternion obstacleRotation = Quaternion.Euler(0, 180, 0); //d�fintion de la rotation des obstacles

        //G�n�ration des route sans obstacles
        for (int j = 0; j < depart; ++j)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + j * 10); //d�finition de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //G�n�ration du pav�
        }

        //G�n�ration du reste de la route et des obstacles
        for(int i = depart; i < nbRoad; ++i)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + i*10); //d�finition de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //g�n�ration du pav�

            int randomObstacle = Random.Range(0, obstacles.Length); //s�lection al�atoire d'un osbtacle

            //si l'objet choisi est un objet mobile, il se g�n�re au centre de la route
            if (obstacles[randomObstacle].tag == "MoveCrate")
            {
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�n�ration d'une "MoveCrate"
            }
            //Sinon, on choisi un position al�atoire sur la route
            else
            {
                float defX = Random.Range(-larg, larg); //D�finition de la postion en x
                spawnPosition = transform.position + new Vector3(defX, 1, zt + i * 10); //d�finition des coordon�es
                Instantiate(obstacles[randomObstacle], spawnPosition, obstacleRotation); //g�n�artion de l'obstacle
            }
        }

        //G�n�artion des routes de fins pour l'arriv�
        for (int k = nbRoad; k < nbRoad + 5; ++k)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, zt + k * 10); //calcul de la position
            Instantiate(normalRoad, spawnPosition, spawnRotation); //g�n�ration du pav�
        }

        Quaternion carRotation = Quaternion.identity; //D�finition de la roation du v�hicule
        carSection = Random.Range(0, car.Length); //S�l�ction al�atoire de la voiture
        Instantiate(car[carSection], transform.position, carRotation); //g�n�ration de la voiture
    }

    private void LateUpdate()
    {
        //Si le joueur atteint la fin de la route, il gagne
        if (FindObjectOfType<PlayerController>().transform.position.z >= nbRoad*10)
            SceneManager.LoadScene("Win");
    }
}