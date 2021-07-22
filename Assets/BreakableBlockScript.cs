using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockScript : MonoBehaviour
{
    public Animator animator;
    private int collCounter;
    
    void Start()
    {
      animator=  gameObject.GetComponent<Animator>();
        collCounter = 0;
    }

    
    void FixedUpdate()
    {
        
    }

    private void BreakBlock()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("BlueBullet"))
        {
            collCounter++;
            print(collCounter);
            if(collCounter==1)
            {
                animator.SetBool("crack1", true);
            }
            else if(collCounter==2)
            {
                animator.SetBool("crack2", true);

            }
            else if(collCounter>2)
            {

                animator.SetBool("broken", true);
                Destroy(gameObject, 0.8f);
            }
        }
    }
}
