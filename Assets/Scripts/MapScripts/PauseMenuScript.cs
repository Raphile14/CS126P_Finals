using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject PauseMenu;
    public GameObject OptionsPage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (OptionsPage.activeSelf)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }                
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        OptionsPage.SetActive(false);
    }

    public void PauseGame()
    {
        OptionsPage.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        PauseMenu.SetActive(false);
        OptionsPage.SetActive(true);
    }
}
