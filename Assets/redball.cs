using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class redball : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider redSlider;
    void Start() {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D col) {
        redSlider.value = .75f;
    }

    
    
}
