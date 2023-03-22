﻿using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public bool collided;
    public float rotation = 0;
    public GameObject splash;

    private bool canRotate = false;
    private bool isSplashCreated = false;
    private float angle = 0;

    public void Release() {
        CameraManager.Instance.ammo = gameObject;
        GameManager.Instance.UpdateGameState(GameState.FallowAmmo1);
        canRotate = true;
        PathPoints.instance.Clear();
        StartCoroutine(CreatePathPoints());
    }

    IEnumerator CreatePathPoints() {
        while (true) {
            if (collided) break;
            PathPoints.instance.CreateCurrentPathPoint(transform.position);
            yield return new WaitForSeconds(PathPoints.instance.timeInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        collided = true;
        canRotate = false;
        Destroy(gameObject, 5f);
        if (!isSplashCreated) {
            Instantiate(splash, transform.position, Quaternion.identity);
            isSplashCreated = true;
            GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
            //GameObject.Find("CameraControl").GetComponent<CameraControl>().pointToEnemy();
        }
    }

    private void Update() {
        if (!canRotate) return;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        angle += this.rotation;
    }
}