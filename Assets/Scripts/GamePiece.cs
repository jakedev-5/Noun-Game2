using UnityEngine;
using System.Collections;
using System;

public class GamePiece : MonoBehaviour
{
    Rigidbody rigidbody; // Hold reference to GamePiece's Rigidbody
    GrabbableObject grab; // Hold reference to GamePiece's GrabbableObject script
    Collider collider; // Hold reference to GamePiece's Collider
    public String word; // Save the word shown on the GamePiece


    public Vector3 initialPosition; // Initial position, used to return the GamePiece if answered incorrectly
    String initialTag; // Save initial tag as string
    public String texturePath; // Path of the GamePiece's texture
    public String audioPath; // Path of the audio path, corresponding to "word"
    public AudioClip wordClip; // Loaded audio file from audioPath
    public bool grabbed;//Rewind Time!
    public bool answered;//No it is not Rewind Time
    void Start()
    {
        // Load the word audio file from the audioPath
        wordClip = Resources.Load("Audio/" + audioPath, typeof(AudioClip)) as AudioClip;

        // Load the Material from texturePath
        Material material = Resources.Load("Materials/" + texturePath, typeof(Material)) as Material;

        rigidbody = GetComponent<Rigidbody>(); // Save reference to Rigidbody

        GetComponent<MeshRenderer>().material = material; // Set the loaded Material to our GamePiece

        initialTag = tag; // Save our tag
        
        // Base GamePiece off of Spawner, so we can easily deside the Transform of our GamePiece by modifying Spawner 
        initialPosition = GameObject.Find("Spawner").transform.localPosition; // Save initial position to Spawner position
        transform.position = GameObject.Find("Spawner").transform.position; // Set GamePiece's position to Spawner position
        transform.localScale = GameObject.Find("Spawner").transform.localScale; // Set GamePiece's scale to Spawner scale

        grab = GetComponent<GrabbableObject>(); // Get a reference to the GamePiece's GrabbableObject script
        collider = GetComponent<BoxCollider>(); // Get a reference to the GamePiece's Collider
        Freeze(); // Call the Freeze function shown previously
    }

    void Update() // Called every frame, typically 60 to 90 times a second
    {
        if (grabbed = true && rigidbody.isKinematic) // If the Leap Motion hand controller is grabbing
        {
            UnFreeze(); // Call the UnFreeze function shown below

        }
    }

    void Freeze()
    {
        // Set the GamePiece to receive forces from other objects
        rigidbody.isKinematic = true;
    }

    void UnFreeze()
    {
        // Set the GamePiece to not receive forces from other objects
        rigidbody.isKinematic = false;
    }

    public void deactivate()
    {
        rigidbody.detectCollisions = false; // Do not detect collision on the GameObject
        collider.enabled = true; // Reenable the trigger

        Freeze(); // Call the Freeze function shown above
    }

    public void deleteTag()
    {
        tag = "Untagged"; // Set tag from "Person" / "Place" / "Thing" to "Untagged"
    }

    public void reinitializeTag()
    {
        tag = initialTag; // Reset tag to its original value
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