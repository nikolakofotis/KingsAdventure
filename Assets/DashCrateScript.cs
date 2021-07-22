using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCrateScript : MonoBehaviour
{
    private int crateBreak;
    public Animator animator;
    private bool once;
    public GameObject particles;
    void Start()
    {
        once = true;
    }

    
    

    public void TakeDamage()
    {
        if (once)
        {
            once=false;
            crateBreak++;
            animator.SetInteger("break", crateBreak);
            StartCoroutine(DisableOnce());
            if(crateBreak==3)
            {
                particles.SetActive(true);
                Destroy(gameObject, 0.8f);
            }
        }
    }

    private IEnumerator DisableOnce()
    {
        yield return new WaitForSeconds(1f);
        once = true;
    }
}
