using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XScientistMove : MonoBehaviour
{
    public float speed;
    public float limit;
    public bool xForward;

    private Vector3 original;

    void Start()
    {
        original = transform.position;
    }

    void Update()
    {
        if (xForward)
        {
            if (transform.position.x - original.x >= limit)
            {
                transform.position = original;
            }
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, original.y, original.z);
        }
        else
        {
            if (transform.position.x - original.x <= limit)
            {
                transform.position = original;
            }
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, original.y, original.z);
        }
        
    }
}
