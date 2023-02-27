using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Slider hpSlider;
    
    public void setMaxHP(int health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void setHP(int health)
    {
        Debug.Log("setting hp to " + health);
        hpSlider.value = health;
    }
}
