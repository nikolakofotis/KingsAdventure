using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button loadButton;
    public AudioSource source;
    public AudioClip song;
    public GameObject languagePanel, lscreen;
   
    private string language;
   
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SoundSystem.PlaySound(song, true, source);
        LoadCheck();

    }

    
    void FixedUpdate()
    {
       
        
            
        
    }

    public void NewLevelLoad()
    {
        
        SaveSystem.ClearMemory();
        SaveSystem.ClearMemoryForPlayer();
        PlayerMove player = new PlayerMove();
        player.newGame = true;
        player.diamonds = 0;
        player.weapon = 0;
        player.usableHearts = 0;
        player.hasKeyForLevels = 0;
        player.hasMadeBottles = 0;
        player.language = language;
        player.ResetBottles();
        SaveSystem.Save(player);
        SceneManager.LoadScene("0");
        
    }

    public void OpenLang()
    {
        languagePanel.SetActive(true);
    }

    public void CloseLang()
    {
        languagePanel.SetActive(false);
    }
    public void LanguageButton(string lang)
    {
        
        language = lang;
        CloseLang();
        lscreen.SetActive(true);
        NewLevelLoad();

    }

    public void LoadGame()
    {
        lscreen.SetActive(true);
       
        PlayerMove player = new PlayerMove();
        
        
        SceneManager.LoadScene(player.level.ToString());
       
    }

    private void LoadCheck()
    {
        if(SaveSystem.Load()==null )
        {
            loadButton.interactable = false;
        }
    }
}
