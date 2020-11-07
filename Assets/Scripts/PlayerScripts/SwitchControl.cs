using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    // Cameras
    public Camera ScientistCamera;
    public Camera DroneCamera;
    public GameObject ScientistVolume;
    public GameObject DroneVolume;
    public Animator PlayerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        ScientistCamera.enabled = true;
        DroneCamera.enabled = false;
        ScientistVolume.SetActive(ScientistCamera.enabled);
        DroneVolume.SetActive(DroneCamera.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScientistCamera.enabled = !ScientistCamera.enabled;
            DroneCamera.enabled = !DroneCamera.enabled;
            ScientistVolume.SetActive(ScientistCamera.enabled);
            DroneVolume.SetActive(DroneCamera.enabled);
        }
        if (!ScientistCamera.enabled)
        {
            PlayerAnimation.SetBool("isInteracting", true);
        }
        else 
        {
            PlayerAnimation.SetBool("isInteracting", false);
        }
    }
}
