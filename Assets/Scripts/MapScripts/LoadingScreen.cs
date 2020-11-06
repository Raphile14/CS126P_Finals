using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private int index = 0;
    public GameObject[] screens;
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(true);
        StartCoroutine(changeImage(3F));
        screens[index].SetActive(true);
    }

    IEnumerator changeImage(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            screens[index].SetActive(false);
            index++;
            if (index > screens.Length - 1) { index = 0; }
            screens[index].SetActive(true);
        }
    }
}
