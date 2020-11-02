using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    // Cameras
    public Camera ScientistCamera;
    public Camera DroneCamera;

    // Start is called before the first frame update
    void Start()
    {
        ScientistCamera.enabled = true;
        DroneCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScientistCamera.enabled = !ScientistCamera.enabled;
            DroneCamera.enabled = !DroneCamera.enabled;
        }
    }
}
