using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject controls;
    public GameObject openCanvas;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenLink()
    {
        Application.OpenURL("www.google.com");
       
    }

    public void OpenCanvas()
    {
        openCanvas.SetActive(true);
    }

}
