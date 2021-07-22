using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCheck : MonoBehaviour
{
    public DialogueScript dialogue;
   
    public string[] sentencesToDisplay;
    public string[] greekSentence;
    private NpcScript npc;
    public AudioSource source;
    public AudioClip clipaki;
    public TMPro.TMP_FontAsset greek, english;
    private bool enterone;
    

    void Start()
    {
        enterone = true;
    }

    
    void FixedUpdate()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.CompareTag("Player") && enterone)
        {

            enterone = false;
                collision.gameObject.GetComponent<PlayerMove>().blockMovement = true;
                collision.gameObject.GetComponent<PlayerMove>().GotDialog(gameObject);
                SoundSystem.PlaySound(clipaki, false, source);
                dialogue.gameObject.SetActive(true);
            if (collision.gameObject.GetComponent<PlayerMove>().language == "el")
            {
                dialogue.SetFont(greek);
                dialogue.SetStrings(greekSentence);
            }
            else
            {
                dialogue.SetFont(english);
                dialogue.SetStrings(sentencesToDisplay);
            }
                StartCoroutine(EnableWait());
                StartCoroutine(WaitForUnblock(collision.gameObject.GetComponent<PlayerMove>()));
            

        }
         

 
    }

    private IEnumerator WaitForUnblock(PlayerMove player)
    {
        yield return new WaitUntil(() => dialogue.ended);
        dialogue.ended = false;
        dialogue.enabled = false;
        player.blockMovement = false;
        gameObject.SetActive(false);
    }

    private IEnumerator EnableWait()
    {
        yield return new WaitForSeconds(0.7f);
        dialogue.enabled = true;
    }


    public void PlayDialogue()
    {
        PlayerMove p = GameObject.Find("Player").GetComponent<PlayerMove>();
       p.gameObject.GetComponent<PlayerMove>().blockMovement = true;
      //  p.gameObject.GetComponent<PlayerMove>().GotDialog(gameObject);
        dialogue.gameObject.SetActive(true);
        if (p.language == "el")
        {
            dialogue.SetFont(greek);
            dialogue.SetStrings(greekSentence);
        }
        else
        {
            dialogue.SetFont(english);
            dialogue.SetStrings(sentencesToDisplay);
        }
        StartCoroutine(EnableWait());
        StartCoroutine(WaitForUnblock(p));
       

    }

    public void GetNpc(NpcScript GetNpc)
    {
        npc = GetNpc;
    }


  
}
