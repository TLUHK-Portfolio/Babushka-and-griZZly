
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    
    public GameObject AmmoPrefab;
    public float force;
    public Vector3 LaunchPosition;
    public Slider HealthBar;
    
    Rigidbody2D ammo;
    
    public void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void CreateAmmo() {
        ammo = Instantiate(AmmoPrefab).GetComponent<Rigidbody2D>();
        ammo.isKinematic = true;
        CameraManager.Instance.ammo = AmmoPrefab;
    }

    void Shoot() {
        ammo.isKinematic = false;
        ammo.velocity = LaunchPosition * force * -1;
        ammo.GetComponent<Ammo>().Release();
        
        //ammo = null;
        //ammoCollider = null;
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.EnemyTurn) {
            CreateAmmo();
        } else if (state == GameState.FallowAmmo2) {
            Shoot();
        } 
    }

    private void OnCollisionEnter2D(Collision2D col) {
        HealthBar.value -= .1f;
        if (HealthBar.value <= 0) {
            GameManager.Instance.UpdateGameState(GameState.Win);
        }
    }
}