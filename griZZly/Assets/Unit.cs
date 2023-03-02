using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour {
    public string unitName;
    public HealthScript healthScript;
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    public bool isDead = false;
    [SerializeField] public GameObject explosion;
    public static int EnemiesAlive = 0;
    private GameControl gameControl;

    private void Start() {
        healthScript.setMaxHP(maxHealth);
        EnemiesAlive++;
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    private void Update()
    {
        
    }

    public void EnemyDoSomething()
    {
        StartCoroutine(Sleep());
        GameObject.Find("Enemy").GetComponent<Unit>().transform.localScale += new Vector3(0.2f,0.2f);
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(3);
    }

    void OnCollisionEnter2D(Collision2D colInfo) {
        if (colInfo.relativeVelocity.magnitude > health) {
            Diy();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("took dmg: " + damage);
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        } else 
        {
            isDead = false;
        }

        healthScript.setHP(health);
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
