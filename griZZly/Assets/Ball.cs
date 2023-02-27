using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour {
    [SerializeField] private GameObject explosion;
    public ThrowableScript throwableScript;
    public float radius = 5.0F; // plahvatuse raadius
    public float power = 10.0F; // plahvatuse võimsus

    private void Start()
    {

    }

    private void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (throwableScript._isFlying)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        /*var explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }*/
    }

    /*private void OnCollisionEnter2D(Collision2D col) {
        //
        if (col.gameObject.tag != "Start" && !isPlaying) {
            isPlaying = true;
            
            
            
        }
    }*/
}