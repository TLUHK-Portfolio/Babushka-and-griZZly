using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;
    public CinemachineVirtualCamera VMcam;
    public GameObject player;
    public GameObject enemy;
    public GameObject ammo;

    public void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    public void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.PlayerTurn) {
            VMcam.Follow = player.transform;    
        } else if (state == GameState.EnemyTurn) {
            VMcam.Follow = enemy.transform;    
        } else if (state == GameState.FallowAmmo1) {
            VMcam.Follow = ammo.transform;
        } else if (state == GameState.FallowAmmo2) {
            Debug.Log(ammo);
            if (ammo) VMcam.Follow = ammo.transform;
        } 
    }
    
    void LateUpdate()
    {
        // Lerp toward target position at all times.
        //smoothPosition = Vector3.Lerp(  VMcam.transform.position, targetPosition, smoothSpeed);
        //VMcam.transform.position = smoothPosition;
 
        // Lerp toward target rotation at all times.
        //smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        //transform.rotation = smoothRotation;
    }
}