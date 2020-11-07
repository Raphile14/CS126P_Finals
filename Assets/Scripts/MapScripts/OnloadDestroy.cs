using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnloadDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("BackgroundMusic"));
    }
}
