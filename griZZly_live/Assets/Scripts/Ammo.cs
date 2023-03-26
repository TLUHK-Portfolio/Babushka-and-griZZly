using System;
using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public bool collided;
    public float rotation = 0;
    public GameObject splash;
    public float waitUntilAirBorne = 0.5f;

    private bool canRotate = false;
    private bool isSplashCreated = false;
    private float angle = 0;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("mutt_0").GetComponent<Collider2D>(), true);
    }

    public void Release()
    {
        CameraManager.Instance.ammo = gameObject;
        if (GameManager.Instance.State == GameState.PlayerTurn)
        {
            GameManager.Instance.UpdateGameState(GameState.FallowAmmo1);
        }
        else if (GameManager.Instance.State == GameState.EnemyTurn)
        {
            GameManager.Instance.UpdateGameState(GameState.FallowAmmo2);
        }

        canRotate = true;
        PathPoints.instance.Clear();
        StartCoroutine(CreatePathPoints());

       StartCoroutine(EnableColliderMutiga());
    }

    IEnumerator EnableColliderMutiga()
    {
        yield return new WaitForSeconds(waitUntilAirBorne);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("mutt_0").GetComponent<Collider2D>(), false);
    }

    IEnumerator CreatePathPoints()
    {
        while (true)
        {
            if (collided) break;
            PathPoints.instance.CreateCurrentPathPoint(transform.position);
            yield return new WaitForSeconds(PathPoints.instance.timeInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        canRotate = false;
        Destroy(gameObject, 3f);
        if (!isSplashCreated)
        {
            if (splash != null) {
                Instantiate(splash, transform.position, Quaternion.identity);
                isSplashCreated = true;
            }
        }
    }

    private void Update()
    {
        if (canRotate)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            angle += this.rotation;
        }
    }

    private void OnDestroy()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.FallowAmmo1:
                GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
                break;
            case GameState.FallowAmmo2:
                GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
                break;
        }
    }
}