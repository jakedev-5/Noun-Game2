using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour
{

    public float rotatespeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -0.5f * rotatespeed, 0);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0, 0.5f * rotatespeed, 0);
        }

    }
}
