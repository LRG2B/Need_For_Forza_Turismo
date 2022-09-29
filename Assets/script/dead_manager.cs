using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dead_manager : MonoBehaviour
{
    GameObject bus;
    bool bus_exist = true;

    private void Awake()
    {
        Debug.Log("current scene = " + SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("GameManager"));

        if (GameObject.Find("Bus") != null)
        {
            bus = GameObject.Find("Bus");
            bus_exist = true;
        }
        else
            bus_exist = false;
    }

  
    // Update is called once per frame
    void LateUpdate()
    {
        if (bus == null)
            bus = GameObject.Find("Bus");

        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            //Debug.Log(bus.transform.position.y);
            if (bus_exist && bus.transform.position.y < -12)
            {
                Debug.Log("bus under the dead zone");
                Restart();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void Restart()
    {
        Debug.Log("initiate restart");
        StartCoroutine(Launch_level());
    }

    IEnumerator Launch_level()
    {
        SceneManager.LoadScene("dead_scene");
        Debug.Log("waiting for loading");
        yield return new WaitForSeconds(3);
        Debug.Log("trying to restart the game");
        SceneManager.LoadScene("Level_1");
    }
}
