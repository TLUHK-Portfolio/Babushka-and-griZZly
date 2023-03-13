using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    [SerializeField] private float health = 4f;
    [SerializeField] public GameObject explosion;
    public static int EnemiesAlive = 0;

    private void Start() {
        EnemiesAlive++;
    }

    void OnCollisionEnter2D(Collision2D colInfo) {
        if (colInfo.relativeVelocity.magnitude > health) {
            Diy();
        }
    }

    private void Diy() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        EnemiesAlive--;
        if (EnemiesAlive <= 0) {
            //SceneManager.
            Debug.Log("You WON!");
        }
        Destroy(gameObject);
        
    }
}
