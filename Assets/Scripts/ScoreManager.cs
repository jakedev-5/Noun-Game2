using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public static int score;
    public Text scoreText;
    public ParticleSystem fireworks1;
    public ParticleSystem fireworks2;
    public ParticleSystem fireworks3;
    bool onetime = false;


    //Use this for initialization
    void Start() { 
    
       score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:  " + score;
            if (!onetime && score == LevelManager.Instance.initialWordCapacity)
            {
                onetime = true;
                StartCoroutine(PlayParticles());

            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                SceneManager.LoadScene(12);
                Debug.Log("Hong Kong 2020");
            }

            }
        if (score == 6)
        {
            SceneManager.LoadScene(0);
        }
    }

    public IEnumerator PlayParticles()
    {
        yield return new WaitForSeconds(3.5f);

        fireworks1.Play();
        fireworks2.Play();
        fireworks3.Play();
        KillParticles();

        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.playGameOver();
    }

    public IEnumerator KillParticles()
    {
        yield return new WaitForSeconds(1.0f);
        StopParticles();
    }

    public void StopParticles()
    {

        fireworks1.Clear();
        fireworks2.Clear();
        fireworks3.Clear();
    }
       
}
