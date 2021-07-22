using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCombinationScript : MonoBehaviour
{
    public string[] combination;
    public LockComponentScript[] hasLocks;
    private string hasEntered;
    public DoorScript isLinkedToDoor;
    public AudioClip pressureClip;
    public AudioSource pressureSource;
    public Canvas lockedCanvas;
    public Camera gameCamera;

    
    void Start()
    {
        isLinkedToDoor.enter = false;
        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        lockedCanvas.worldCamera = gameCamera;
        isLinkedToDoor.specialDoor = true;
        lockedCanvas.gameObject.SetActive(false);
       
    }

    
   public void CheckDoor(string openClose)
    {
        
        switch(openClose)
        {
            case "open":
                if(isLinkedToDoor.enter)
                {
                    lockedCanvas.gameObject.SetActive(false);
                }
                else
                {
                    lockedCanvas.gameObject.SetActive(true);
                }
                break;
            case "close":
                lockedCanvas.gameObject.SetActive(false);
                break;
        }
    }

   

    public void CheckCombination()
    {
        int counter = 0;
        SoundSystem.PlaySound(pressureClip, false, pressureSource);
       for(int i=0;i<hasLocks.Length;i++)
        {
           
            if(combination[i]==hasLocks[i].isUp.ToString())
            {
                counter++;
            }
        }

       if(counter==hasLocks.Length)
        {
            
            isLinkedToDoor.enter = true;
            isLinkedToDoor.open = true;
            isLinkedToDoor.animator.SetBool("Open", true);
        }
        

    }

    public void ResetLocks()
    {
        SoundSystem.PlaySound(pressureClip, false, pressureSource);
       for(int i=0;i<hasLocks.Length;i++)
        {
            hasLocks[i].ResetLock();
        }
    }

    
}
