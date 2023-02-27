using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowableScript : MonoBehaviour
{
    [SerializeField] public float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] public float maxDragDistance = 2f;
    private bool _isPressed;
    public bool _isFlying;
    public int damage;
    private Unit enemyUnit;
    private Unit playerUnit;
    private GameControl gameControl;
    private bool canHurtPlayer = false;
    public static int Lives = 3;
    private bool isPlaying = false;
    private Rigidbody2D rb;
    [SerializeField] public GameObject nextBall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();
        playerUnit = GameObject.Find("Player").GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressed && gameControl.state == BattleState.PLAYERTURN) {
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

     void FixedUpdate()
    {
        if (!_isFlying)
        {
            Physics2D.IgnoreLayerCollision(1, 1);
        } else
        {
            Physics2D.IgnoreLayerCollision(1, 1, false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isFlying)
        {
            Destroy(gameObject, 0);

            if (collision.gameObject.name == "Enemy") {
                enemyUnit.TakeDamage(damage);
            } else if (collision.gameObject.name == "Player") {
                playerUnit.TakeDamage(damage);
            }

            gameControl.EnemyTurn();
        }
    }

    IEnumerator Release() {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        enabled = false; // ei saa rohkem liigutada
        //raske öelda millal täpselt pall üle hooki juba on
        yield return new WaitForSeconds(releaseTime);
        _isFlying = true;

        //siin kutsume välja kaamera jälitamise?

        yield return new WaitForSeconds(2f);
        if (nextBall != null) {
            _isFlying = false;
            Destroy(gameObject);
            nextBall.SetActive(true);
            Lives--;
        }
        else {
            Unit.EnemiesAlive = 0;
            Lives = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
//Instantiate(explosion, transform.position, Quaternion.identity); handlida