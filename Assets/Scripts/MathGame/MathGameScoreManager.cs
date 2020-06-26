using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathGameScoreManager : MonoBehaviour
{
    public static int score;
    bool onetime = false;
    public TextMesh title;

    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        title.text = "Score:  " + score;
    }
}
