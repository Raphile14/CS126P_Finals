using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class CreateScript : MonoBehaviour
{
    public GameObject SeedInput;    
    public TMP_Dropdown DifficultyInput;

    public void SetSeed()
    {
        // Sets the seed value
        string seed = SeedInput.GetComponent<TMP_InputField>().text;

        // Sets the difficulty value
        DifficultyInput.value = 0;
        DifficultyInput.RefreshShownValue();
        int difficultyLevel = DifficultyInput.value;

        // Updates Value in PersistentData
        PersistentData.GameSeed = seed;
        PersistentData.GameDifficulty = difficultyLevel;
    }

    // Back to main menu
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }    
}
