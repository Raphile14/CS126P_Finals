using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public GameObject LeftDoor, RightDoor, tracker, pointLight;
    private RoomTemplates roomTemplates;
    private bool gg = false;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.Find("Station").GetComponent<RoomTemplates>();
    }

    void OnTriggerStay(Collider other){
        if (other.name == "PlayerObject"){
            if (Input.GetKeyDown(KeyCode.E) && roomTemplates.win){
                gg = true;
            }
            if (gg){
                LeftDoor.transform.rotation = Quaternion.Slerp(LeftDoor.transform.rotation, Quaternion.Euler(0,195,0), 0.3f * Time.deltaTime);
                RightDoor.transform.rotation = Quaternion.Slerp(RightDoor.transform.rotation, Quaternion.Euler(0,-15,0), 0.3f * Time.deltaTime);
                Invoke("ChangeScene", 4f);
            }
        }
    }

    public void ChangeScene ()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(6);
    }

    public void showExit(){
        StartCoroutine(lightTimer(1f));
    }

    IEnumerator lightTimer(float time){
        while (Cursor.lockState == CursorLockMode.Locked){
            yield return new WaitForSeconds(time);
            pointLight.SetActive(!pointLight.activeSelf);
            tracker.SetActive(!tracker.activeSelf);
        }
    }
}
