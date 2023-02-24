using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour {
    [SerializeField] private float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject nextBall;
    [SerializeField] private float maxDragDistance = 2f;
    [SerializeField] private GameObject explosion;

    public float radius = 5.0F; // plahvatuse raadius
    public float power = 10.0F; // plahvatuse võimsus
    private Rigidbody2D rb;
    private bool _isPressed;
    public static int Lives = 3;
    private bool isPlaying = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_isPressed) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance) {
                // max kaugus
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else
                rb.position = mousePos;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D col) {
        //
        if (col.gameObject.tag != "Start" && !isPlaying) {
            isPlaying = true;
            
            
            
        }
    }*/

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
            Instantiate(explosion, transform.position, Quaternion.identity);

            /*var explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            Debug.Log(colliders.Length);
            foreach (Collider hit in colliders) {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }*/

            Destroy(gameObject);
            nextBall.SetActive(true);
            Lives--;
        }
        else {
            Enemy.EnemiesAlive = 0;
            Lives = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}