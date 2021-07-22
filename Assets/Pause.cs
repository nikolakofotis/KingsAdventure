using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseOpen;
    public Button restart, load, exitToMenu ,pauseButton;
    private PlayerMove player;
    private Canvas controls;
    public GameObject dashButton;
    private bool check;
    void Start()
    {
        controls = GameObject.Find("Controls").GetComponent<Canvas>();
        check = false;
        
    }

    
    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        
        
    }


    public void SelectPause()
    {
        check = false;
        controls.gameObject.SetActive(false);
        dashButton.SetActive(false);
        pauseOpen.SetActive(true);
        
        StartCoroutine(StopTime());
        
        
    }

    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.3f);
        if (!check)
        {
            
            Time.timeScale = 0f;
        }
        else
        {
            check = true ;
        }
    }

    public void ResumeGame()
    {
        player.gameObject.GetComponent<AdScript>().HideBanner();
        check = true;
        controls.gameObject.SetActive(true);
        dashButton.SetActive(true);
        pauseOpen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        check = true;
        controls.gameObject.SetActive(true);
        dashButton.SetActive(true);
        pauseOpen.SetActive(false);
        player.LoadLastSave();
    }


    public void ExitToMenu()
    {
        Time.timeScale=1f;
        controls.gameObject.SetActive(true);
        pauseOpen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }


}
