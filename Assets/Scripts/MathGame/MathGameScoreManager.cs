using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log(title.text);
    }
}
