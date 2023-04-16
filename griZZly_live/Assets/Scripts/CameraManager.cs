using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;
    public CinemachineVirtualCamera VMcam;
    public GameObject player;
    public GameObject enemy;
    public GameObject ammo;
    
    public float introDuration = 5;
    private bool canChange;

    private float orthographicSize = 15f;
    float t = 0;
    private bool startMovement = false;
    
    
    public void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    public void OnDestroy() {
        if (GameManager.Instance)
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.Intro) {

        } else if (state == GameState.PlayerTurn) {
            VMcam.Follow = player.transform;    
        } else if (state == GameState.EnemyTurn) {
            VMcam.Follow = enemy.transform;    
        } else if (state == GameState.FallowAmmo1) {
            canChange = true;
            VMcam.Follow = ammo.transform;
        } else if (state == GameState.FallowAmmo2) {
            canChange = true;
            VMcam.Follow = GameObject.FindWithTag("Stone").transform;
        } 
    }
    
    void LateUpdate()
    {
        if (GameManager.Instance.State == GameState.FallowAmmo1 && VMcam.transform.position.x > 22 && canChange) {
            VMcam.Follow = enemy.transform;
            canChange = false;
        }
        
        if (GameManager.Instance.State == GameState.FallowAmmo2 && VMcam.transform.position.x < -1 && canChange) {
            VMcam.Follow = player.transform;
            canChange = false;
        }

        if (GameManager.Instance.State == GameState.Intro ) {
            StartCoroutine(enableMovement());
            if (startMovement) {
                t += Time.deltaTime / introDuration;
                orthographicSize = Mathf.Lerp(15, 5, t); // camera zoom
                VMcam.m_Lens.OrthographicSize = orthographicSize;
                VMcam.transform.position =
                    Vector3.Lerp(
                        new Vector3(7.9f, 7.3f, -10f), 
                        new Vector3(-11.6f, -1.4f, -10f), 
                        t); // move to babushka

                // todo näita kuidas moosiga visata
                if (orthographicSize < 5.05) {
                    //GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
                }
            }
        }
        
        IEnumerator enableMovement()
        {
            yield return new WaitForSeconds(3);
            startMovement = true;
        }

    }
}
