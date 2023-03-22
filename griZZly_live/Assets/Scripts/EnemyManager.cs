using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    public GameObject AmmoPrefab;
    public float force;
    public Vector3 LaunchPosition;
    
    Rigidbody2D ammo;
    Collider2D ammoCollider;
    
    private void CreateAmmo() {
        ammo = Instantiate(AmmoPrefab).GetComponent<Rigidbody2D>();
        ammoCollider = ammo.GetComponent<Collider2D>();
        ammo.isKinematic = true;
    }

    void Shoot() {
        ammo.isKinematic = false;
        ammo.velocity = LaunchPosition * force * -1;
        ammo.GetComponent<Ammo>().Release();
        ammo = null;
        ammoCollider = null;
    }

}