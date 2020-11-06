using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskGasRefill : MonoBehaviour
{
    public float Progress = 0f;
    public float Rate = 10f;
    private bool isPressed;
    public GameObject ButtonImage;
    public GameObject[] meters;
    public AudioSource beepGas;
    public AudioSource beepSuccess;

    private void Start()
    {
        playSound();
    }

    private void playSound()
    {
        beepGas.volume = UnityEngine.Random.Range(0.4f, 0.6f);
        beepGas.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
        beepGas.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            if (Progress >= 100f)
            {
                TaskFinished();
                meters[13].SetActive(true);
            }
            else
            {
                Progress += Rate * Time.deltaTime;
            }
            ButtonImage.SetActive(true);

        }
        else
        {
            ButtonImage.SetActive(false);
        }
        
        if (Progress > 2f && Progress < 6f )
        {
            meters[0].SetActive(true);
        }
        else if (Progress > 6f &&  Progress < 12f)
        {
            meters[0].SetActive(false);
            meters[1].SetActive(true);
        }
        else if (Progress > 12f && Progress < 18f)
        {
            meters[1].SetActive(false);
            meters[2].SetActive(true);
        }
        else if (Progress > 18f && Progress < 24f)
        {
            meters[2].SetActive(false);
            meters[3].SetActive(true);
        }
        else if (Progress > 24f && Progress < 30f)
        {
            meters[3].SetActive(false);
            meters[4].SetActive(true);
        }
        else if (Progress > 36f && Progress < 42f)
        {
            meters[4].SetActive(false);
            meters[5].SetActive(true);
        }
        else if (Progress > 48f && Progress < 54f)
        {
            meters[5].SetActive(false);
            meters[6].SetActive(true);
        }
        else if (Progress > 60f && Progress < 66f)
        {
            meters[6].SetActive(false);
            meters[7].SetActive(true);
        }
        else if (Progress > 66f && Progress < 72f)
        {
            meters[7].SetActive(false);
            meters[8].SetActive(true);
        }
        else if (Progress > 72f && Progress < 78f)
        {
            meters[8].SetActive(false);
            meters[9].SetActive(true);
        }
        else if (Progress > 78f && Progress < 84f)
        {
            meters[9].SetActive(false);
            meters[10].SetActive(true);
        }
        else if (Progress > 84f && Progress < 90f)
        {
            meters[10].SetActive(false);
            meters[11].SetActive(true);
        }
        else if (Progress > 90f && Progress < 96f)
        {
            meters[11].SetActive(false);
            meters[12].SetActive(true);
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
