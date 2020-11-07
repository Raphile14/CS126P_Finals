using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    // Controller Reference
    public CharacterController controller;
    public Transform GroundCheck;
    public Camera DroneCamera;
    public AudioSource DroneSound;

    // Values
    public float speed = 7.0f;
    public float rise = 10.0f;
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
            if (!DroneSound.isPlaying)
            {
                DroneSound.Play();
            }
            velocity.y = 0;

            // Movement forward and sideward
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            // Float up
            if (Input.GetKey(KeyCode.Space) && !isGrounded) {
                transform.position = new Vector3(transform.position.x, transform.position.y + (rise * Time.deltaTime), transform.position.z);            
            }

            // Float Down
            else if (Input.GetKey(KeyCode.LeftShift) && !isGrounded)
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
            if (DroneSound.isPlaying)
            {
                DroneSound.Stop();
            }
            if (!isGrounded)
            {                
            }
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        
    }
}
