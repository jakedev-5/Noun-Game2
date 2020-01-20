using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Code : MonoBehaviour
{
    int one;
    int two;
    int three;
    int four;

    public Text textone;
    public Text texttwo;
    public Text textthree;
    public Text textfour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        textone.text = one.ToString();
        texttwo.text = two.ToString();
        textthree.text = three.ToString();
        textfour.text = four.ToString();

        if (one == 3)
        {
            if (two == 5)
            {
                if (three == 7)
                {
                    if (four == 1)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
            }

        }
    }
     public void onepress()
    {
        if(one > 8)
        {
            one = 0;
        }
        else {
            one++;
        }
    }
     public void twopress()
    {
        if (two > 8)
        {
            two = 0;
        }
        else {
            two++;
        }
    }
    public void threepress()
    {
        if (three > 8)
        {
            three = 0;
        }
        else {
            three++;
        }
    }
    public void fourpress()
    {
        if (four > 8)
        {
            four = 0;
        }
        else {
            four++;
        }
    }
}
