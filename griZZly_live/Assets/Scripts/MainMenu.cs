using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject backgroundMusic;

    private void Start() {
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }

    public void PlayGame()
    {
        StartCoroutine(FadeOut(backgroundMusic.GetComponent<AudioSource>(), 1, gameObject));
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, GameObject mainMenuObject) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        mainMenuObject.SetActive(false);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
