using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScipt : MonoBehaviour
{
    private bool openSpikes;
    public Animator animator;
    private bool locking;
    void Start()
    {
        openSpikes = false;
        locking = false;
    }

    
    void FixedUpdate()
    {
        animator.SetBool("openSpikes", openSpikes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            openSpikes = true;
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            openSpikes = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!locking)
            {
                collision.gameObject.GetComponent<PlayerMove>().LoseLife(6f);
                locking = true;
                StartCoroutine(UnlockWait());

            }
        }
    }

    private IEnumerator UnlockWait()
    {
        yield return new WaitForSeconds(1f);
        locking = false;

        
    }
}
