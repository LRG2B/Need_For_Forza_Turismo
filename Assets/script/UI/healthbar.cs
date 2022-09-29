using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public dead_manager manager;

    private void Awake()
    {
        slider.maxValue = 1000;
        slider.value = 1000;
    }

    public void set_health(int health)
    {
        slider.value = health;
        if (slider.value < 0)
            manager.Restart();
    }
}
