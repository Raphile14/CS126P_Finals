using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    // Controller Reference
    public CharacterController controller;
    public Transform GroundCheck;
    public Camera DroneCamera;

    // Values
    public float speed = 1.5f;
    public float rise = 3.0f;
    public float gravity = -9.81f;
    public float GroundDistance = 0.2f;
    public LayerMask GroundMask;

    // Velocity
    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {        

        // If drone is active
        if (DroneCamera.enabled)
        {
            velocity.y = 0;

            // Movement forward and sideward
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            // Float up
            if (Input.GetKey(KeyCode.Space)) {
                transform.position = new Vector3(transform.position.x, transform.position.y + (rise * Time.deltaTime), transform.position.z);            
            }

            // Float Down
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y - (rise * Time.deltaTime)), transform.position.z);
            }
            

        }        

        // If drone is not controlled
        else
        {
            // Physics
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        
    }
}
