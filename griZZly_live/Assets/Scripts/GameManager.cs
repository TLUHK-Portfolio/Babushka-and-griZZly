using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    //public GameState State;
    public static Action<GameState> OnGameStateChanged;
    public TMP_Text text;
    public GameObject pauseMenu;
    private Boolean gamePaused = false;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.PlayerTurn);
    }

    private void Update() {
        //kui vajutada mängus escape'i
        if (Input.GetKeyDown(KeyCode.Escape)) {
            //kui menüü oli juba aktiivne, sulgeme selle
            if (pauseMenu.activeSelf) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }

    public void PauseGame() {
        pauseMenu.SetActive(false);

        gamePaused = false;
    }

    public void ResumeGame() {
        pauseMenu.SetActive(true);

        gamePaused = true;
    }

    public void ToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void PauseHandler() {
        if (gamePaused) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }


    public void UpdateGameState(GameState newState) {
        //State = newState;

        switch (newState) {
            case GameState.PlayerTurn:
                text.text = "Babushka kord";
                break;
            case GameState.FallowAmmo1:
                text.text = "Babushka ründab";
                break;
            case GameState.FallowAmmo2:
                text.text = "GriZZly ründab";
                break;
            case GameState.EnemyTurn:
                text.text = "GriZZly kord";
                StartCoroutine(EnemyAttacks());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    IEnumerator EnemyAttacks() {
        yield return new WaitForSeconds(3);
        UpdateGameState(GameState.FallowAmmo2);
    }
}

public enum GameState {
    PlayerTurn,
    FallowAmmo1,
    EnemyTurn,
    FallowAmmo2,
}