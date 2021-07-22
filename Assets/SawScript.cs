using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    private bool lockDeath;
   

    
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !lockDeath)
        {
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(6f);
            lockDeath = true;
            StartCoroutine(UnlockDeath());
        }
    }

    private IEnumerator UnlockDeath()
    {
        yield return new WaitForSeconds(1f);
        lockDeath = false;
    }
}
