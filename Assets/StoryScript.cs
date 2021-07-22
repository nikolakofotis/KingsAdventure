using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    private bool close;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        close = false; 
    }

   
    void Update()
    {
        
    }

    public void DisableStory()
    {
        SoundSystem.PlaySound(clip, false, source);
        close = true;
        gameObject.SetActive(false); 
        
    }

    public IEnumerator RemoteDisable(GameObject objToDis)
    {
        yield return new WaitUntil(() => close);
        objToDis.SetActive(false);
        SoundSystem.PlaySound(clip, false, source);
    }
}
