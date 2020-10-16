using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Switch to Create Lobby Scene
    public void CreateButton()
    {
        SceneManager.LoadScene(1);
    }

    // Switch to Join Lobby Scene
    public void JoinButton()
    {
        SceneManager.LoadScene(2);
    }

    // Switch to How To Play Scene
    public void HowToPlayButton()
    {
        SceneManager.LoadScene(3);
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(4);
    }

    // Quit Program
    public void QuitGame()
    {
        Debug.Log("ApplicationQuit");
        Application.Quit();
    }
}
