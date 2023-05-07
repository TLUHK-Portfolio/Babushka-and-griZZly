using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private GameObject backgroundMusic;

    private void Start() {
        backgroundMusic = GameObject.Find("BackgroundMusic");

        int levelOneCompleted = PlayerPrefs.GetInt("LevelOneCompleted", 0);

        if (levelOneCompleted == 1) {
            UnLockLevel("LevelTwo");
            // mõtle mooodus kuidas kuidas resettida leveleid
        }
    }

    public void LockLevel(string levelName)
    {
        GameObject level = gameObject.transform.Find(levelName).gameObject;
        
        level.transform.Find("Locked").gameObject.SetActive(true);
        level.GetComponent<Button>().interactable = false;
        level.transform.Find("Text").gameObject.SetActive(false);
    }

    public void UnLockLevel(string levelName)
    {
        GameObject level = gameObject.transform.Find(levelName).gameObject;
        
        level.transform.Find("Locked").gameObject.SetActive(false);
        level.GetComponent<Button>().interactable = true;
        level.transform.Find("Text").gameObject.SetActive(true);
    }

    public void StartLevel(int index)
    {
        StartCoroutine(FadeOut(backgroundMusic.GetComponent<AudioSource>(), 1, index));
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, int index)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
        //SceneManager.LoadScene("Level1");
        // mainMenuObject.SetActive(false);
    }
}
