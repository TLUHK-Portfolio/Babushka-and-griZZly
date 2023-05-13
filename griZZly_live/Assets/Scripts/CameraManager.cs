using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;
    public CinemachineVirtualCamera VMcam;
    public GameObject player;
    public float PlayerOffset;
    public float EnemyOffset;
    public GameObject enemy;
    public GameObject ammo;

    public float introDuration = 5;
    private bool canChange;

    private float orthographicSize = 15f;
    private CinemachineFramingTransposer transposer;
    float t = 0;
    private bool startMovement = false;


    public void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        transposer = VMcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void OnDestroy() {
        if (GameManager.Instance) {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.Intro) { }
        else if (state == GameState.PlayerTurn) {
            StartCoroutine(showPlayer());
        }
        else if (state == GameState.EnemyTurn) {
            StartCoroutine(showEnemy());
        }
        else if (state == GameState.FallowAmmo1) {
            canChange = true;
            VMcam.Follow = ammo.transform;
            transposer.m_TrackedObjectOffset = new Vector3(0, 0, 0);
        }
        else if (state == GameState.FallowAmmo2) {
            canChange = true;
            VMcam.Follow = GameObject.FindWithTag("Stone").transform;
            transposer.m_TrackedObjectOffset = new Vector3(0, 0, 0);
        }
        else if (state == GameState.Win) {
            VMcam.Follow = GameObject.Find("Mutt").transform;
            transposer.m_TrackedObjectOffset = new Vector3(PlayerOffset, 4, 0);
            VMcam.m_Lens.OrthographicSize = 6f;
        }
        else if (state == GameState.Lose) {
            VMcam.Follow = GameObject.Find("Grizzly").transform;
            transposer.m_TrackedObjectOffset = new Vector3(EnemyOffset, 4, 0);
            VMcam.m_Lens.OrthographicSize = 6f;
        }
    }

    void LateUpdate() {
        if (GameManager.Instance.State == GameState.FallowAmmo1 && VMcam.transform.position.x > 22 && canChange) {
            VMcam.Follow = enemy.transform;
            canChange = false;
        }

        if (GameManager.Instance.State == GameState.FallowAmmo2 && VMcam.transform.position.x < -1 && canChange) {
            VMcam.Follow = player.transform;
            canChange = false;
        }

        if (GameManager.Instance.State == GameState.Intro) {
            StartCoroutine(enableMovement());
            if (startMovement) {
                t += Time.deltaTime / introDuration;
                orthographicSize = Mathf.Lerp(15, 4.5f, t); // camera zoom
                VMcam.m_Lens.OrthographicSize = orthographicSize;
                VMcam.transform.position =
                    Vector3.Lerp(
                        new Vector3(7.9f, 7.3f, -10f),
                        new Vector3(-11.6f, -1.8f, -10f),
                        t); // move to babushka
            }
        }

        IEnumerator enableMovement() {
            yield return new WaitForSeconds(3);
            startMovement = true;
        }
    }
    
    IEnumerator showPlayer() {
        yield return new WaitForSeconds(1);
        VMcam.Follow = player.transform;
        VMcam.m_Lens.OrthographicSize = 4f;
        transposer.m_TrackedObjectOffset = new Vector3(PlayerOffset, 0, 0);
    }

    IEnumerator showEnemy() {
        yield return new WaitForSeconds(1);
        VMcam.Follow = enemy.transform;
        transposer.m_TrackedObjectOffset = new Vector3(EnemyOffset, 0, 0);
    }
}