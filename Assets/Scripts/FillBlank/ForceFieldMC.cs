using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldMC : MonoBehaviour
{
     public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        hit = true;
        Debug.Log("Radio Ga Ga");
    }
}
