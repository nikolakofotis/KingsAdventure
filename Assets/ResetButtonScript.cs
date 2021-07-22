using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonScript : MonoBehaviour
{

    public bool isUp;
    public Animator animator;
    public BoxCollider2D colliderHelp;
    public LockCombinationScript lockC;
    private bool playerIsOn,block,blockExit;
    
    void Start()
    {
        isUp = true;
        playerIsOn = false;
        block = false;
        blockExit = true;
        
    }


    void FixedUpdate()
    {
        animator.SetBool("Up", isUp);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !block)
        {
            isUp = false;
            block = true;
            blockExit = true;
            print("in enter");
            playerIsOn = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.0126915f, 0.005645931f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.5584183f, 0.08279419f);
            colliderHelp.offset = new Vector2(0.01817799f, -0.0121184f);
            colliderHelp.size = new Vector2(1.036356f, 0.05474377f);
            lockC.ResetLocks();
            StartCoroutine(UnblockExit());
            StartCoroutine(ResetUp());
        }

       
    }

    private IEnumerator UnblockExit()
    {
        yield return new WaitForSeconds(0.1f);
        blockExit = false;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& !blockExit)
        {
            print("unblocked exit");
            playerIsOn = false;
        }
    }
    public void ResetLock()
    {
        colliderHelp.offset = new Vector2(0.01817799f, 0.4423315f);
        colliderHelp.size = new Vector2(1.036356f, 0.9636436f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.0126915f, 0.5085709f);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.5584183f, 1.088644f);
        isUp = true;
    }

   private IEnumerator ResetUp()
    {

        yield return new WaitUntil(() => playerIsOn == false);
        yield return new WaitForSeconds(0.8f);
        block = false;
        ResetLock();
    }

}
