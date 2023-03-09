using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
    public float rotation = 0;
    public bool canRotate = false;
    public GameObject splash;
    private bool isSplashCreated;
    private float angle = 0;

    private void OnCollisionEnter2D(Collision2D col) {
        Vector2 collisionForce = col.relativeVelocity;
        //Debug.Log(collisionForce.magnitude);
        //Debug.Log(col.gameObject.tag);
        if (collisionForce.magnitude > 5 && !col.gameObject.CompareTag("Player")) {
            canRotate = false;
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
        angle += this.rotation;
    }
}