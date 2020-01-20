using UnityEngine;
using System.Collections;

public class fillblank_ScoreManager : MonoBehaviour {

    public static int score;
    bool onetime = false;
    public TextMesh title;

    // Use this for initialization
    void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        title.text = "Score:  " + score;
	}
}
