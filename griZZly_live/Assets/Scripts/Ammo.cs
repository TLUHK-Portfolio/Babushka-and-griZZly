using System;
using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public bool collided;
    public float rotation = 0;
    public GameObject splash;
    public float waitUntilAirBorne = 0.5f;

    private bool canRotate = false;
    private bool isSplashCreated = false;
    private float angle = 0;
    private AudioSource source;

    private void Awake() {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Mutt").GetComponent<Collider2D>(),
            true);
    }

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    public void Release() {
        CameraManager.Instance.ammo = gameObject;
        if (GameManager.Instance.State == GameState.PlayerTurn) {
            GameManager.Instance.UpdateGameState(GameState.FallowAmmo1);
        }
        else if (GameManager.Instance.State == GameState.EnemyTurn) {
            GameManager.Instance.UpdateGameState(GameState.FallowAmmo2);
        }

        canRotate = true;
        PathPoints.instance.Clear();
        
        StartCoroutine(CreatePathPoints());
        StartCoroutine(EnableCollider());
        StartCoroutine(EnableColliderMutiga());
    }

    IEnumerator EnableColliderMutiga() {
        yield return new WaitForSeconds(waitUntilAirBorne);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Mutt").GetComponent<Collider2D>(),
            false);
    }

    IEnumerator CreatePathPoints() {
        while (true) {
            if (collided) break;
            PathPoints.instance.CreateCurrentPathPoint(transform.position);
            yield return new WaitForSeconds(PathPoints.instance.timeInterval);
        }
    }

    IEnumerator EnableCollider() {
        yield return new WaitForSeconds(.5f);
        if (gameObject.GetComponent<BoxCollider2D>()) { // moosipurk
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        } else if (gameObject.GetComponent<PolygonCollider2D>()) { // kivi
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(gameObject.name + " collided with " + collision.collider.name);
        collided = true;
        canRotate = false;
        // Destroy(gameObject, 3f);
        if (!isSplashCreated) {
            if (splash != null) {
                Instantiate(splash, transform.position, Quaternion.identity);
            }
            isSplashCreated = true;
            source.Play();
        }

        StartCoroutine(NextStop());
    }

    IEnumerator NextStop() {
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);

        Destroy(gameObject);

        switch (GameManager.Instance.State) {
            case GameState.FallowAmmo1:
                GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
                break;
            case GameState.FallowAmmo2:
                GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
                break;
        }
    }

    private void Update() {
        if (canRotate) {
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            angle += this.rotation;
        }
    }

    private void OnDestroy() {
        // switch (GameManager.Instance.State)
        // {
        //     case GameState.FallowAmmo1:
        //         GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
        //         break;
        //     case GameState.FallowAmmo2:
        //         GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        //         break;
        // }
    }
}