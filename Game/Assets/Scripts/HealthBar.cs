using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This script is used to set the health/stamina bars
 */

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    //sets the max health
    public void SetMax(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    //sets current health
    public void Set(int health)
    {
        slider.value = health;
    }
}
