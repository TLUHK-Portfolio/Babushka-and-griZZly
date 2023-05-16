using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] public TMP_Text volumeTextUI;
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    [SerializeField] public Toggle showProjectile;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    private int currentShowProjectile = 0;

    private void Start()
    {
        LoadValues();
    }

    public void PopulateDResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>(resolutions);

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                if (!filteredResolutions.Contains(resolutions[i])){
                    filteredResolutions.Add(resolutions[i]);
                }
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + " x " + filteredResolutions[i].height;

            if (!options.Contains(option))
            {
                options.Add(option);
            }

            if (filteredResolutions[i].width == Screen.currentResolution.width && filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        }        

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (filteredResolutions == null)
        {
            PopulateDResolutionDropdown();
        }

        if (resolutionIndex == null || resolutionIndex == 1000)
        {
            resolutionIndex = filteredResolutions.Count - 1;
        }
        Debug.Log("Resolution Index: " + resolutionIndex);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        // Debug.Log("Resolution changed to: " + resolution.width + " x " + resolution.height);
    }

    private void Update()
    {
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

    public void LoadValues()
    {
        if (filteredResolutions == null)
        {
            PopulateDResolutionDropdown();
        }

        float savedVolume = PlayerPrefs.GetFloat("VolumeValue", 100f);
        //Debug.Log("Volume Value: " + savedVolume);

        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume / 100;

        showProjectile.isOn = PlayerPrefs.GetInt("ShootingAid", 1) == 1;
    }

    public void SetProjectile(bool value)
    {
        showProjectile.isOn = value;
        if (value)
        {
            PlayerPrefs.SetInt("ShootingAid", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShootingAid", 0);
        }
    }
}
