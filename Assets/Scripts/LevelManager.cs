using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public const string xmlPath = "WordList";
    public WordContainer wordCollection; // Reference to our WordContainer
    public int initialWordCapacity; // Amount of words in game

    // GamePiece Prefabs
    public GameObject prefabGamePiece;
    public GameObject prefabTextGamePiece;
    public bool useTextPrefab = true;

    // The selected prefab
    public GameObject gamePiece;
    public Transform initialTransform;

    // Whether or not to stack cubes in AnswerBin
    public static bool stackCubes = false;

    // Object declared as a singleton, can be accessed using LevelManager.Instance from anywhere
    private static LevelManager instance = null;
    public static LevelManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        // Retrieve User preferences
        useTextPrefab = System.Convert.ToBoolean(PlayerPrefs.GetInt("useTextPrefab"));
        stackCubes = System.Convert.ToBoolean(PlayerPrefs.GetInt("stackCubes"));
        if (useTextPrefab)
        {
            gamePiece = prefabTextGamePiece;
        } else
        {
            gamePiece = prefabGamePiece;
        }

        // Load WordContainer and store in <wordCollection>
        wordCollection = WordContainer.Load(xmlPath);

        // Set to amount of words found in <wordCollection>
        initialWordCapacity = wordCollection.words.Count;

        // Spawn the first GamePiece at specified Position
        StartCoroutine(spawnGamePiece(new Vector3(0.0f, 2.135f, -5.5f)));
    }

    void Update()
    {
      
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            useTextPrefab = !useTextPrefab;
            PlayerPrefs.SetInt("useTextPrefab", System.Convert.ToInt32(useTextPrefab));
        }
        if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            stackCubes = !stackCubes;
            PlayerPrefs.SetInt("stackCubes", System.Convert.ToInt32(stackCubes));
        }
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

    public IEnumerator spawnGamePiece(Vector3 initialPosition)
    {
        if (wordCollection.words.Count != 0)
        {
            // Choose index of word from <wordCollection.words>
            int chosenIndex = Random.Range(0, wordCollection.words.Count);

            // Initialize new GamePiece using info from <wordCollection.words[chosenIndex]>
            GameObject clone = (GameObject)Instantiate(gamePiece, initialPosition, Quaternion.Euler(new Vector3(270.0f, 135.0f, 0.0f)));
            GamePiece cloneScript = clone.GetComponent<GamePiece>();

            //cloneScript.initialPosition = initialTransform.position;
            cloneScript.name = wordCollection.words[chosenIndex].word;
            cloneScript.tag = wordCollection.words[chosenIndex].initialTag;
            cloneScript.texturePath = wordCollection.words[chosenIndex].texturePath;
            cloneScript.audioPath = wordCollection.words[chosenIndex].wordClipPath;

            // Remove <chosenIndex> element from <wordCollection.words>, since this GamePiece has already been played
            wordCollection.words.RemoveAt(chosenIndex);

            yield return new WaitForSeconds(2.0f);
        }
    }


}