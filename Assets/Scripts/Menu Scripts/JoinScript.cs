﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinScript : MonoBehaviour
{
    // Back to main menu
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
