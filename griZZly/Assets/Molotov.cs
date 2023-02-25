using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Molotov : MonoBehaviour
{
    [SerializeField] private float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject nextBall;
    [SerializeField] private float maxDragDistance = 2f;
    [SerializeField] private GameObject explosion;

    public float impactRadius = 1.0f; // plahvatuse raadius
    public float fireBurnLength = 4;
    public GameObject firePrefab;
    public GameObject collisionPrefab;
    public float power = 10.0F; // plahvatuse võimsus
    private Rigidbody2D rb;
    private bool _isPressed;
    public static int Lives = 3;
    private bool isPlaying = false;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0);

        collisionPrefab.transform.localScale = new Vector3(impactRadius, impactRadius, 0);

        //Instantiate(collisionPrefab, gameObject.transform.position, Quaternion.identity);

        for (var flameCount = 0; flameCount < collision.contactCount;flameCount++)
        {
            GameObject fire = Instantiate(firePrefab, collision.GetContact(flameCount).point, Quaternion.identity); // no rotation Quate...
            fire.transform.SetParent(collision.gameObject.transform);
            rb.isKinematic = false;

            Destroy(fire, fireBurnLength);
        }

        // genereerime leeke seni punktidele, kuni üks neist satub õigele objektile, et kuvada
        // vaatame mis on uue onjekti ja olemasolevate ühisosa ja genereerime neisse suvaliselt genereeritud punktidesse leegi

        // for (var flameCount = 0; flameCount < 10;flameCount++) {
        //     // jagame kahega, sest collisioni keskpunktist võetakse maha raadius
        //     Vector3 randomPoint = new Vector3(
        //         Random.Range(transform.position.x - impactRadius/2, transform.position.x + impactRadius/2),
        //         Random.Range(transform.position.y - impactRadius/2, transform.position.y + impactRadius/2),
        //         0
        //     );

        //     Collider[] cols = Physics.OverlapSphere(randomPoint, impactRadius*2);
        //     Debug.Log(cols.Length);
        //     Debug.Log(randomPoint.ToString());
        //     if (cols.Length != 0) {
        //         Instantiate(firePrefab, randomPoint, Quaternion.identity); // no rotation Quate...
                
        //     }
        // }
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

// molotovi collider peaks olema eraldi tõmmatavast colliderist vms.
// throwable stuff võiks olla üldise klassi all ja teda extendime erivõimete jaoks