using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    // Go back to main menu
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
