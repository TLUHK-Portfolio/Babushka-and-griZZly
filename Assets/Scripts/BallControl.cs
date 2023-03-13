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

    private void Start() {
        //StartCoroutine(Release());
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Vector2 collisionForce = col.relativeVelocity;
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


    IEnumerator Release() {
        yield return new WaitForSeconds(.5f);
        //GetComponent<SpringJoint2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        //enabled = false; // ei saa rohkem liigutada

        yield return new WaitForSeconds(2f);
        //if (nextBall != null) {
        //Instantiate(explosion, transform.position, Quaternion.identity);

        /*var explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }*/

        Destroy(gameObject);
        //nextBall.SetActive(true);
        //Lives--;
        //}
        //else {
        //Enemy.EnemiesAlive = 0;
        //Lives = 3;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
}