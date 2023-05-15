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
    public GameObject settingsMenu;
    GameState lastGameState;
    public GameObject fireworks;
    public GameObject winMenu;
    public GameObject loseMenu;
    public AudioSource victorySound;
    public AudioSource loseSound;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        var showIntro = PlayerPrefs.GetInt("FirstGame", 1);
        if (showIntro == 1) {
            UpdateGameState(GameState.Intro);
            PlayerPrefs.SetInt("FirstGame", 0);
        }
        else {
            UpdateGameState(GameState.PlayerTurn);
        }
    }

    private void Update() {
        //kui vajutada mängus escape'i
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseHandler();
        }

        // loeme x sekundit ja siis kontrollime uuesti kas visatav objekt on tekkinud
        // kui pole tekkinud, siis muudame kelle käik on
        checkIfGameStuck();
    }

    private void checkIfGameStuck()
    {
        GameState state = GameManager.Instance.State;

        if (state == GameState.PlayerTurn || state == GameState.Lose || state == GameState.Win || state == GameState.Intro || state == GameState.Pause) return;

        StartCoroutine(changeTurn(state));
    }

    IEnumerator changeTurn(GameState state)
    {
        yield return new WaitForSeconds(6);

        GameState newState = GameManager.Instance.State;

        if (state == newState)
        {
            Debug.Log("Game stuck, changing to player's turn");
            UpdateGameState(GameState.PlayerTurn);
        }
    }

    public void ToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void ToSettingsMenu() {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ToPauseMenu() {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            case GameState.Intro:
                break;
            case GameState.PlayerTurn:
                text.text = "Babushka's turn";
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
                // StartCoroutine(showMainMenu());
                OpenWinMenu();
                // particle käitub 3D maailmas
                Instantiate(fireworks, new Vector3(-11, -4.2f, 0), Quaternion.Euler(-100, 0, 0));
                PlayerPrefs.SetInt("LevelOneCompleted", 1);
                PathPoints.instance.Clear();
                victorySound.Play();
                break;
            case GameState.Lose:
                text.text = "You lose :(";
                OpenLoseMenu();
                // StartCoroutine(showMainMenu());
                PathPoints.instance.Clear();
                loseSound.Play();
                break;
            case GameState.Pause:
                text.text = "Game paused";
                showPause();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        Debug.Log("Game state changed to: " + newState);

        OnGameStateChanged?.Invoke(newState);
    }

    private void OpenLoseMenu()
    {
        loseMenu.SetActive(true);
    }

    private void OpenWinMenu()
    {
        winMenu.SetActive(true);
        // Time.timeScale = 0;
    }

    public void ToLevelTwo() {
        SceneManager.LoadScene("Level2");
    }

    public void ToLevelOne() {
        SceneManager.LoadScene("Level1");
    }

    public void showPause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void disablePause() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        UpdateGameState(lastGameState);
    }

    IEnumerator EnemyAttacks() {
        yield return new WaitForSeconds(2);
        UpdateGameState(GameState.FallowAmmo2);
    }

    IEnumerator showMainMenu() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PauseHandler() {
        // kui pausimenüü on hetkel ees
        if (State == GameState.Pause) {
            disablePause();
        }
        else {
            lastGameState = State;
            UpdateGameState(GameState.Pause);
        }
    }
}

public enum GameState {
    Intro,
    PlayerTurn,
    FallowAmmo1,
    EnemyTurn,
    FallowAmmo2,
    Win,
    Lose,
    Pause
}