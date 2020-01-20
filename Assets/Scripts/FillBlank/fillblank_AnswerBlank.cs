﻿using UnityEngine;
using System.Collections;

public class fillblank_AnswerBlank : MonoBehaviour
{
    // Keep a reference to frequently used BoxCollider
    BoxCollider triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider answerPiece)
    {
        // Get reference to the other.answerPiece script
        fillblank_AnswerPiece answerPieceRef = answerPiece.gameObject.GetComponent<fillblank_AnswerPiece>();
        var grab = answerPiece.gameObject.GetComponent<GrabbableObject>();
        // Temporarily disable the Collider trigger to avoid multiple scoring
        triggerCollider.enabled = false;

        //Debug.Log(answerPieceRef.tag);
        if (answerPieceRef != null && grab != null)
        {
            if (!grab.IsGrabbed())
            {
                switch (answerPieceRef.tag.ToLower()) // Compare the answerbox's expected tag (ToLower() makes this case-insensitive)
                {
                    case "answer":
                        if (answerPieceRef.correctAnswer)
                        {
                            StartCoroutine(CorrectAnswer(answerPiece, answerPieceRef));
                        }
                        else
                        {
                            StartCoroutine(WrongAnswer(answerPiece, answerPieceRef));
                        }
                        break;
                    case "untagged":
                        break;
                    default:
                        break;
                }
            }
        }

        // reenable the Collider trigger to avoid multiple scoring
        triggerCollider.enabled = true;
    }

    private IEnumerator CorrectAnswer(Collider collider, fillblank_AnswerPiece answerPiece)
    {
        // Play audio sequence "Good Job, <word> is a <type>"
//        StartCoroutine(fillblank_SoundManager.Instance.playCorrectAudio());
        fillblank_ScoreManager.score++;

        // Reference the stacker child object, used to lerp correct answerPieces into a stack
        Transform blackboardTransform = GameObject.Find("Blackboard/Chalkboard Question").transform;

        // Move correct answerPiece into stacker position and rotate towards user
        iTween.MoveTo(answerPiece.gameObject, iTween.Hash("position", blackboardTransform.position, "easetype", iTween.EaseType.easeInOutSine, "time", 0.5f));
        // Set Stackers rotation accordingly each time (using some function(increment <i>)), then set answerPiece rotation to this, rather than stacking Vector3(0, 270, 0)
        iTween.RotateTo(answerPiece.gameObject, iTween.Hash("rotation", new Vector3(0.0f, 270.0f, 0.0f), "easetype", iTween.EaseType.easeInOutSine, "time", 0.5f));

        // Freeze answerPiece now that its in place
        collider.GetComponent<Rigidbody>().isKinematic = true;
        collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Destroy(collider.GetComponent<GrabbableObject>());

        yield return new WaitForSeconds(0.5f);

        TextMesh chalkboardQuestion = GameObject.Find("Blackboard/Chalkboard Question").GetComponent<TextMesh>();
        chalkboardQuestion.text = chalkboardQuestion.text.Replace("___", answerPiece.answer);

        fillblank_LevelManager.Instance.playChalkDust();


        Destroy(collider.gameObject); // Delete correct answer

        // Delete wrong answers
        GameObject[] toKill = GameObject.FindGameObjectsWithTag("Answer");
        foreach (GameObject iterator in toKill)
        {
            Destroy(iterator);    
        }

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(fillblank_LevelManager.Instance.spawnAnswerPiece());
    }

    private IEnumerator WrongAnswer(Collider collider, fillblank_AnswerPiece answerPiece)
    {
        // Play audio sequence "Wrong!"
        //        StartCoroutine(fillblank_SoundManager.Instance.playCorrectAudio());

        yield return new WaitForSeconds(0.75f);

        // Move wrong answerPiece into initial position and rotate towards user (reset)
        iTween.MoveTo(answerPiece.gameObject, iTween.Hash("position", answerPiece.initialPosition, "easetype", iTween.EaseType.easeInOutSine, "time", 0.5f));
        // Set Stackers rotation accordingly each time (using some function(increment <i>)), then set answerPiece rotation to this, rather than stacking Vector3(0, 270, 0)
        iTween.RotateTo(answerPiece.gameObject, iTween.Hash("rotation", new Vector3(0.0f, 270.0f, 0.0f), "easetype", iTween.EaseType.easeInOutSine, "time", 0.5f));

        // Freeze answerPiece now that its in place
        collider.GetComponent<Rigidbody>().isKinematic = true;
        collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(2.5f);

        // UnFreeze GamePiece now that its back in play
        collider.GetComponent<Rigidbody>().isKinematic = false;
    }

}
