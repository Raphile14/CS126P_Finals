using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistScript : MonoBehaviour{
    private RoomTemplates roomTemplates;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.Find("Station").GetComponent<RoomTemplates>();
        int rand = roomTemplates.GetRandomValue();
        transform.rotation = Quaternion.Euler(0, rand % 180, 0);
        if (rand % 2 == 0){
            Destroy(transform.gameObject);
        }
        // transform.gameObject.SetActive(rand % 2 == 0);
    }

}
