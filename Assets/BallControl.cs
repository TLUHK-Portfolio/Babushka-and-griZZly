using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
    private float angle = 0;
    private bool canRotate = true; 
    private void OnCollisionEnter2D(Collision2D col) {
        Vector2 collisionForce = col.relativeVelocity;
        //Debug.Log(collisionForce.magnitude);
        if (collisionForce.magnitude > 10) {
            canRotate = false;
            //Destroy(this.gameObject);
        }
    }

    private void Update() {
        if (canRotate) {
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            angle += .5f;
        }
    }
}
