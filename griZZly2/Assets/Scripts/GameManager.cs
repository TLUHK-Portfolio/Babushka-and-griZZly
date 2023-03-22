using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState State;
    public static Action<GameState> OnGameStateChanged;
    public TMP_Text text;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.PlayerTurn);
    }


    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.PlayerTurn:
                text.text = "Babushka turn";
                break;
            case GameState.FallowAmmo1:
                text.text = "Babushka attacks";
                break;
            case GameState.FallowAmmo2:
                text.text = "GriZZly attacks";
                break;
            case GameState.EnemyTurn:
                text.text = "GriZZly turn";
                StartCoroutine(EnemyAttacks());
                break;
            case GameState.Win:
                text.text = "You WIN!!!";
                break;
            case GameState.Lose:
                text.text = "You lose :(";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        Debug.Log(newState);
        OnGameStateChanged?.Invoke(newState);
    }

    IEnumerator EnemyAttacks() {
        yield return new WaitForSeconds(3);
        UpdateGameState(GameState.FallowAmmo2);
        //yield return new WaitForSeconds(3);
        //UpdateGameState(GameState.PlayerTurn);
    }
}

public enum GameState {
    PlayerTurn,
    FallowAmmo1,
    EnemyTurn,
    FallowAmmo2,
    Win,
    Lose
}