using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBulletScript : MonoBehaviour
{
    private Pooling pool;
    public AudioSource sourceG;
    public AudioClip clip;
    private bool move;
    private float timer;
    void Start()
    {
        move = false;
    }

   
    void FixedUpdate()
    {
        pool = GameObject.Find("Player").GetComponent<Pooling>();

        timer += Time.fixedDeltaTime;
        if(timer>3f)
        {
            timer = 0f;
           
            pool.EnQ("GhostBullets", gameObject, 1f);
           
        }
        
          
            
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMove>().lives > 0)
            {
                SoundSystem.PlaySound(clip, false, sourceG);
                print("hit player");
                GameObject temp = pool.Spawn("GhostParticles", collision.gameObject.transform.position, Quaternion.identity);
                collision.gameObject.GetComponent<PlayerMove>().LoseLife(1f);
                pool.EnQ("GhostBullets", gameObject, 1f);
                pool.EnQ("GhostParticles", temp, 0.5f);
            }
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            SoundSystem.PlaySound(clip, false, sourceG);
            GameObject temp = pool.Spawn("GhostParticles", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("GhostParticles", temp, 0.5f);
            pool.EnQ("GhostBullets", gameObject, 0);
            
        }
       
       
    }

   
}
