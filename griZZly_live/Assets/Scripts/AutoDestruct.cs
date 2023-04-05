using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoDestruct : MonoBehaviour {
    public float Resistance;
    public ParticleSystem explosion;
    
    private static float lives;

    private void Start() {
        lives = Resistance;
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D col) {
        ContactPoint2D[] contacts = new ContactPoint2D[col.contactCount];
        col.GetContacts(contacts);
        float totalImpulse = 0;
        foreach (ContactPoint2D contact in contacts) {
            totalImpulse += contact.normalImpulse * .05f;
        }
        lives -= totalImpulse;
        if (lives < 0) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject,1f);
        }
        
    }
}
