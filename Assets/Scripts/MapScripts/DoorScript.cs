using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorScript : MonoBehaviour
{

    // NOTES
    // Z AXIS = NORTH/SOUTH
    // X AXIS = EAST/WEST

    // Public Values
    // Doors
    public GameObject LeftDoor;
    public GameObject RightDoor;
    private GameObject DoorSwap;
    public AudioSource DoorSound;
    private bool isOpen = false;

    // Open speed
    public float SpeedOpen = 5f;
    // How far doors open
    public float DistanceOpen = 5f;

    // Store original door position
    private Vector3 LeftOriginalPosition, RightOriginalPosition, LeftMovePosition, RightMovePosition;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.z < LeftDoor.transform.position.z){
            DoorSwap = RightDoor;
            RightDoor = LeftDoor;
            LeftDoor = DoorSwap;
        } else if (transform.position.x < LeftDoor.transform.position.x) {
            DoorSwap = RightDoor;
            RightDoor = LeftDoor;
            LeftDoor = DoorSwap;
        }
        LeftOriginalPosition = LeftDoor.transform.position;
        RightOriginalPosition = RightDoor.transform.position;

        // Calculate New Position
        if (RightDoor.transform.position.z == LeftDoor.transform.position.z)
        {
            LeftMovePosition = new Vector3(LeftOriginalPosition.x - DistanceOpen, LeftOriginalPosition.y, LeftOriginalPosition.z);
            RightMovePosition = new Vector3(RightOriginalPosition.x + DistanceOpen, RightOriginalPosition.y, RightOriginalPosition.z);
        }
        else if (RightDoor.transform.position.x == LeftDoor.transform.position.x)
        {
            LeftMovePosition = new Vector3(LeftOriginalPosition.x, LeftOriginalPosition.y, LeftOriginalPosition.z - DistanceOpen);
            RightMovePosition = new Vector3(RightOriginalPosition.x, RightOriginalPosition.y, RightOriginalPosition.z + DistanceOpen);
        }

        // StartCoroutine(getReference(0.1F));
    }

    // IEnumerator getReference(float time){
    //     while (!PlayerObject){y
    //         yield return new WaitForSeconds(time);
            
    //     }
    // }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerObject" || other.name == "CubeObject")
        {            
            if (Vector3.Distance(transform.position, other.transform.position) < 7){
                LeftDoor.transform.position = Vector3.Slerp(LeftDoor.transform.position, LeftMovePosition, SpeedOpen * Time.deltaTime);
                RightDoor.transform.position = Vector3.Slerp(RightDoor.transform.position, RightMovePosition, SpeedOpen * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, other.transform.position) > 7) {
                LeftDoor.transform.position = Vector3.Slerp(LeftDoor.transform.position, LeftOriginalPosition, SpeedOpen * 2f * Time.deltaTime);
                RightDoor.transform.position = Vector3.Slerp(RightDoor.transform.position, RightOriginalPosition, SpeedOpen * 2f * Time.deltaTime);                
            }            
        }         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerObject" || other.name == "CubeObject")
        {
            if (!DoorSound.isPlaying && !isOpen)
            {
                DoorSound.Play();
                isOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerObject" || other.name == "CubeObject")
        {
            if (!DoorSound.isPlaying && isOpen)
            {
                DoorSound.Play();
                isOpen = false;
            }
        }
    }
}
