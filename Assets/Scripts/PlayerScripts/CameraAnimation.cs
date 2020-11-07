using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public CharacterController playerController;
    public Animation anim; //Empty GameObject's animation component
    public AudioSource Footstep;
    public AudioSource Footstep2;
    private bool isMoving;
    public Camera ScientistCamera;

    private bool left;
    private bool right;

    void CameraAnimations()
    {
        if (playerController.isGrounded == true && ScientistCamera.enabled)
        {
            if (isMoving == true)
            {
                if (left == true)
                {
                    if (!anim.isPlaying)
                    {//Waits until no animation is playing to play the next
                        anim.Play("walkLeft");
                        left = false;
                        right = true;
                        Footstep.volume = UnityEngine.Random.Range(0.4f, 0.6f);
                        Footstep.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
                        Footstep.Play();
                    }
                }
                if (right == true)
                {
                    if (!anim.isPlaying)
                    {
                        anim.Play("walkRight");
                        right = false;
                        left = true;
                        Footstep2.volume = UnityEngine.Random.Range(0.4f, 0.6f);
                        Footstep2.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
                        Footstep2.Play();
                    }
                }           
            }
        }
    }


    void Start()
    { //First step in a new scene/life/etc. will be "walkLeft"
        left = true;
        right = false;
    }


    void Update()
    {
        float inputX = Input.GetAxis("Horizontal"); //Keyboard input to determine if player is moving
        float inputY = Input.GetAxis("Vertical");

        if (inputX != 0 || inputY != 0)
        {
            isMoving = true;
        }
        else if (inputX == 0 && inputY == 0)
        {
            isMoving = false;
        }

        CameraAnimations();

    }
}
