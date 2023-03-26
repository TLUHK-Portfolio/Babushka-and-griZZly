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
        ammo.transform.position = LaunchPosition;
        ammo.isKinematic = true;
    }

    void Shoot() {
        ammo.isKinematic = false;
        Quaternion Rotation = Quaternion.Euler( 0, 0, 45f);
        ammo.AddForce(Rotation * Vector2.up * force, ForceMode2D.Impulse);
        //CameraManager.Instance.ammo = AmmoPrefab;
        ammo.GetComponent<Ammo>().Release();
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