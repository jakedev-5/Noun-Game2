using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CODING : MonoBehaviour

{
    int one;
    int two;
    int three;

    public Text textone;
    public Text texttwo;
    public Text textthree;
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

        if (one == 9)
        {
            if (two == 0)
            {
                if (three == 8)
                {
                    SceneManager.LoadScene(8);
                }
            }

        }
    }
    public void onepress()
    {
        if (one > 8)
        {
            one = 0;
        }
        else
        {
            one++;
        }
    }
    public void twopress()
    {
        if (two > 8)
        {
            two = 0;
        }
        else
        {
            two++;
        }
    }
    public void threepress()
    {
        if (three > 8)
        {
            three = 0;
        }
        else
        {
            three++;
        }
    }
}