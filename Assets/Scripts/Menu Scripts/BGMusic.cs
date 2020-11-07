using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    public static BGMusic Instance { get; private set; } = null;

    private void Awake()
    {
        if ((Instance != null && Instance != this) || SceneManager.GetActiveScene().name == "Map")
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
