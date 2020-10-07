using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MathGameScoreManager : MonoBehaviour
{
    public static int score;
    bool onetime = false;
    public Text title;

    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        title.text = "Score:  " + score;
        
        if (!onetime && score == MathGame_LevelManager.Instance.initialProblemCapacity)
        {
            onetime = true;

            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                SceneManager.LoadScene(12);
                
            }

        }
    }
}
