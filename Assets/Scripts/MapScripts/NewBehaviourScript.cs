using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour{

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Walls") || other.CompareTag("OuterWalls")){
            // Debug.Log("Trigger False!" + RoomTemplates.numRooms.ToString());
            RoomTemplates.good = false;
        }
    }
}

