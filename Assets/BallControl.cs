using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
    private float angle = 0;
    public bool canRotate = false;
    public GameObject splash;
    private bool isSplashCreated;
    
    private void OnCollisionEnter2D(Collision2D col) {
        Vector2 collisionForce = col.relativeVelocity;
        //Debug.Log(collisionForce.magnitude);
        //Debug.Log(col.gameObject.tag);
        if (collisionForce.magnitude > 5 && !col.gameObject.CompareTag("Player")) {
            canRotate = false;
            //Destroy(this.gameObject);
        }

        if (col.gameObject.CompareTag("ground") && !isSplashCreated) {
            Instantiate(splash, transform.position, Quaternion.identity);
            isSplashCreated = true;
        }
    }

    private void Update() {
        if (!canRotate) return;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        angle += 1f;
    }
}
