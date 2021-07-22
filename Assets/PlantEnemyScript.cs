using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemyScript : MonoBehaviour
{
    private GameObject player;
    private float time;
    public GameObject shootPos;
    public Animator animator;
    private float lives;
    public string direction;
    private bool unlock;
    public AudioClip clip;
    public AudioSource source;
    private Pooling g;
    void Start()
    {
        player = GameObject.Find("Player");
        g = player.GetComponent<Pooling>();
        time = 0.1f;
        lives = 2;
        unlock = false;
        if(direction=="right")
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

   
    

   

    private void Attack()
    {
        time += Time.deltaTime;
        if (time>0.65f)
        {
            time = 0;
            source.clip = clip;
            source.Play();
           g.Spawn("PlantBullet", shootPos.transform.position, Quaternion.identity);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")|| collision.gameObject.CompareTag("BlueBullet"))
        {
            
            Hit();
        }
    }

   private void Hit()
    {
        if(lives>0)
        {
            print("hitttt");
            animator.SetBool("hit", true);
            StartCoroutine(HitDisable());
            lives--;
        }
        else
        {
           GameObject g= GameObject.Find("Player").GetComponent<Pooling>().Spawn("PlantDeathParticle", transform.position, Quaternion.identity);
            GameObject.Find("Player").GetComponent<Pooling>().EnQ("PlantDeathParticle",g, 1.5f);
            Destroy(gameObject);

        }
    }

    private IEnumerator HitDisable()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            unlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", false) ;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
            Attack();
        }
    }
}
