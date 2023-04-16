using System;
using System.Collections;
using UnityEngine;

public class DemoManager : MonoBehaviour {
    public GameObject demoAmmo;
    public Transform ammoStartPoint;
    public GameObject demoMouse;
    public float introDuration = 5;
    public IntroState State;
    private Animator mouseAnimator;
    private bool ammoCreated = false;
    private Vector3 mousePos;
    float t = 0;
    float t2 = 0;
    private GameObject ammo;


    void Start() {
        demoMouse.GetComponent<Animator>().StopPlayback();
        mouseAnimator = demoMouse.GetComponent<Animator>();
        State = IntroState.MoveMouseToStartingPosition;
    }

    
    void LateUpdate() {
        if (GameManager.Instance.State == GameState.Intro) {
            switch (State) {
                case IntroState.MoveMouseToStartingPosition:
                    t += Time.deltaTime / introDuration;
                    demoMouse.transform.position = Vector3.Lerp(
                        new Vector3(-15f, -7f, 0),
                        new Vector3(-12.64f, -2.8f, 0),
                        t);
                    mousePos = demoMouse.transform.position;
                    if (demoMouse.transform.position.y > -3) {
                        mouseAnimator.SetBool("clicked", true);
                        if (!ammo) {
                            ammoCreated = true;
                            ammo = Instantiate(demoAmmo, ammoStartPoint.position, Quaternion.identity);
                            ammo.GetComponent<Rigidbody2D>().isKinematic = true;
                            State = IntroState.MoveMouseBack;
                        }
                    }
                    break;
                case IntroState.MoveMouseBack:
                    t2 += Time.deltaTime / 2;
                    demoMouse.transform.position = Vector3.Lerp(
                        mousePos,
                        new Vector3(-13.64f, -3.8f, 0),
                        t2);
                    ammo.transform.position = Vector3.Lerp(ammoStartPoint.position,
                        new Vector3(-13.64f, -3.8f, 0),
                        t2);
                    if (demoMouse.transform.position.y < -3.79) {

                        State = IntroState.ExitIntro;
                    }
                    break;
                case IntroState.Shoot:
                    break;
                case IntroState.ExitIntro:
                    StartCoroutine(StartGame());
                    break;
            }
        }
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(2f);
        if (mouseAnimator)
        mouseAnimator.SetBool("clicked", false);
        Destroy(demoMouse);
        Destroy(ammo);
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

}

public enum IntroState {
    MoveMouseToStartingPosition,
    MoveMouseBack,
    Shoot,
    ExitIntro
}