using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadScript : MonoBehaviour
{
    public Animator animator;
    private float time;
    private bool enter;
    
    void Start()
    {
        enter = true;
    }

   
    void FixedUpdate()
    {
        
        time += Time.deltaTime;
        if(time>5f)
        {
            time = 0;
            Blink();
        }
    }

    private void Blink()
    {
        animator.SetBool("blink", true);
        StartCoroutine(SetFalse());
        
    }

    private IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("blink", false);
        enter = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && enter)
        {
            enter = false;
            StartCoroutine(SetFalse());
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(6f);
        }
    }
}
