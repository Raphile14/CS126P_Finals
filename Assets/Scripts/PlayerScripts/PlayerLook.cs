using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    // Mouse sensitivity
    public float MouseSensitivity = 100f;
    public Camera Camera;

    // Player Reference
    public Transform PlayerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.enabled)
        {
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * MouseX);
        }        
    }
}
