using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillblankSoundManager : MonoBehaviour
{
    public AudioClip goodJob;
    public AudioClip WrongOneKid;

    public AudioClip is1;
    public AudioClip am1;
    public AudioClip are1;

    public AudioClip gameOver;

    public AudioSource jukebox;

    public fillblank_AnswerBlank answerblank;

    private static FillblankSoundManager instance = null;
    public static FillblankSoundManager Instance
    {
        get { return instance; }
    }

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

    public IEnumerator playCorrectAudio(AudioClip ql, AudioClip qt)
    {
        // Get reference to the SoundManager's Audio Source component
        jukebox = this.gameObject.GetComponent<AudioSource>();

        // Play "Good Job!" audio clip
        jukebox.PlayOneShot(goodJob);

        // Wait.. 
        yield return new WaitForSeconds(goodJob.length + 0.7f);

        // Play "<word> " audio clip
        jukebox.PlayOneShot(ql);

        // Wait..
        yield return new WaitForSeconds(ql.length);

        // Play "is a (Person/Place/Thing)." audio clip
        if (answerblank.answernumber == 1)
        {
            jukebox.PlayOneShot(is1);
        }
        else if (answerblank.answernumber == 2)
        {
            jukebox.PlayOneShot(am1);
        }
        else if (answerblank.answernumber == 3)
        {
            jukebox.PlayOneShot(are1);
        }
        // Play "<word> " audio clip
        jukebox.PlayOneShot(qt);

        // Wait..
        yield return new WaitForSeconds(qt.length);
    }

    public IEnumerator playWrongAudio()
    {
        jukebox.PlayOneShot(WrongOneKid);
        yield return new WaitForSeconds(0.1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
