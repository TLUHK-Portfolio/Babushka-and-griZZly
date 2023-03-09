using System;
using UnityEngine.UI;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public Slider Slider;
    
    private void OnCollisionEnter2D(Collision2D col) {
            Slider.value -= .2f;
    }
}
