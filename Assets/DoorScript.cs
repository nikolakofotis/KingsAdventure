using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int isDoorForLevel;
    public bool open;
    public Animator animator;
    public GameObject doorButton;
     public bool enter;
    public AudioClip doorOpen, doorClose;
    public AudioSource doorSource;
    public GameObject keyText;
    public bool specialDoor;

   
    void Start()
    {
        doorButton.SetActive(false);
    }

   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            open = true;
            animator.SetBool("Open", open);
            SoundSystem.PlaySound(doorOpen, false, doorSource);
            if(specialDoor)
            {
                GetComponentInParent<LockCombinationScript>().CheckDoor("open");
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            doorButton.SetActive(false) ;
            SoundSystem.PlaySound(doorClose, false, doorSource);
            open = false;
            animator.SetBool("Open", open);
            keyText.SetActive(false);
            if(specialDoor)
            {
                GetComponentInParent<LockCombinationScript>().CheckDoor("close");
            }
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(enter && collision.gameObject.GetComponent<PlayerMove>().GetKeyLevel()>=isDoorForLevel )
            {
                doorButton.SetActive(true);
            }
            else if(collision.gameObject.GetComponent<PlayerMove>().GetKeyLevel() < isDoorForLevel)
            {
                keyText.SetActive(true);
                //doorButton.SetActive(false);
            }
            //open = true;
        }
    }






}
