using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowableScript : MonoBehaviour
{
    [SerializeField] public float releaseTime = .15f;
    [SerializeField] public Rigidbody2D hook;
    [SerializeField] public float maxDragDistance = 2f;
    public bool _isPressed;
    public string asd = "as";
    public static int Lives = 3;
    public bool isPlaying = false;
    public Rigidbody2D rb;
    [SerializeField] public GameObject nextBall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            //Instantiate(explosion, transform.position, Quaternion.identity);

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
//Instantiate(explosion, transform.position, Quaternion.identity); handlida