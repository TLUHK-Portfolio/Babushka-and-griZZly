using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] public TMP_Text volumeTextUI;
    [SerializeField] public Slider volumeSlider;

    private void Start() {
        LoadValues();
    }

    private void Update() {
        float volumeValue = volumeSlider.value;
        volumeTextUI.text = volumeValue.ToString();

        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    // public void VolumeSlider(float volume)
    // {
    //     gameObject.GetComponent<TextMesh>().text = volume.ToString("0");
    //     float volumeValue = volumeSlider.value;

    //     PlayerPrefs.SetFloat("VolumeValue", volumeValue);
    //     LoadValues();
    // }

    void LoadValues()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumeValue");

        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume / 100;
    }
}
