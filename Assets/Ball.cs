using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour {
    [SerializeField] private float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject nextBall;
    [FormerlySerializedAs("maxDistance")] [SerializeField] private float maxDragDistance = 2f;
    public Rigidbody2D rb;
    private bool _isPressed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_isPressed) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance) { // max kaugus
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            } else 
            rb.position = mousePos;
        }
    }

    void OnMouseDown() {
        _isPressed = true;
        rb.isKinematic = true; // vedru ei mõju enam
    }

    private void OnMouseUp() {
        _isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }

    IEnumerator Release() {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        enabled = false; // ei saa rohkem liigutada

        yield return new WaitForSeconds(2f);
        if (nextBall != null) {
            nextBall.SetActive(true);    
        }
        else {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
        }
         
    }
}