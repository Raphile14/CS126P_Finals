using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    //public AudioMixer audioMixer;
    public Slider VolumeSlider;
    public TMP_Dropdown GraphicSettings;
    public TMP_Dropdown ResolutionSettings;
    public Toggle FullscreenToggle;

    Resolution[] Resolutions;

    private void Start()
    {
        Resolutions = Screen.resolutions;
        ResolutionSettings.ClearOptions();
        List<string> StringResolutions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < Resolutions.Length; i++)
        {
            string option = Resolutions[i].width + " x " + Resolutions[i].height;
            StringResolutions.Add(option);

            if ((Resolutions[i].width == Screen.currentResolution.width) && Resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionSettings.AddOptions(StringResolutions);
        ResolutionSettings.value = currentResolutionIndex;
        ResolutionSettings.RefreshShownValue();
        FullscreenToggle.isOn = PlayerPrefs.HasKey("Fullscreen") ? (PlayerPrefs.GetInt("Fullscreen") != 0) : MainMenuScript.DefaultFullscreen;
        VolumeSlider.value = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : MainMenuScript.DefaultVolumeLevel;
        GraphicSettings.value = PlayerPrefs.HasKey("QualityLevel") ? PlayerPrefs.GetInt("QualityLevel") : MainMenuScript.DefaultQuality;
        PlayerPrefs.Save();
    }

    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        VolumeSlider.value = volume;
        PlayerPrefs.Save();
    }

    public void SetQuality (int qualityIndex)
    {
        GraphicSettings.value = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetDefault()
    {
        SetResolution(Resolutions.Length-1);
        ToggleFullscreen(MainMenuScript.DefaultFullscreen);
        SetVolume(MainMenuScript.DefaultVolumeLevel);
        SetQuality(MainMenuScript.DefaultQuality);
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0));
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = Resolutions[index];
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.Save();
    }
}
