using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHeadBallScript : MonoBehaviour
{
    private Pooling pool;
    public AudioClip explosionSound;
    
    private PlayerMove player;
    void Start()
    {
        
    }

  
    void FixedUpdate()
    {
        pool = GameObject.Find("Player").GetComponent<Pooling>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")  || collision.gameObject.CompareTag("Steps"))
        {
            player.PlayRemoteSound(explosionSound, 2);
            pool.EnQ("CannonHeadBullets", gameObject, 0f);
            GameObject bulletParticle = pool.Spawn("BulletParticle", transform.position, Quaternion.identity);
            pool.EnQ("BulletParticle", bulletParticle, 0.25f);
        }
        else if( collision.gameObject.CompareTag("Player"))
        {
            player.PlayRemoteSound(explosionSound, 2);
            PlayerMove p = collision.gameObject.GetComponent<PlayerMove>();
            p.LoseLife(1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(p.force * 1.5f, 3f), ForceMode2D.Impulse);
            pool.EnQ("CannonHeadBullets", gameObject, 0f);
            GameObject bulletParticle = pool.Spawn("BulletParticle",transform.position, Quaternion.identity);
            pool.EnQ("BulletParticle", bulletParticle, 1f);
           
        }
    }
}
