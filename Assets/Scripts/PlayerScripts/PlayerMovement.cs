using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Controller Reference
    public CharacterController controller;
    public Transform GroundCheck;
    public Camera ScientistCamera;
    public PostScript postScript;
    public AudioSource TiredSound;
    public AudioSource ScreamSound;
    public AudioSource SyringeSound;
    public Animator PlayerAnimation;
    public Text SyringeCounter;
    public Text HealthCounter;
    public Text TasksCounter;
    public GameObject[] Videos;

    // Values
    public float speed = 6f;
    public float sprintSpeed = 15f;
    public float sprintDecay = 10f;
    public float sprintRegen = 6f;
    public float stamina = 100f;
    public float maxHealth = 100f;
    public float health = 100f;
    public float damage = 50f;
    public float staminaThreshold = 40f;
    public float gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public float StunTimer = 5f;
    public static int syringe = 0;
    public float SyringeRegen = 25f;
    public LayerMask GroundMask;
    private int index;

    // Private Values
    Vector3 velocity;
    bool isGrounded;
    public static bool isDead = false;
    bool isStunned = false;

    // Private values
    public bool isSprinting = false;
    public bool isTired;
    
    public void ScreamPlayer ()
    {
        if (!ScreamSound.isPlaying)
        {
            ScreamSound.Play();            
        }
        health -= damage;
        if (health <= 0)
        {
            // transfer to death scene
            isDead = true;
            PlayerAnimation.SetBool("isDead", true);
            Invoke("ChangeScene", 5f);            
        }
        else
        {
            isStunned = true;
            index = UnityEngine.Random.Range(0, Videos.Length - 1);
            Videos[index].SetActive(true);
            Invoke("stunPlayer", StunTimer);            
        }
    }

    void stunPlayer()
    {
        // Set to False Default
        // Walking
        PlayerAnimation.SetBool("isWalking", false);
        PlayerAnimation.SetBool("isWalkingBack", false);
        PlayerAnimation.SetBool("isWalkingRight", false);
        PlayerAnimation.SetBool("isWalkingLeft", false);

        // Sprinting
        PlayerAnimation.SetBool("isRunningRightSide", false);
        PlayerAnimation.SetBool("isRunningLeftSide", false);
        PlayerAnimation.SetBool("isRunningBack", false);
        PlayerAnimation.SetBool("isRunningForward", false);        
        isStunned = false;
        Videos[index].SetActive(false);
    }

    void ChangeScene()
    {
        isDead = false;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(5);
    }

    // Update is called once per frame
    void Update()
    {
        TasksCounter.text = "TASKS: " + RoomTemplates.taskToBeFinish;
        HealthCounter.text = "HEALTH: " + health;
        SyringeCounter.text = "SYRINGE: " + syringe;
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (ScientistCamera.enabled)
        {
            if (!isStunned)
            {
                if (Input.GetKey(KeyCode.F) && syringe > 0 && health != maxHealth)
                {
                    if (!SyringeSound.isPlaying)
                    {
                        SyringeSound.Play();
                    }
                    health += SyringeRegen;
                    if (health > maxHealth) { health = maxHealth; }
                    syringe--;
                }
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = transform.right * x + transform.forward * z;

                // Set to False Default
                PlayerAnimation.SetBool("isStunned", false);
                // Walking
                PlayerAnimation.SetBool("isWalking", false);
                PlayerAnimation.SetBool("isWalkingBack", false);
                PlayerAnimation.SetBool("isWalkingRight", false);
                PlayerAnimation.SetBool("isWalkingLeft", false);

                // Sprinting
                PlayerAnimation.SetBool("isRunningRightSide", false);
                PlayerAnimation.SetBool("isRunningLeftSide", false);
                PlayerAnimation.SetBool("isRunningBack", false);
                PlayerAnimation.SetBool("isRunningForward", false);

                // Walking            
                if (Input.GetKey(KeyCode.W))
                {
                    PlayerAnimation.SetBool("isWalking", true);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    PlayerAnimation.SetBool("isWalkingBack", true);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerAnimation.SetBool("isWalkingLeft", true);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    PlayerAnimation.SetBool("isWalkingRight", true);
                }

                // Sprinting
                if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
                {
                    isSprinting = true;
                    // Sprinting
                    PlayerAnimation.SetBool("isRunning", true);
                    if (Input.GetKey(KeyCode.W))
                    {
                        PlayerAnimation.SetBool("isRunningForward", true);
                        stamina -= sprintDecay * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        PlayerAnimation.SetBool("isRunningBack", true);
                        stamina -= sprintDecay * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        PlayerAnimation.SetBool("isRunningLeftSide", true);
                        stamina -= sprintDecay * Time.deltaTime;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        PlayerAnimation.SetBool("isRunningRightSide", true);
                        stamina -= sprintDecay * Time.deltaTime;
                    }
                    controller.Move(move * sprintSpeed * Time.deltaTime);
                }
                else
                {
                    if (stamina < 100)
                    {
                        stamina += sprintRegen * Time.deltaTime;
                    }
                    isSprinting = false;
                    PlayerAnimation.SetBool("isRunning", false);
                    controller.Move(move * speed * Time.deltaTime);
                }
                if (stamina < staminaThreshold)
                {
                    isTired = true;
                }
                else
                {
                    isTired = false;
                }
            }
            else
            {
                PlayerAnimation.SetBool("isStunned", true);
            }
            
        }
        if (isTired && !TiredSound.isPlaying)
        {
            TiredSound.Play();
        }
        else if (!isTired && TiredSound.isPlaying)
        {
            TiredSound.Stop();
        }
        postScript.motionBlur.intensity.Override((100 - stamina) / 100);
        postScript.vignette.intensity.Override((100 - stamina) / 100);
        postScript.filmGrain.intensity.Override((100 - stamina) / 100);

        if (health <= damage)
        {
            postScript.colorAdjustments.saturation.Override(-100);
        }
        else
        {
            postScript.colorAdjustments.saturation.Override(0);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
