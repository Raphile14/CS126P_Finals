using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
