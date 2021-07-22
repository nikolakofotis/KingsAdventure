using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CheckPointScript : MonoBehaviour
{
    public Animator animator;
    private bool open;
    public Light2D light;
    private float radius;
    private bool locklight,alreadyOpened;
    public AudioSource source;
    public AudioClip clipaki;
    private bool blink;
   


    void Start()
    {
        alreadyOpened = false;
        radius = 0f;
        locklight = true;
        blink = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (blink)
        {
            LightBlink();
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyOpened)
        {
            SoundSystem.PlaySound(clipaki, false, source);
            
            open = true;
            animator.SetBool("Open", open);
            alreadyOpened = true;
            blink = true;
           
            
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            if (!player.CheckMusic())
            {
                player.StartStopMusic("start");
                player.myCollider.offset = new Vector2(-0.25f, 0);
                player.uiElements.CheckLives(player.lives);
                player.animator.SetBool("IsDead", false);

            }
            
            
            if(player.isDead || player.blockMovement)
            {
                player.AdRessurect();
            }
           


        }
    }


    private void LightBlink()
    {
        light.pointLightOuterRadius = radius;

        if (radius < 4 &&locklight)
        {

            radius += 0.05f;
        }
        else if(radius>=4)
        {
            
            locklight = false;
        }
        
       




    }
}
