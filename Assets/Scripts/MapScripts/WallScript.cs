using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private bool hasMoved = false;

    private void Update()
    {
        if (!hasMoved)
        {
            if (RoomTemplates.isMapFinished)
            {
                //northwall.transform.position.x + 0.2;
            }
            hasMoved = true;
        }
    }
    // Start is called before the first frame update
    // public void OnTriggerEnter(Collider other){
    //     if (other.transform.position == transform.position){
    //         Debug.Log("Destroyed!");
    //         Destroy(transform.gameObject);
    //     }
    //     Debug.Log(other.tag);
    // }
}
