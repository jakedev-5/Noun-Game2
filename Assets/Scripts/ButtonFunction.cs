using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{

    public MathGame_LevelManager FallenKingdom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FallenKingdom = GameObject.Find("Dadsuki").GetComponent<MathGame_LevelManager>();
    }
    public void restartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        StartCoroutine(FallenKingdom.spawnAnswerPiece());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
    public void ChangeScene2()
    {
        SceneManager.LoadScene(4);
    }
    public void ChangeScene3()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene(5);
    }
    public void ChangeScene5()
    {
        SceneManager.LoadScene(6);
    }
    public void ChangeScene6()
    {
        SceneManager.LoadScene(7);
    }
}
