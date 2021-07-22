using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Credits : MonoBehaviour
{
    public Canvas creditsCanvas;
    public GameObject gameName;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    public void OpenCredits(string s)
    {
        switch(s)
        {
            case "open":
                creditsCanvas.gameObject.SetActive(true);
                gameName.SetActive(false);
                break;
            case "close":
                gameName.SetActive(true);
                creditsCanvas.gameObject.SetActive(false);
                
                break;
        }
        
    }


}
