using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class redball : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider Slider;
    void Start() {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "red_ball")
        Slider.value -= .2f;
    }

    
    
}
