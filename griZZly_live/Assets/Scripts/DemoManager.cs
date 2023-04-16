using System;
using System.Collections;
using UnityEngine;

public class DemoManager : MonoBehaviour {
    public GameObject demoAmmo;
    public Transform ammoStartPoint;
    public GameObject demoMouse;
    public float introDuration = 5;
    private Animator mouseAnimator;
    private bool startMovement = false;
    private bool ammoCreated = false;
    private Vector3 mousePos;
    float t = 0;
    private GameObject ammo;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void Start() {
        demoMouse.GetComponent<Animator>().StopPlayback();
        mouseAnimator = demoMouse.GetComponent<Animator>();
    }

    void LateUpdate() {
        if (startMovement) {
            if (!ammoCreated) {
                t += Time.deltaTime / introDuration;
                demoMouse.transform.position = Vector3.Lerp(
                    new Vector3(-15f, -7f, 0),
                    new Vector3(-12.64f, -2.8f, 0),
                    t);
                mousePos = demoMouse.transform.position;
                if (demoMouse.transform.position.y > -2.9) {
                    mouseAnimator.SetBool("clicked", true);
                    if (!ammoCreated) {
                        ammoCreated = true;
                        demoAmmo.GetComponent<Rigidbody2D>().isKinematic = true;
                        ammo = Instantiate(demoAmmo, ammoStartPoint.position, Quaternion.identity);
                    }
                }
            }
            else { // ammoCreated
                //t += Time.deltaTime / introDuration;
                //demoMouse.transform.position = Vector3.Lerp(mousePos, new Vector3(-13.5f, -4.3f, 0), t);
                StartCoroutine(StartGame());
            }
        }
    }

    public void GameManagerOnGameStateChanged(GameState state) {
        startMovement = (state == GameState.Intro);
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(2f);
        Destroy(demoMouse, .5f);
        Destroy(ammo, .5f);
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
}