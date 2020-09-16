using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongEnd : MonoBehaviour
{

    public AudioSource DarkDecks;

    // Start is called before the first frame update
    void Start()
    {
        DarkDecks = GetComponent<AudioSource>();
        DarkDecks.Play(0);
        StartCoroutine(CreditsMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator CreditsMusic()
    {
        
        yield return new WaitForSeconds(133);
        SceneManager.LoadScene(0);
    }

}
