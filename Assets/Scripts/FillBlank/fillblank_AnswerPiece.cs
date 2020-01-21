﻿using UnityEngine;
using System.Collections;

public class fillblank_AnswerPiece : MonoBehaviour {

    public bool grabbed;
    string initialTag;
    Rigidbody physicsBody;
    Collider collision;
    public Vector3 initialPosition;
    public bool answered;
    
    public string answer;
    public bool correctAnswer = false;

	// Use this for initialization
	void Start () {
        GetComponent<MeshFilter>().mesh = Resources.Load("Meshes/" + answer.ToLower(), typeof(Mesh)) as Mesh;
	    initialPosition = transform.position;
	    initialTag = tag;
        physicsBody = GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
	    Physics.IgnoreLayerCollision(8, 8, true); // Ignore collision between AnswerPieces
        Physics.IgnoreLayerCollision(8, 9, true); // Ignore collision between AnswerPiece and Hands

        Freeze();

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(grabbed && physicsBody.isKinematic)
        {
            
            UnFreeze();
        }
	}

    void Freeze()
    {
        physicsBody.isKinematic = true;
    }

    void UnFreeze()
    {
        physicsBody.isKinematic = false;
    }

    //    public void deactivate()
    //    {
    //        physicsBody.detectCollisions = false;
    //        //physicsBody.useGravity = false;
    //        collision.enabled = true;
    //        Freeze();
    //    }

    public void deleteTag()
    {
        tag = "Untagged";
    }

    public void reinitializeTag()
    {
        tag = initialTag;
    }
    private void OnMouseDown()
    {
        grabbed = true;
    }
    private void OnMouseUp()
    {
        grabbed = false;
    }
}
