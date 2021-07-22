using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPieceScript : MonoBehaviour
{

    public BoxCollider2D myCollider;
    
    
   
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LadderTag"))
        {
            StartCoroutine(DeactivateTrigger());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LadderTag"))
        {
            myCollider.isTrigger = true;
        }
    }


    private IEnumerator DeactivateTrigger()
    {
        yield return new WaitForSeconds(0.1f);
        myCollider.isTrigger = false;
    }
}
