using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance { get; private set; } = null;
    public static string GameSeed { get; set; } = "";
    public static int GameDifficulty { get; set; } = 0;

    private void Awake()
    {
        if ((Instance != null && Instance != this))
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
