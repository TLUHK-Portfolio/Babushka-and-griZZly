using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bomb : MonoBehaviour {
    [SerializeField] private GameObject explosion;
    public ThrowableScript throwableScript;
    private GameObject enemyGameObject;
    public float radius = 5.0F; // plahvatuse raadius
    public float power = 10.0F; // plahvatuse võimsus

    private void Start()
    {
        enemyGameObject = GameObject.Find("Enemy");
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

            if (cO.name == "Roof 1" || cO.name == "Roof 2")
            {
                GameObject.Find(cO.name).AddComponent<Rigidbody2D>();
            }

            //kui vastane oli raadiuses
            if (cO.name == "Enemy") {
                float proximity = (transform.position - enemyGameObject.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                enemyGameObject.GetComponent<Unit>().TakeDamage((int)Mathf.Round(throwableScript.damage * effect));
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