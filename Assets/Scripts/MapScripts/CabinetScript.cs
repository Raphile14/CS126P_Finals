using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetScript : MonoBehaviour
{
    private GameObject item;
    private RoomTemplates roomTemplates;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.Find("Station").GetComponent<RoomTemplates>();
        if (roomTemplates.GetRandomValue() % 2 == 0){
            Destroy(transform.gameObject);
        }
    }

    void OnTriggerStay(Collider other){
        if (other.CompareTag("Player")){
            if (Input.GetKeyDown(KeyCode.E) && transform.childCount == 3){
                PlayerMovement.syringe++;
                Debug.Log("Total Syringe: " + PlayerMovement.syringe.ToString());
                Destroy(transform.GetChild(2).gameObject);
                roomTemplates.SpawnSyringe(transform.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
