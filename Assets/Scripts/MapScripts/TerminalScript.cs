using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject task, tracker;
    private Light lights;
    private RoomTemplates roomTemplates;
    private EndingScript endingScript;
    private bool isDone = false;
    private bool isLightsOn = true;

    void Start(){
        lights = transform.GetChild(0).gameObject.GetComponent<Light>();
        tracker = transform.GetChild(2).gameObject;
        roomTemplates = GameObject.Find("Station").GetComponent<RoomTemplates>();
        endingScript = GameObject.Find("Ending Interior").GetComponent<EndingScript>();
        StartCoroutine(lightTimer(0.5F));
    }

    private void OnTriggerStay(Collider other) {
        if (task && other.name == "PlayerObject"){
            if (Input.GetKeyDown(KeyCode.E) && !task.activeSelf){
                Debug.Log("has pressed E");
                changeState();
            } else if (task.activeSelf && Input.GetKeyDown(KeyCode.E)) {
                changeState();
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if (task){
            if (other.name == "PlayerObject" && task.activeSelf){
                changeState();
            }
        }
    }

    private void changeState(){
        task.SetActive(!task.activeSelf);
        Debug.Log(task.activeSelf);
        if (task.activeSelf){
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }   

    IEnumerator lightTimer(float time){
        while (!isDone){
            yield return new WaitForSeconds(time);
            isLightsOn = !isLightsOn;
            lights.enabled = isLightsOn;

            if (!task && transform.GetChild(0).gameObject.activeSelf){
                task = Instantiate(roomTemplates.GetTask(), new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity);
                task.transform.SetParent (transform);
                task.SetActive(false);
            }
            if (task) {
                if (task.GetComponent<Collider>().isTrigger) {
                    roomTemplates.finishedTasks++;
                    RoomTemplates.taskToBeFinish = roomTemplates.totalTasks - roomTemplates.finishedTasks;
                    changeState();
                    Destroy(task);

                    Destroy(tracker);
                    lights.color = Color.green;
                    lights.enabled = true;
                    isDone = true;

                    if (roomTemplates.finishedTasks == roomTemplates.totalTasks){
                        roomTemplates.win = true;
                        endingScript.showExit();
                    }
                }
            }
        }
    }

}
