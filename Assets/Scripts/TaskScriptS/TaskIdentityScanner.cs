using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskIdentityScanner : MonoBehaviour
{
    public float Progress = 0f;
    public float Rate = 10f;
    private bool isPressed;
    public GameObject ButtonImage;
    public GameObject ButtonImageSuccess;
    public GameObject[] meters;
    public AudioSource bigStatic;
    public AudioSource beepSuccess;

    private void Start()
    {
        playSound();
    }
    private void playSound()
    {
        bigStatic.volume = UnityEngine.Random.Range(0.4f, 0.6f);
        bigStatic.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
        bigStatic.Play();
    }

    // Update is called once per frame
    void Update()
    {        
        if (isPressed)
        {
            if (Progress >= 100f)
            {
                TaskFinished();
                meters[23].SetActive(false);
                meters[24].SetActive(true);
                ButtonImage.SetActive(false);
                ButtonImageSuccess.SetActive(true);
            }
            else
            {
                Progress += Rate * Time.deltaTime;
                ButtonImage.SetActive(true);
            }            
        }
        else
        {
            ButtonImage.SetActive(false);
        }

        if (Progress < 100)
        {
            float multiplier = Progress / 4;
            multiplier = Mathf.Round(multiplier);
            if (multiplier >= 1)
            {
                meters[(int)multiplier - 1].SetActive(true);
                if ((int)multiplier > 1)
                {
                    meters[((int)multiplier) - 2].SetActive(false);
                }
            }
            
        }                
    }

    public void PointerDown()
    {
        isPressed = true;
    }

    public void PointerUp()
    {
        isPressed = false;
    }

    public void TaskFinished()
    {
        GetComponent<Collider>().isTrigger = true;
        beepSuccess.volume = UnityEngine.Random.Range(0.4f, 0.6f);
        beepSuccess.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
        beepSuccess.Play();
    }
}
