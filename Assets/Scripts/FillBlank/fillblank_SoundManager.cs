using UnityEngine;
using System.Collections;

public class fillblank_SoundManager : MonoBehaviour
{
    public AudioClip goodJob;

    // Object declared as a singleton, can be accessed using fillblank_SoundManager.Instance from anywhere
    private static fillblank_SoundManager instance = null;
    public static fillblank_SoundManager Instance
    {
        get { return instance; }
    }

    // Ensures two copies of the singleton do not exist
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

    public IEnumerator playCorrectAudio(AudioClip questionLeadingAudio2, AudioClip questionTrailingAudio2)
    {
        AudioSource jukebox = this.gameObject.GetComponent<AudioSource>();

        // "Good Job!"
        jukebox.PlayOneShot(goodJob);

        yield return new WaitForSeconds(goodJob.length + 0.5f);
    }
}
