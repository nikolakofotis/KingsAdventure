using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingTextScript : MonoBehaviour
{
    private TextMeshProUGUI floatingText;
    public string sentences;
    private bool enter;
    private Animator textAnimator;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        enter = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        textAnimator = GameObject.Find("FText").GetComponent<Animator>();
        floatingText = GameObject.Find("FText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && enter)
        {
            SoundSystem.PlaySound(clip, false,0.7f, source);
            enter = false;
            DisplaySentences();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            textAnimator.SetBool("end", true);
        }


    }

    private void  DisplaySentences()
    {
        
       
        

            floatingText.text = sentences;
            textAnimator.SetBool("start", true);
            
           



        
    }

}
