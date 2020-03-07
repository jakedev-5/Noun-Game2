using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMovment : MonoBehaviour
{
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(-speed, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(speed, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            transform.position += transform.TransformDirection(0, 0, -speed);
        }
        if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(0, 0, speed);
        }
        
    }
}
