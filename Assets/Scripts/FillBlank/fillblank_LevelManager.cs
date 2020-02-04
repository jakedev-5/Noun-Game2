using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fillblank_LevelManager : MonoBehaviour
{
    public const string xmlPath = "fillblank_ProblemList";
    public fillblank_ProblemsContainer problemsCollection;
    public int initialProblemCapacity;

    public fillblank_AnswerBlank answerblank;

    // AnswerPiece Prefabs
    public GameObject answerPiece;
    public Transform initialTransform;

    public ParticleSystem chalkDustParticleSystem;

    // Object declared as a singleton, can be accessed using LevelManager.Instance from anywhere
    private static fillblank_LevelManager instance = null;
    public static fillblank_LevelManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        problemsCollection = fillblank_ProblemsContainer.Load(xmlPath);
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

            answerblank.questionnumber = chosenIndex;

            // Initialize new fillblank_AnswerPiece using info from <problemsCollection.problems[chosenIndex]>
            GameObject cloneM = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 270.0f, 0.0f)));
            fillblank_AnswerPiece cloneScriptM = cloneM.GetComponent<fillblank_AnswerPiece>();

            GameObject cloneL = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 270.0f, 0.0f)));
            cloneL.transform.Translate(-0.3f, 0.0f, 0.0f);
            fillblank_AnswerPiece cloneScriptL = cloneL.GetComponent<fillblank_AnswerPiece>();

            GameObject cloneR = (GameObject)Instantiate(answerPiece, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 270.0f, 0.0f)));
            cloneR.transform.Translate(0.3f, 0.0f, 0.0f);
            fillblank_AnswerPiece cloneScriptR = cloneR.GetComponent<fillblank_AnswerPiece>();

            cloneScriptM.answer = problemsCollection.problems[chosenIndex].answer2;
            cloneScriptL.answer = problemsCollection.problems[chosenIndex].answer1;
            cloneScriptR.answer = problemsCollection.problems[chosenIndex].answer3;

            switch (problemsCollection.problems[chosenIndex].correctAnswerIndex) {
                case 1:
                    cloneScriptL.correctAnswer = true;
                    break;
                case 2:
                    cloneScriptM.correctAnswer = true;
                    break;
                case 3:
                    cloneScriptR.correctAnswer = true;
                    break;
            }

            GameObject.Find("Blackboard").GetComponentInChildren<TextMesh>().text = problemsCollection.problems[chosenIndex].questionLeading + " ___ " + problemsCollection.problems[chosenIndex].questionTrailing;

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