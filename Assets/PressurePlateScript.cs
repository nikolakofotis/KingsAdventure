using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public BoxCollider2D plateCollider,helpingCollider;
    public Animator animator;
    private bool blockEnter, blockExit;
    public int activatePlate;
    public GameObject isLinkedToFire;
    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {

        activatePlate =0;
        blockEnter = false;
        blockExit = true;
       
    }

   
    void FixedUpdate()
    {
        animator.SetInteger("StateNum",activatePlate);
       
    }


   


    public void ChangeOffset(float newOffset)
    {
        plateCollider.offset=new Vector2(0,newOffset);
    }

   

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
          if(!blockEnter)
            {
                blockEnter = true;
               
                StartCoroutine(StartFalling());
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("RockBullet"))
        {
            if (!blockEnter)
            {
                print("hit by rock ");
                StartCoroutine(StartFalling());
            }

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!blockExit )
            {
                blockExit = true;
                print("exited");



                StartCoroutine(ReverseIt());
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
        
    }

    private IEnumerator StartFalling()
    {
        SoundSystem.PlaySound(clip, false, audioSource);
        helpingCollider.offset = new Vector2(0, -0.02730209f);
        helpingCollider.size = new Vector2(1, 0.03078556f);
        // yield return new WaitForSeconds(0.1f);
        activatePlate = 1;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 2;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 3;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 4;
        isLinkedToFire.GetComponent<FireScript>().StopFire();
        isLinkedToFire.GetComponent<FireScript>().canTakeDamage = false;
        yield return new WaitForSeconds(0.2f);
        blockExit =false;

       

       
    }


    private IEnumerator ReverseIt()
    {
        yield return new WaitForSeconds(2f);
        activatePlate = 4;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 3;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 2;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 1;
        yield return new WaitForSeconds(0.1f);
        activatePlate = 0;
        blockEnter = false;
        blockExit = true;
        helpingCollider.offset = new Vector2(0, 0.4777813f);
        helpingCollider.size = new Vector2(1, 1.040952f);
        SoundSystem.PlaySound(clip, false, audioSource);
        isLinkedToFire.GetComponent<FireScript>().canTakeDamage = true;
        isLinkedToFire.GetComponent<FireScript>().StartFire();
       
        print("ok");

    }

  
}
