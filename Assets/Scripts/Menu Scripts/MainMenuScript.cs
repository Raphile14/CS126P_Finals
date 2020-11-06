using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static float DefaultVolumeLevel = 0.8f;    
    public static float DefaultMouseSensitivity = 100f;
    public static int DefaultQuality = 5;
    public static bool DefaultFullscreen = true;

    private void Start()
    {
        int QualityLevel = PlayerPrefs.HasKey("QualityLevel") ? PlayerPrefs.GetInt("QualityLevel") : DefaultQuality;
        QualitySettings.SetQualityLevel(QualityLevel);
        AudioListener.volume = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : DefaultVolumeLevel;
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            Screen.fullScreen = (PlayerPrefs.GetInt("Fullscreen") != 0);
        }
        else
        {
            Screen.fullScreen = DefaultFullscreen;
        }
        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), Screen.fullScreen);
        }
        if (!PlayerPrefs.HasKey("MouseSensitivity"))
        {
            PlayerPrefs.SetFloat("MouseSensitivity", DefaultMouseSensitivity);
        }
    }    

    // Switch to Create Lobby Scene
    public void CreateButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    // Switch to Join Lobby Scene

    // Switch to How To Play Scene
    public void HowToPlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(3);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(4);
    }    

    public void ClearSeed()
    {
        PersistentData.GameSeed = "";
    }

    // Quit Program
    public void QuitGame()
    {
        Debug.Log("ApplicationQuit");
        Application.Quit();
    }    
}
