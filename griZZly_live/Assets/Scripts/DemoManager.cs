using System.Collections;
using UnityEngine;

public class DemoManager : MonoBehaviour {
    public GameObject demoAmmo;
    public Transform ammoStartPoint;
    public GameObject demoMouse;
    public float introDuration = 5;
    public IntroState State;
    private Animator mouseAnimator;
    private Vector3 mousePos;
    float t = 0;
    float t2 = 0;
    private GameObject ammo;
    private AudioSource source;


    void Start() {
        demoMouse.GetComponent<Animator>().StopPlayback();
        mouseAnimator = demoMouse.GetComponent<Animator>();
        State = IntroState.MoveMouseToStartingPosition;
        source = GetComponent<AudioSource>();
        if (GameManager.Instance.State == GameState.Intro) {
         //
         //source.Play();
        }
    }


    void LateUpdate() {
        if (GameManager.Instance.State == GameState.Intro) {
            switch (State) {
                case IntroState.MoveMouseToStartingPosition:
                    if (!ammo) {
                        source.Play();
                        ammo = Instantiate(demoAmmo, new Vector3(ammoStartPoint.position.x, ammoStartPoint.position.y, 0), Quaternion.identity);
                        ammo.GetComponent<Rigidbody2D>().isKinematic = true;
                    }
                    t += Time.deltaTime / introDuration;
                    demoMouse.transform.position = Vector3.Lerp(
                        new Vector3(-20f, -16f, 0),
                        new Vector3(-13f,-7.8f,0), //Vector3(-12.64f, -2.8f, 0),
                        t);
                    mousePos = demoMouse.transform.position;
                    if (demoMouse.transform.position.y > -8) {
                        mouseAnimator.SetBool("clicked", true);
                        State = IntroState.MoveMouseBack;
                    }

                    break;
                case IntroState.MoveMouseBack:
                    t2 += Time.deltaTime / 2;
                    demoMouse.transform.position = Vector3.Lerp(
                        mousePos,
                        new Vector3(-15.64f, -8.8f, 0),
                        t2);
                    ammo.transform.position = Vector3.Lerp(ammoStartPoint.position,
                        new Vector3(-15.64f, -8.8f, 0),
                        t2);
                    if (demoMouse.transform.position.x < -15) {
                        State = IntroState.ExitIntro;
                    }

                    source.volume = Mathf.Lerp(1, 0, t2); // fade out music
                    break;
                case IntroState.Shoot:
                    break;
                case IntroState.ExitIntro:
                    source.Stop();

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