using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MathGame_LevelManager : MonoBehaviour
{
    public const string xmlPath = "MathQuestions";
    public MathProblemContainer problemsCollection;
    public int initialProblemCapacity;

    public BoardCollisionMathGame answerblank;

    // AnswerPiece Prefabs
    public GameObject answerPiece;
    public Transform initialTransform;

    public ParticleSystem chalkDustParticleSystem;

    // Object declared as a singleton, can be accessed using LevelManager.Instance from anywhere
    private static MathGame_LevelManager instance = null;
    public static MathGame_LevelManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        problemsCollection = MathProblemContainer.Load(xmlPath);
        initialProblemCapacity = problemsCollection.problems.Count;

        StartCoroutine(spawnAnswerPiece());
    }

    void Update()
    {

    }

    // Ensures two copies of the singleton do not exist
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void playChalkDust()
    {
        chalkDustParticleSystem.Clear();
        chalkDustParticleSystem.Simulate(chalkDustParticleSystem.duration);
        chalkDustParticleSystem.Play();
    }

    public IEnumerator spawnAnswerPiece()
    {
        Vector3 spawnPosition = GameObject.Find("answerPieceSpawner").transform.position;
        if (problemsCollection.problems.Count != 0)
        {
            // Choose index of word from <wordCollection.words>
            int chosenIndex = Random.Range(0, problemsCollection.problems.Count);



            answerblank.answernumber = problemsCollection.problems[chosenIndex].correctAnswerIndex;
            answerblank.questionLeadingAudio = problemsCollection.problems[chosenIndex].questionLeadingAudio;

            answerblank.questionLeadingAudio2 = Resources.Load("Audio/Fill BLANK/" + answerblank.questionLeadingAudio, typeof(AudioClip)) as AudioClip;

            // Initialize new fillblank_AnswerPiece using info from <problemsCollection.problems[chosenIndex]>
            GameObject cloneM = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneM.transform.Translate(0.1f, -0.1f, 0.0f);
            MathGameGamePiece cloneScriptM = cloneM.GetComponent<MathGameGamePiece>();
            cloneM.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer2;
            Debug.Log(problemsCollection.problems[chosenIndex].answer2);

            GameObject cloneL = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneL.transform.Translate(0.1f, -0.1f, 0.4f);
            MathGameGamePiece cloneScriptL = cloneL.GetComponent<MathGameGamePiece>();

            cloneL.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer1;
            Debug.Log(problemsCollection.problems[chosenIndex].answer1);

            GameObject cloneR = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneR.transform.Translate(0.1f, -0.1f, -0.4f);
            MathGameGamePiece cloneScriptR = cloneR.GetComponent<MathGameGamePiece>();

            cloneR.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer3;
            Debug.Log(problemsCollection.problems[chosenIndex].answer3);

            GameObject cloneTM = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneTM.transform.Translate(0.1f, 0.2f, 0.0f);
            MathGameGamePiece cloneScriptTM = cloneTM.GetComponent<MathGameGamePiece>();

            cloneTM.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer5;
            Debug.Log(problemsCollection.problems[chosenIndex].answer5);

            GameObject cloneTL = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneTL.transform.Translate(0.1f, 0.2f, 0.4f);
            MathGameGamePiece cloneScriptTL = cloneTL.GetComponent<MathGameGamePiece>();

            cloneTL.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer4;
            Debug.Log(problemsCollection.problems[chosenIndex].answer4);

            GameObject cloneTR = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            cloneTR.transform.Translate(0.1f, 0.2f, -0.4f);
            MathGameGamePiece cloneScriptTR = cloneTR.GetComponent<MathGameGamePiece>();

            cloneTR.GetComponent<TextMesh>().text = problemsCollection.problems[chosenIndex].answer6;
            Debug.Log(problemsCollection.problems[chosenIndex].answer6);

            cloneScriptM.answer = problemsCollection.problems[chosenIndex].answer2;
            cloneScriptL.answer = problemsCollection.problems[chosenIndex].answer1;
            cloneScriptR.answer = problemsCollection.problems[chosenIndex].answer3;
            cloneScriptTL.answer = problemsCollection.problems[chosenIndex].answer4;
            cloneScriptTM.answer = problemsCollection.problems[chosenIndex].answer5;
            cloneScriptTR.answer = problemsCollection.problems[chosenIndex].answer6;

            answerblank.answernumber = problemsCollection.problems[chosenIndex].correctAnswerIndex;
            answerblank.questionLeadingAudio = problemsCollection.problems[chosenIndex].questionLeadingAudio;

            answerblank.questionLeadingAudio2 = Resources.Load("Audio/Fill BLANK/" + answerblank.questionLeadingAudio, typeof(AudioClip)) as AudioClip;

            switch (problemsCollection.problems[chosenIndex].correctAnswerIndex)
            {
                case 1:
                    cloneScriptL.correctAnswer = true;
                    break;
                case 2:
                    cloneScriptM.correctAnswer = true;
                    break;
                case 3:
                    cloneScriptR.correctAnswer = true;
                    break;
                case 4:
                    cloneScriptTL.correctAnswer = true;
                    break;
                case 5:
                    cloneScriptTM.correctAnswer = true;
                    break;
                case 6:
                    cloneScriptTR.correctAnswer = true;
                    break;
            }

            GameObject.Find("Blackboard").GetComponentInChildren<TextMesh>().text = problemsCollection.problems[chosenIndex].questionLeading + " ___ ";

            // Remove <chosenIndex> element from <problemsCollection.problems>, since this fillblank_AnswerPiece has already been played
            problemsCollection.problems.RemoveAt(chosenIndex);

            yield return new WaitForSeconds(2.0f);
        }
    }

    public void restartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}