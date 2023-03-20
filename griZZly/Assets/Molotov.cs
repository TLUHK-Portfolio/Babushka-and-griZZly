using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    // [SerializeField] private float releaseTime = .15f;
    // [SerializeField] private Rigidbody2D hook;
    // [SerializeField] private GameObject nextBall;
    // [SerializeField] private float maxDragDistance = 2f;
    public ThrowableScript throwableScript;
    private Unit enemyUnit;
    private Unit playerUnit;
    public int damage;
    public float impactRadius = 1.0f; // plahvatuse raadius
    public float fireBurnLength = 4;
    public GameObject firePrefab;
    public GameObject collisionPrefab;
    //private Rigidbody2D rb;
    // private bool _isPressed;
    // public static int Lives = 3;
    // private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();
        playerUnit = GameObject.Find("Player").GetComponent<Unit>();

        throwableScript.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (throwableScript._isFlying)
        {

            //collisionPrefab.transform.localScale = new Vector3(impactRadius, impactRadius, 0);

            //Instantiate(collisionPrefab, gameObject.transform.position, Quaternion.identity);

            for (var flameCount = 0; flameCount < collision.contactCount;flameCount++)
            {
                GameObject fire = Instantiate(firePrefab, collision.GetContact(flameCount).point, Quaternion.identity); // no rotation Quate...
                //fire.transform.SetParent(collision.gameObject.transform);

                Destroy(fire, fireBurnLength);
            }
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
}

// molotovi collider peaks olema eraldi tõmmatavast colliderist vms.