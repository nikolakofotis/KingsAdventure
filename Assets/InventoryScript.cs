using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryScript : MonoBehaviour
{
    public TextMeshProUGUI[] bottleTexts;
    public GameObject inventoryCanvas;
    public Button craftButton;
    public Image[] craftedBottles;
    public AudioClip clip, openInventoryClip;
    public AudioSource source;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OpenInventory()
    {
        SoundSystem.PlaySound(openInventoryClip, false, source);
        inventoryCanvas.SetActive(true);   
        PlayerMove player = GameObject.Find("Player").GetComponent<PlayerMove>();
        
            
           
        }



    private void ResetInventory()
    {
        inventoryCanvas.SetActive(true);
        PlayerMove player = GameObject.Find("Player").GetComponent<PlayerMove>();
        for (int i = 0; i < player.hasBottles.Length; i++)
        {

            bottleTexts[i].text = "X " + player.hasBottles[i].ToString();


        }
        CheckCraft(player);

    }


    private void CheckCraft(PlayerMove player)
    {
        int count=0;
       
        
            for (int i = 0; i < player.hasBottles.Length; i++)
            {
                if (player.hasBottles[i] > 0)
                {
                    count++;
                }
            }
            if(player.hasMadeBottles>0)
            {
                for (int i = 0; i < player.hasMadeBottles; i++)
                {
                    craftedBottles[i].color = new Color(255, 255, 255);
                   
                }
            }

            if (count >= 4)
            {
                craftButton.interactable = true;
            }
            else
            {
                craftButton.interactable = false;
            }
        
        
    }

    public void CancelInventory()
    {
        inventoryCanvas.SetActive(false);
    }

    public void Craft()
    {
        SoundSystem.PlaySound(clip, false, source);
        PlayerMove player = GameObject.Find("Player").GetComponent<PlayerMove>();
        craftedBottles[player.hasMadeBottles].color = new Color(255, 255, 255);
        for (int i = 0; i < player.hasBottles.Length; i++)
        {
            player.hasBottles[i]--;
        }
        ResetInventory();
        player.hasMadeBottles++;
        player.RemoteSave();
    }

    }

    






