using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;
    public CinemachineVirtualCamera VMcam;
    public GameObject player;
    public GameObject enemy;
    public GameObject ammo;
    private bool canChange;

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
        if (state == GameState.PlayerTurn) {
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


        // Lerp toward target position at all times.
        //smoothPosition = Vector3.Lerp(  VMcam.transform.position, targetPosition, smoothSpeed);
        //VMcam.transform.position = smoothPosition;

        // Lerp toward target rotation at all times.
        //smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        //transform.rotation = smoothRotation;
    }
}
