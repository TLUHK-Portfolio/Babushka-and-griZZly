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
            GameObject a = Instantiate(explosion, transform.position, Quaternion.identity);
            Explode();
            Destroy(a, 2f);
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

    void Explode() {
        Collider2D[] inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D cO in inExplosionRadius)
        {
            Rigidbody2D cORigidbody = cO.GetComponent<Rigidbody2D>();

            Debug.Log(cO.name);

            if (cO.name == "Roof 1" || cO.name == "Roof 2")
            {
                GameObject.Find(cO.name).AddComponent<Rigidbody2D>();
            }

            if (cORigidbody != null)
            {
                Vector2 distanceVector = cO.transform.position - transform.position;

                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = power / distanceVector.magnitude;

                    cORigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D col) {
        //
        if (col.gameObject.tag != "Start" && !isPlaying) {
            isPlaying = true;
            
            
            
        }
    }*/
}