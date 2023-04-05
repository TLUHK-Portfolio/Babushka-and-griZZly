using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Slider HealthBar;
    public float damageFlashTime = .45f;
    private Color origColor;
    private SpriteRenderer playerSpriteRenderer;

    private void Start() {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        origColor = playerSpriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(gameObject.name + " collided with " + col.collider.name);
        IndicateDamage();

        HealthBar.value -= .1f;
        if (HealthBar.value <= 0) {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

     private void IndicateDamage()
    {

        playerSpriteRenderer.color = Color.red;

        Invoke("ResetMesh", damageFlashTime);
    }

    private void ResetMesh()
    {
        playerSpriteRenderer.color = origColor;
    }
}
