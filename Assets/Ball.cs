using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour {
    [SerializeField] private float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject nextBall;
    [SerializeField] private float maxDragDistance = 2f;
    public Rigidbody2D rb;
    private bool _isPressed;
    public static int Lives = 3;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        //GameObject test = GameObject.Find("Canvas");
        //Canvas can = test.GetComponent<Canvas>();
        //Debug.Log(can.GetComponent<Image>());
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
            Lives--;
        }
        else {
            Enemy.EnemiesAlive = 0;
            Lives = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
         
    }
}