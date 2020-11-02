using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour{
    
    public int openingDirection;
    // 1 == BOTTOM DOOR
    // 2 == TOP DOOR
    // 3 == LEFT DOOR
    // 4 == RIGHT DOOR

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public static int numRooms = 20;
    public static int currentRooms = 0;
    public static string seed = "";
    public static int fullRoom = 1;

    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5F);
    }

    void Spawn(){
        if (spawned == false){
            if (seed.Length != 0){
                if (openingDirection == 1){
                    // BOTTOM DOOR
                    Instantiate(templates.bottomRooms[int.Parse(seed.Substring(0,1))], transform.position, templates.bottomRooms[int.Parse(seed.Substring(0,1))].transform.rotation);
                } else if (openingDirection == 2){
                    // TOP DOOR
                    Instantiate(templates.topRooms[int.Parse(seed.Substring(0,1))], transform.position, templates.topRooms[int.Parse(seed.Substring(0,1))].transform.rotation);
                } else if (openingDirection == 3){
                    // LEFT DOOR
                    Instantiate(templates.leftRooms[int.Parse(seed.Substring(0,1))], transform.position, templates.leftRooms[int.Parse(seed.Substring(0,1))].transform.rotation);
                } else if (openingDirection == 4){
                    // RIGHT DOOR
                    Instantiate(templates.rightRooms[int.Parse(seed.Substring(0,1))], transform.position, templates.rightRooms[int.Parse(seed.Substring(0,1))].transform.rotation);
                }
                seed = seed.Substring(1);
            }
            else{
                if (openingDirection == 1){
                    // BOTTOM DOOR
                    rand = Random.Range(0 + fullRoom, templates.bottomRooms.Length * fullRoom);
                    templates.seed += rand.ToString();
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                } else if (openingDirection == 2){
                    // TOP DOOR
                    rand = Random.Range(0 + fullRoom, templates.topRooms.Length * fullRoom);
                    templates.seed += rand.ToString();
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                } else if (openingDirection == 3){
                    // LEFT DOOR
                    rand = Random.Range(0 + fullRoom, templates.leftRooms.Length * fullRoom);
                    templates.seed += rand.ToString();
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                } else if (openingDirection == 4){
                    // RIGHT DOOR
                    rand = Random.Range(0 + fullRoom, templates.rightRooms.Length * fullRoom);
                    templates.seed += rand.ToString();
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                }
            }
            currentRooms++;
            if (currentRooms >= numRooms) fullRoom = 0;
            spawned = true;
            Debug.Log(templates.seed);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("SpawnPoints")){
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
