using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public Slider HealthBar;
    public float damageFlashTime = .45f;
    private Color origColor;

    private void Start() {
        foreach (Transform child in gameObject.transform) {
            if (child.GetComponent<MeshRenderer>()) {
                Debug.Log(child.GetComponent<MeshRenderer>().material.color);
                origColor = child.GetComponent<MeshRenderer>().material.color;
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(gameObject.name + " collided with " + col.collider.name);
        IndicateDamage();

        HealthBar.value -= .1f;
        if (HealthBar.value <= 0) {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    private void IndicateDamage() {
        foreach (Transform child in gameObject.transform) {
            if (child.GetComponent<MeshRenderer>()) {
                child.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        Invoke("ResetMesh", damageFlashTime);
    }

    private void ResetMesh() {
        foreach (Transform child in gameObject.transform) {
            if (child.GetComponent<MeshRenderer>()) {
                child.GetComponent<MeshRenderer>().material.color = origColor;
            }
        }
    }
    
}