using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    string s;
   public  bool ended, skip;
    private float waitFor;
    public string[] test;
    public bool endedLast;
    public bool enabled;
    
    


   public string[] sentences;
    void Start()
    {
        endedLast = false;
        ended = false;
        waitFor = 0.02f;
        skip = false;
        enabled = false;
        //SetStrings(test);
        //StartCoroutine(DisplayText());

    }


    void FixedUpdate()
    {
        if (enabled)
        {
            SkipText();
        }
    }

    public IEnumerator DisplayText()
    {
         
        
        
        foreach (string s in sentences)
        {
            skip = false;
            
            text.text = "";
            foreach (char c in s.ToCharArray())
            {


                skip = false;
                text.text += c;
               
                
                    yield return new WaitForSeconds(waitFor);
                

            }

            yield return new WaitUntil(() => skip == true);
            
        }
        text.text = "";
        endedLast = true;
        skip = false;


    }

    public void SkipText()
    {
        if (Input.touchCount > 0 && endedLast)
        {
           
            ended = true;
            endedLast = false;
            waitFor = 0.02f;
            sentences = null;
            gameObject.SetActive(false);
        }
        else if (Input.touchCount > 0   && !skip)
        {
            skip = true;
            
        }
        
        
        


    }

    public void SetStrings(string[] sentence)
    {

        sentences= sentence;
        StartCoroutine(DisplayText());
            

      
    }
    public void SetFont(TMP_FontAsset font)
    {
        text.font = font;
    }
}
