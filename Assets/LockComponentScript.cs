using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockComponentScript : MonoBehaviour
{
    public bool isUp;
    public Animator animator;
    public BoxCollider2D colliderHelp;
    public LockCombinationScript lockC;
    
    void Start()
    {
        isUp = true;
    }

    
    void FixedUpdate()
    {
        animator.SetBool("Up", isUp);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isUp = false;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.0126915f, 0.005645931f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.5584183f, 0.08279419f);
            colliderHelp.offset = new Vector2(0.01817799f, -0.0121184f);
            colliderHelp.size = new Vector2(1.036356f, 0.05474377f);
            lockC.CheckCombination();
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


}
