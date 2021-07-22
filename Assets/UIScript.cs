using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIScript : MonoBehaviour
{
    public PlayerMove player;
    private float diamonds, hearts;
    public Image[] heartsUI;
    public Button attackButton;
    public Sprite melee, ranged;
   
    public Canvas controller;
    public bool isShoping;
    public TextMeshProUGUI text, text1;
    
    
    public Button backFront;
    
    public Button[] items;
   
   
    public AudioClip invSound;
   
   
   

   
   

 
    public void SetDiamondText(float diamonds)
    {
        text.text = "X" + "  " + diamonds.ToString();
        text1.text = "X" + " " + diamonds.ToString();

    }

    public void CheckLives(float hearts)
    {
        switch(hearts)
        {
            case 6:
                heartsUI[5].color = Color.white;
                heartsUI[4].color = Color.white;
                heartsUI[3].color = Color.white;
                heartsUI[2].color = Color.white;
                heartsUI[1].color = Color.white;
                heartsUI[0].color = Color.white;
                break;
            case 5:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.white;
                heartsUI[3].color = Color.white;
                heartsUI[2].color = Color.white;
                heartsUI[1].color = Color.white;
                heartsUI[0].color = Color.white;

                break;
            case 4:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.clear;
                heartsUI[3].color = Color.white;
                heartsUI[2].color = Color.white;
                heartsUI[1].color = Color.white;
                heartsUI[0].color = Color.white; 
                 break;
            case 3:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.clear;
                heartsUI[3].color = Color.clear;
                heartsUI[2].color = Color.white;
                heartsUI[1].color = Color.white;
                heartsUI[0].color = Color.white; 
                break;
            case 2:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.clear;
                heartsUI[3].color = Color.clear;
                heartsUI[2].color = Color.clear;
                heartsUI[1].color = Color.white;
                heartsUI[0].color = Color.white;
                break;
            case 1:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.clear;
                heartsUI[3].color = Color.clear;
                heartsUI[2].color = Color.clear;
                heartsUI[1].color = Color.clear;
                heartsUI[0].color = Color.white;
                break;
            case 0:
                heartsUI[5].color = Color.clear;
                heartsUI[4].color = Color.clear;
                heartsUI[3].color = Color.clear;
                heartsUI[2].color = Color.clear;
                heartsUI[1].color = Color.clear;
                heartsUI[0].color = Color.clear;
                break;
        }
    }


   


   


    


    
   

   
   
    
  

   
  
   

   
}
