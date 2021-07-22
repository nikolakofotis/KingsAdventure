using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeScript : MonoBehaviour
{

    public StoryScript myEnvelope;
    public AudioClip clip;
    public AudioSource source;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


     

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                SoundSystem.PlaySound(clip, false, source);
                myEnvelope.gameObject.SetActive(true);
                StartCoroutine(myEnvelope.RemoteDisable(gameObject));
            }
        
    }


}
