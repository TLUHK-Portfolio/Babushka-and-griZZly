using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static Action<GameState> OnGameStateChanged;
    public TMP_Text text;
    public GameObject pauseMenu;
    GameState lastGameState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.PlayerTurn);
    }

    private void Update()
    {
        //kui vajutada mängus escape'i
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseHandler();
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
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
                PlayerPrefs.SetInt("LevelOneCompleted", 1);
                break;
            case GameState.Lose:
                text.text = "You lose :(";
                StartCoroutine(showMainMenu());
                break;
            case GameState.Pause:
                text.text = "Game paused";
                showPause();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void showPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void disablePause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        UpdateGameState(lastGameState);
    }

    IEnumerator EnemyAttacks()
    {
        yield return new WaitForSeconds(2);
        UpdateGameState(GameState.FallowAmmo2);
    }

    IEnumerator showMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PauseGame()
    {
        UpdateGameState(GameState.Pause);
    }

    public void PauseHandler()
    {
        // kui pausimenüü on hetkel ees
        if (GameManager.Instance.State == GameState.Pause)
        {
            disablePause();
        }
        else
        {
            lastGameState = GameManager.Instance.State;
            UpdateGameState(GameState.Pause);
        }
    }
}

public enum GameState
{
    PlayerTurn,
    FallowAmmo1,
    EnemyTurn,
    FallowAmmo2,
    Win,
    Lose,
    Pause
}