using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private GameObject cube;
    private float duration = 0.5f;
    private float time;
    private Color color0 = Color.red;
    private Color color1 = Color.blue;
    private Light lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = transform.GetChild(0).gameObject.GetComponent<Light>();
        lights.enabled = false;
    }

    void Update(){
        if (RoomTemplates.isMapFinished){
            if (!cube){
                cube = GameObject.Find("CubeObject");
            }
            if (Vector3.Distance(cube.transform.position, transform.position) < 30){
                lights.enabled = true;
                time = Mathf.PingPong(Time.time, duration) / duration;
                lights.color = Color.Lerp(color0, color1, time);
            }
            else {
                lights.enabled = false;
            }
        }
    }

}
