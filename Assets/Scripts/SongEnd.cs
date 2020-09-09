using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEnd : MonoBehaviour
{

    public AudioSource DarkDecks;

    // Start is called before the first frame update
    void Start()
    {
        CreditsMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator CreditsMusic()
    {
        DarkDecks.Play();
        yield return new WaitForSeconds(1);
        Debug.Log("Anti-Universe");
    }

}
