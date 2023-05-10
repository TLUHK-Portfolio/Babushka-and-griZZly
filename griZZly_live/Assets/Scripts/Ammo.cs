using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ammo : MonoBehaviour {
    public bool collided;
    public float rotation = 0;
    public GameObject splash;
    public float waitUntilAirBorne = 0.5f;
    public bool destroyOnCollision;


    private bool canRotate = false;
    private bool isSplashCreated = false;
    private float angle = 0;
    private AudioSource source;
    private float collisionCheckRadius = .5f;
    private LineRenderer lr; // projectile
    private GameObject ropeObject;
    private Rope ropeScript;

    private void Awake() {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Mutt").GetComponent<Collider2D>(),
            true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("PauseButton").GetComponent<Collider2D>(),
            true);
        lr = GetComponent<LineRenderer>();
        if (lr) lr.startColor = Color.white;
    }

    private void Start() {
        source = GetComponent<AudioSource>();
        ropeObject = GameObject.FindGameObjectWithTag("Rope");
        ropeScript = ropeObject.GetComponent<Rope>();
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
        if (gameObject.GetComponent<BoxCollider2D>()) {
            // moosipurk
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (gameObject.GetComponent<PolygonCollider2D>()) {
            // kivi
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(gameObject.name + " collided with " + collision.collider.name);
        collided = true;
        canRotate = false;
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
        yield return new WaitForSeconds(destroyOnCollision ? .5f : 3f);
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
        else if (lr && ropeScript.ammoForce2.magnitude > 0 && !collided) {
            lr.positionCount = 0;
            if (PlayerPrefs.GetInt("ShootingAid", 1) == 1 && SceneManager.GetActiveScene().buildIndex<2) {
                SimulateArc();
            }
        }
    }

    private void SimulateArc() {
        float simulateForDuration = 5f; //simulate for 5 secs in the future
        float simulationStep = 0.05f; //Will add a point every 0.1 secs.

        int steps = (int)(simulateForDuration / simulationStep); //50 in this example
        //lr.positionCount = 0;
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = ropeScript.ammoDirection;
        Vector2 launchPosition = transform.position; //Position where you launch from
        float launchSpeed = ropeScript.ammoForce2.magnitude * (float)Math.Sqrt(2);

        for (int i = 0; i < steps-1; ++i) {
            calculatedPosition = launchPosition + (directionVector * (launchSpeed * i * simulationStep));
            //Calculate gravity
            calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep) * (i * simulationStep);
            lineRendererPoints.Add(calculatedPosition);
            if (CheckForCollision(calculatedPosition)) //if you hit something
            {
                break; //stop adding positions
            }
            lr.positionCount += 1;
            lr.SetPosition(i, calculatedPosition);
        }
    }

    private bool CheckForCollision(Vector2 position) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, collisionCheckRadius);
        if (hits.Length > 0) {
            //We hit something 
            //check if its a wall or something
            //if its a valid hit then return true
            for (int x = 0; x < hits.Length; x++) {
                if (hits[x].tag == "TakistusObject") {
                    // karukoobas
                    lr.startColor = Color.yellow;
                    return true;
                } if (hits[x].tag == "Grizzly") {
                    lr.startColor = Color.green;
                    return true;
                }
                else {
                    lr.startColor = Color.white;
                }
                
            }
        }

        return false;
    }
}