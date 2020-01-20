using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class AnswerBin : MonoBehaviour
{ 
    public enum AnswerType
    {
        none,
        PERSON,
        PLACE,
        THING
    };

    // Keep a reference to frequently used BoxCollider
    BoxCollider triggerCollider;

    void Start() // Called when AnswerBin is initialized
    {
        triggerCollider = GetComponent<BoxCollider>(); // Save reference to Box Collider Trigger
    }

    void OnTriggerEnter(Collider gamePiece) // Called when Box Collider Trigger interacts with another collider
    {
        // Get reference to the other.GamePiece script
        GamePiece gamePieceRef = gamePiece.gameObject.GetComponent<GamePiece>();
       // var grab = gamePiece.gameObject.GetComponent<GrabbableObject>();
        // Temporarily disable the Collider trigger to avoid multiple scoring
        triggerCollider.enabled = false;

        //Debug.Log(gamePieceRef.tag);
        if (gamePieceRef != null)
        {
            bool grab = gamePieceRef.grabbed;
            gamePiece.GetComponent<Rigidbody>().useGravity = false;
            if (!grab)
           {
                gamePieceRef.answered = true;
                switch (gamePieceRef.tag.ToLower()) // Compare the answerbox's expected tag (ToLower() makes this case-insensitive)
                {
                    case "untagged":
                        break;
                    case "person": // answer box accepts "person" type
                        if (gamePieceRef.CompareTag(tag)) // We know the GamePiece.AnswerType, now see if it was placed in correct AnswerBin
                        {
                            // correct answer, congratulate and delete game piece
                            StartCoroutine(CorrectAnswer(gamePiece, gamePieceRef, AnswerType.PERSON));
                        }
                        else
                        {  // wrong answer
                            StartCoroutine(WrongAnswer(gamePiece, gamePieceRef, TagToAnswerType(tag)));
                        }
                        break;

                    case "place":
                        if (gamePieceRef.CompareTag(tag)) // We know the GamePiece.AnswerType, now see if it was placed in correct AnswerBin
                        {
                            // correct answer, congratulate and delete game piece
                            StartCoroutine(CorrectAnswer(gamePiece, gamePieceRef, AnswerType.PLACE));
                        }
                        else
                        {  // wrong answer
                            StartCoroutine(WrongAnswer(gamePiece, gamePieceRef, TagToAnswerType(tag)));
                        }
                        break;

                    case "thing":
                        if (gamePieceRef.CompareTag(tag))    // We know the GamePiece.AnswerType, now see if it was placed in correct AnswerBin
                        {
                            // correct answer, congratulate and delete game piece
                            StartCoroutine(CorrectAnswer(gamePiece, gamePieceRef, AnswerType.THING));
                        }
                        else
                        {  // wrong answer
                            StartCoroutine(WrongAnswer(gamePiece, gamePieceRef, TagToAnswerType(tag)));
                        }
                        break;

                    default:
                        break;
                }

                // Disable tag (temporarily if wrong) to avoid 
                if (gamePieceRef.tag != "Untagged")
                {
                    gamePieceRef.deleteTag();
                }
           }
        }

        // reenable the Collider trigger to avoid multiple scoring
        triggerCollider.enabled = true;
    }

    private IEnumerator CorrectAnswer(Collider collider, GamePiece gamePiece, AnswerType correctType)
    {
        // Play audio sequence "Good Job, <word> is a <type>"
        StartCoroutine(SoundManager.Instance.playCorrectAudio(gamePiece.wordClip, correctType));
        ScoreManager.score++;

        // Reference the stacker child object, used to lerp correct GamePieces into a stack
        Transform stacker = transform.Find("Stacker");

        collider.gameObject.transform.position = stacker.transform.position;
        collider.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        // Freeze GamePiece now that its in place
        collider.GetComponent<Rigidbody>().isKinematic = true;
        collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Destroy(collider.GetComponent<GrabbableObject>());

        yield return new WaitForSeconds(gamePiece.wordClip.length + 2.5f);

        if (LevelManager.stackCubes == false) {
            Destroy(collider.gameObject);
            Destroy(gamePiece);
        } else
        {
            // Move the stacker reference up so the next objects stacks on top
            stacker.Translate(0.0f, gamePiece.transform.localScale.y, 0.0f, Space.World);
        }

        StartCoroutine(LevelManager.Instance.spawnGamePiece(gamePiece.initialPosition));
    }

    private IEnumerator WrongAnswer(Collider collider, GamePiece gamePiece, AnswerType wrongType)
    {
        // Get reference to the other.GamePiece script
        GamePiece gamePieceRef = gamePiece.gameObject.GetComponent<GamePiece>();
        gamePieceRef.answered = true;

        // Play audio sequence "<word> is not a <type>"
        StartCoroutine(SoundManager.Instance.playWrongAudio(gamePiece.wordClip, wrongType));

        // Reference the stacker child object, used to lerp correct GamePieces into a stack
        Transform stacker = transform.Find("Stacker");

        collider.gameObject.transform.position = GameObject.Find("Spawner").transform.position;
        collider.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        // Freeze GamePiece now that it has been tweened
        collider.GetComponent<Rigidbody>().isKinematic = true;
        collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(gamePiece.wordClip.length + 1.5f);

        

        // UnFreeze GamePiece now that its back in play
        collider.GetComponent<Rigidbody>().isKinematic = false;
        collider.GetComponent<Rigidbody>().useGravity = true;

        //yield return new WaitForSeconds(1.0f);

        // Set the gamePiece's tag back to its initial value that way we can try again
        gamePiece.reinitializeTag();
        gamePiece.answered = false;
    }

    AnswerType TagToAnswerType(string tag)
    {
        switch (tag)
        {
            case "Person":
                return AnswerType.PERSON;
            case "Place":
                return AnswerType.PLACE;
            case "Thing":
                return AnswerType.THING;
        }

        Debug.Log("AnswerBin.TagToAnswerType() could not match the provided tag: " + tag);
        return AnswerType.none;
    }

}
