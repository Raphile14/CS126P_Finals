using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScriptSample : MonoBehaviour
{
    public void onClick(){
        GetComponent<Collider>().isTrigger = true;
    }
}
