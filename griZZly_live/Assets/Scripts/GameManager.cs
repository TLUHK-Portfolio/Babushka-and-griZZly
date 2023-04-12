using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState State;
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
            }
            else {
                ResumeGame();
            }
        }
    }

    public void PauseGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
        }
        else {
            ResumeGame();
        }
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
                StartCoroutine(showMainMenu());
                break;
            case GameState.Lose:
                text.text = "You lose :(";
                StartCoroutine(showMainMenu());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    IEnumerator EnemyAttacks() {
        yield return new WaitForSeconds(2);
        UpdateGameState(GameState.FallowAmmo2);
    }

    IEnumerator showMainMenu() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);        
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