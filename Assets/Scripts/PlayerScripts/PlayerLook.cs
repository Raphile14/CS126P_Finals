using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    // Mouse sensitivity
    public float MouseSensitivity = 100f;
    public Camera Camera;
    public bool isScientist;

    // Player Reference
    public Transform PlayerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.enabled && !PlayerMovement.isDead)
        {
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * MouseX);
        }
        if (PlayerMovement.isDead)
        {
            if (isScientist)
            {
                Camera.enabled = true;
                transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                transform.localRotation = Quaternion.Euler(90, 0f, 0f);
            }
            else
            {
                Camera.enabled = false;
            }
        }
    }
}
