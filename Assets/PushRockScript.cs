using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRockScript : MonoBehaviour
{
    private bool blink;
    public Animator animator;
    private bool enter;
    void Start()
    {
        blink = false;
        enter = true;
        //StartCoroutine(Blink());
    }

    

    void FixedUpdate()
    {
        animator.SetBool("blink", blink);
        if(enter)
        {
            StartCoroutine(Blink());
        }
       
    }

    private IEnumerator Blink()
    {
        enter = false;
        yield return new WaitForSeconds(4f);
        blink = true;
        yield return new WaitForSeconds(0.1f);
        blink = false;
        enter = true;
    }


   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject temp = collision.gameObject;
        if(temp.CompareTag("Player"))
        {
            temp.GetComponent<PlayerMove>().SetPush(true);
        }
    }

    private void OnTriggerExit2D(Collision2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.CompareTag("Player"))
        {
            temp.GetComponent<PlayerMove>().SetPush(false); 
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.CompareTag("Player"))
        {
            temp.GetComponent<PlayerMove>().SetPush(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.CompareTag("Player"))
        {
            temp.GetComponent<PlayerMove>().SetPush(false);
        }
    }
}

