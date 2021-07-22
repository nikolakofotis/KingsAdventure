using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheckScript : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(6);
        }
        else if (tag == "Enemy")
        {
            collision.gameObject.GetComponent<PigScript>().takeDamage(0, 100f);
        }
        else if (tag == "ShadowPig")
        {
            collision.gameObject.GetComponent<ShadowPig>().takeDamage(0, 100f);
        }
        else if (tag == "CannonHead")
        {
            collision.gameObject.GetComponent<EnemyScript>().LoseLife(100);
        }
        else if(tag=="Ghost")
        {
            collision.gameObject.GetComponent<GhostScript>().LoseLife(250);
        }

    }

}
