using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUp : MonoBehaviour
{

    Vector3 pos;

    // Start is alledc before the first frame update
    void Start()
    {
        Vector3 pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.y += .40f;
        transform.position = pos;
        
    }
}
