using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathGameSoundManager1 : MonoBehaviour
{
    public AudioClip goodJob1;
    public AudioClip WrongOneKid1;

    public AudioClip goodJob2;
    public AudioClip WrongOneKid2;

    public AudioClip goodJob3;
    public AudioClip WrongOneKid3;

    public AudioClip goodJob4;
    public AudioClip WrongOneKid4;

    public AudioClip goodJob5;
    public AudioClip WrongOneKid5;

    public AudioClip goodJob6;
    public AudioClip WrongOneKid6;

    public AudioClip goodJob7;
    public AudioClip WrongOneKid7;

    public AudioClip goodJob8;
    public AudioClip WrongOneKid8;

    public AudioClip goodJob9;
    public AudioClip WrongOneKid9;

    public AudioClip goodJob10;
    public AudioClip WrongOneKid10;

    public AudioClip goodJob11;
    public AudioClip WrongOneKid11;

    public AudioClip goodJob12;
    public AudioClip WrongOneKid12;

    public AudioClip goodJob13;
    public AudioClip WrongOneKid13;

    public AudioClip goodJob14;
    public AudioClip WrongOneKid14;

    public AudioClip WrongOneKid15;

    public AudioClip is1;
    public AudioClip am1;
    public AudioClip are1;

    public AudioClip gameOver;

    public AudioSource jukebox;

    public AnswerMath answerblank;

    private static MathGameSoundManager1 instance = null;
    public static MathGameSoundManager1 Instance
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

   // public IEnumerator playCorrectAudio(AudioClip ql, AudioClip qt)
  //  {
        // Get reference to the SoundManager's Audio Source component
    //    jukebox = this.gameObject.GetComponent<AudioSource>();

        // Play "Good Job!" audio clip
     //   jukebox.PlayOneShot(goodJob);

        // Wait.. 
     //   yield return new WaitForSeconds(goodJob.length + 0.7f);

        // Play "<word> " audio clip
    //    jukebox.PlayOneShot(ql);

        // Wait..
   //     yield return new WaitForSeconds(ql.length);

        // Play "is a (Person/Place/Thing)." audio clip
        //if (answerblank.answernumber == 1)
   //     {
      //      jukebox.PlayOneShot(is1);
    //    }
     //   else if (answerblank.answernumber == 2)
   //     {
  //          jukebox.PlayOneShot(am1);
  //      }
    //    else if (answerblank.answernumber == 3)
       // {
    //        jukebox.PlayOneShot(are1);
 //       }
        // Play "<word> " audio clip
   //     jukebox.PlayOneShot(qt);

        // Wait..
     //   yield return new WaitForSeconds(qt.length);
//    }

   // public IEnumerator playWrongAudio()
 //   {
 //       jukebox.PlayOneShot(WrongOneKid);
  //      yield return new WaitForSeconds(0.1f);
  //  }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
