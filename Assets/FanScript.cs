using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    private bool lockFly;
    // Start is called before the first frame update
    void Start()
    {
        lockFly = true;
    }

    // Update is called once per frame
   

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player") && lockFly)
        {
            lockFly = false;
            StartCoroutine(Fly(collision.gameObject.GetComponent<Rigidbody2D>()));
        }
    }

    private IEnumerator Fly(Rigidbody2D rb)
    {
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);
        lockFly = true;
    }




}
