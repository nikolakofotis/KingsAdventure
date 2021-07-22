using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBulletScript : MonoBehaviour
{
    private float speed;
    private Pooling player;
    
    void Start()
    {
        speed = 15f;
        OnSpawn();
    }

   
    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<Pooling>();
        //OnSpawn();
    }

    private void OnSpawn()
    {
        PlayerMove p = GameObject.Find("Player").GetComponent<PlayerMove>();
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed *Time.deltaTime* p.force,0), ForceMode2D.Impulse);
        //transform.position = new Vector3(transform.position.x + speed *(p.force) *Time.deltaTime, transform.position.y, transform.position.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject g = collision.gameObject;
        if(collision.gameObject.CompareTag("Bunny"))
        {
            g.GetComponent<BossesScript>().CheckLife(5f);
           GameObject p= player.Spawn("BatBulletParticles", transform.position, Quaternion.identity);
            player.EnQ("BatBulletParticles", p, 0.5f);
            player.EnQ("BatBullet", gameObject, 0);
           
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            GameObject p = player.Spawn("BatBulletParticles", transform.position, Quaternion.identity);
            player.EnQ("BatBulletParticles", p, 0.5f);
            player.EnQ("BatBullet", gameObject, 0);
        }
    }



}
