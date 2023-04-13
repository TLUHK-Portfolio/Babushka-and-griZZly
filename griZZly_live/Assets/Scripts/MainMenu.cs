using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject backgroundMusic;

     [SerializeField]
     private GameObject SettingsController;

    private void Start() {
        backgroundMusic = GameObject.Find("BackgroundMusic");
        SettingsController.GetComponent<SettingsController>().LoadValues();
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
