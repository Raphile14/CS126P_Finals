using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour{
    // Win variable
    public bool win = false;

    // Prefabs
    public GameObject[] rooms, interiors, tasks, environments;

    // Characters
    public GameObject player, drone, cube, item, loadingScreen ;

    // Map Properties
    public int mapSize, totalTasks, totalSyringe, finishedTasks;
    public static int taskToBeFinish, difficultyLevel;

    // Seed Variables
    private int rand, seed;
    public static string stringSeed;

    // Map Generation Variables
    private List<GameObject> activeTerminals = new List<GameObject>();
    private GameObject[] availableTerminals, availableCabinets, compiledWalls;
    private GameObject currentObject, environmentHolder;
    private UnityEngine.AI.NavMeshAgent pathChecker;
    private UnityEngine.AI.NavMeshPath path;
    private int curXArea, curZArea, nextXArea, nextZArea;
    private int numRooms = -1;
    private int numInteriors = 0;
    private int spawnedSyringe = 0;
    public static bool good;
    private bool isRoomsFinished = false;
    public static bool isMapFinished;
    private bool isInteriorStructuresFinished = false;
    private bool isInteriorsFinished = false;

    // Start is called before the first frame update
    void Start(){
        // Map Setup
        Time.timeScale = 1f;
        loadingScreen.SetActive(true);
        player.SetActive(false);
        cube.SetActive(false);
        drone.SetActive(false);
        isMapFinished = false;
        good = true;
        stringSeed = PersistentData.GameSeed;
        difficultyLevel = PersistentData.GameDifficulty;
        if (difficultyLevel == 0){
            totalTasks = 1;
            totalSyringe = 15;
        } else if (difficultyLevel == 1){
            totalTasks = 20;
            totalSyringe = 10;
        } else if (difficultyLevel == 2){
            totalTasks = 30;
            totalSyringe = 5;
        }
        taskToBeFinish = totalTasks;
        PlayerMovement.syringe = 0;
        
        pathChecker = GameObject.Find("PathChecker").GetComponent<UnityEngine.AI.NavMeshAgent>();
        path = new UnityEngine.AI.NavMeshPath();
        if (stringSeed.Length == 0){
            stringSeed = Random.Range(0,100000).ToString();
        }
        seed = stringSeed.GetHashCode();
        Random.InitState(seed);
        Debug.Log(stringSeed);
        PersistentData.GameSeed = stringSeed;

        curXArea = mapSize;
        curZArea = mapSize;
        environmentHolder = GameObject.Find("Environment");        
        StartCoroutine(SpawnTimer(0.1F));
    }

    // Spawn Rooms and location handler
    void SpawnRooms(){
        if (curXArea < -mapSize){
            curXArea = mapSize;
            curZArea -= 50;
        }
        if (curXArea == 0 && curZArea == 0){
            curXArea -= 50;
        }

        rand = GetRandomValue() % rooms.Length;
        currentObject = Instantiate(rooms[rand], new Vector3(curXArea, 0, curZArea), rooms[rand].transform.rotation);
        currentObject.transform.SetParent (transform);
        
        curXArea -= 50;

        if (curXArea < -mapSize){
            nextXArea = curXArea + 50;
            nextZArea = curZArea - 50;
        }
        else{
            nextXArea = curXArea;
            nextZArea = curZArea;
        }
    }

    void SpawnEnvironment(){
        for (int i = 0; i < 500; i++){
            int index = GetRandomValue() % environments.Length;
            int x = GetRandomValue() % 120;
            int z = GetRandomValue() % 120;
            if (GetRandomValue() % 2 == 0){
                x *= -1;
            } 
            if (GetRandomValue() % 2 == 0) {
                z *= -1;
            }
            rand = GetRandomValue() % 4;
            currentObject = Instantiate(environments[index], new Vector3(x,3,z), Quaternion.Euler(0, rand * 90, 0));
            currentObject.transform.SetParent(transform.GetChild(0));
        }
    }

    // Interior Spawner
    void SpawnInteriors(){
        if (curXArea < -mapSize){
            curXArea = mapSize;
            curZArea -= 50;
        }

        rand = GetRandomValue() % 4;
        if (curXArea == mapSize && curZArea == mapSize){
            currentObject = Instantiate(interiors[3], new Vector3(curXArea, 0, curZArea), Quaternion.identity);
            currentObject.transform.SetParent(transform);
        }
        else if (curXArea == 0 && curZArea == 0){
            currentObject = Instantiate(interiors[1], new Vector3(curXArea, 0, curZArea), Quaternion.Euler(0, 90 * rand, 0));
            currentObject.transform.SetParent(transform);
        }
        else if (curXArea == -mapSize && curZArea == -mapSize){
            currentObject = Instantiate(interiors[2], new Vector3(curXArea, 0, curZArea), Quaternion.Euler(0,270,0));
            currentObject.transform.SetParent(transform);
        }
        else {
            currentObject = Instantiate(interiors[0], new Vector3(curXArea, 0, curZArea), Quaternion.Euler(0,90 * rand,0));
            currentObject.transform.SetParent (transform);
        }

        curXArea -= 50;
    }

    public int GetRandomValue(){
        try{
            return int.Parse(Random.value.ToString().Substring(3));
        } catch {
            return 0;
        }
    }

    public void SpawnSyringe(GameObject currentCabinet){
        while(spawnedSyringe < totalSyringe){
            GameObject cabinet = availableCabinets[GetRandomValue() % availableCabinets.Length];
            if (cabinet.transform.childCount != 3 && cabinet != currentCabinet){
                currentObject = Instantiate(item, new Vector3(cabinet.transform.position.x, cabinet.transform.position.y + 2, cabinet.transform.position.z), Quaternion.identity);
                currentObject.transform.SetParent (cabinet.transform);
                spawnedSyringe++;
            }
        }
    }

    public GameObject GetTask(){
        rand = GetRandomValue() % tasks.Length;
        return tasks[rand];
    }

    // Map Generation Handler
    IEnumerator SpawnTimer(float respawnTime){        
        while (!isMapFinished){
            // Debug.Log("Call me maybe: " + respawnTime);
            yield return new WaitForSeconds(respawnTime);
            // Checks if the room is accessable
            if (currentObject){
                pathChecker.CalculatePath(currentObject.transform.position, path);
                if (path.status == UnityEngine.AI.NavMeshPathStatus.PathPartial){
                    good = false;
                }
                pathChecker.CalculatePath(new Vector3(nextXArea, 0, nextZArea), path);
                if (path.status == UnityEngine.AI.NavMeshPathStatus.PathPartial){
                    good = false;
                } 
            }

            // Checks if the map is full
            if (!isRoomsFinished && numRooms == Mathf.Pow(mapSize / 20, 2) - 1){
                isRoomsFinished = true;
                compiledWalls = GameObject.FindGameObjectsWithTag("Walls");
                curXArea = mapSize;
                curZArea = mapSize;
            }

            // Checks if the room generated is good to go
            if (!good){
                Destroy(currentObject);
                curXArea += 50;
                good = true;
            } else if (!isRoomsFinished) {
                numRooms++;
            }

            // Spawn rooms until the map is not full
            if (numRooms < Mathf.Pow(mapSize / 20, 2) - 1) {
                SpawnRooms();
            }

            // Spawns the interior structures of each rooms
            if (isRoomsFinished && (numInteriors < Mathf.Pow(mapSize / 20, 2))) {
                SpawnInteriors();
                numInteriors++;
                // Debug.Log("NumRooms: " + numInteriors.ToString());
                // Debug.Log("Total Rooms: " + (Mathf.Pow(mapSize / 20, 2)).ToString());
                if (numInteriors == Mathf.Pow(mapSize / 20, 2)){
                    // Debug.Log("Finished");
                    availableTerminals = GameObject.FindGameObjectsWithTag("Terminals");
                    availableCabinets = GameObject.FindGameObjectsWithTag("Cabinets");
                    SpawnSyringe(availableCabinets[0]);
                    Debug.Log(availableTerminals.Length);
                    isInteriorStructuresFinished = true;
                }
            }

            // Spawns the interior models of each rooms
            if (isInteriorStructuresFinished && activeTerminals.Count != totalTasks){
                rand = GetRandomValue() % availableTerminals.Length;
                if (!activeTerminals.Contains(availableTerminals[rand])){
                    availableTerminals[rand].transform.GetChild(0).gameObject.SetActive(true);
                    availableTerminals[rand].transform.GetChild(1).gameObject.SetActive(true);
                    activeTerminals.Add(availableTerminals[rand]);
                }
                if (activeTerminals.Count == totalTasks){
                    foreach (GameObject child in availableTerminals){
                        if (!child.transform.GetChild(0).gameObject.activeSelf){
                            Destroy(child);
                        }
                    }

                    // List<Vector3> safe = new List<Vector3>();
                    // foreach (GameObject child in compiledWalls){
                    //     if (!safe.Contains(child.transform.position)){
                    //         Debug.Log(child.transform.position);
                    //         safe.Add(child.transform.position);
                    //     } else {
                    //         Destroy(child);
                    //     }
                    // }



                    isInteriorsFinished = true;
                }
            }

            //  Start game if the conditions have met
            if (isRoomsFinished && isInteriorsFinished) {
                player.SetActive(true);
                drone.SetActive(true);
                cube.SetActive(true);
                SpawnEnvironment();
                loadingScreen.SetActive(false);
                Debug.Log(activeTerminals.Count);
                isMapFinished = true;
            }            
        }
    }
}

