using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour {
    public GameObject AmmoPrefab;
    public float force;
    public GameObject LaunchPosition;
    public Slider HealthBar;

    private Animator animator;
    private bool onShootingAction;
    Rigidbody2D ammo;

    public void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        animator = gameObject.GetComponent<Animator>();
    }

    private void CreateAmmo() {
        ammo = Instantiate(AmmoPrefab).GetComponent<Rigidbody2D>();
        ammo.transform.position = LaunchPosition.transform.position;
        ammo.isKinematic = true;
    }

    void Shoot() {
        onShootingAction = false;
        float ai = Random.Range(.5f, 1.5f);
        Quaternion Rotation = Quaternion.Euler(0, 0, 45f * ai);
        ammo.isKinematic = false;
        Vector2 f = Rotation * Vector2.up * force;
        Debug.Log(f);
        ammo.AddForce(f, ForceMode2D.Impulse);
        ammo.GetComponent<Ammo>().Release();
        animator.SetBool("viska", false);
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.EnemyTurn) {
            CreateAmmo();
        }
        else if (state == GameState.FallowAmmo2) {
            //Shoot();
            onShootingAction = true;
            animator.SetBool("viska", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag != "Stone") {
            ContactPoint2D[] contacts = new ContactPoint2D[col.contactCount];
            col.GetContacts(contacts);
            float totalImpulse = 0;
            foreach (ContactPoint2D contact in contacts) {
                totalImpulse += contact.normalImpulse * .05f;
            }
            HealthBar.value -= totalImpulse;
            if (HealthBar.value <= 0) {
                GameManager.Instance.UpdateGameState(GameState.Win);
            }
        }
    }

    public void Update() {
        if (ammo && onShootingAction) {
            ammo.transform.position = LaunchPosition.transform.position;
        }
    }
}