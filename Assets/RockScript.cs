using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    private bool move,lockIt;
    private float speed;
    private float rotation;
    public GameObject rockParticles;
    public AudioClip breakingClip;
    private PlayerMove player;

    void Start()
    {
        lockIt = true;
        speed = 2.5f;
    }

   
    void FixedUpdate()
    {
        
        Moving();
    }


   private void Moving()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        if(speed>0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
       
        if (lockIt)
        {
            lockIt = false;
           
            StartCoroutine(Countdown());

        }
    }

    private IEnumerator Countdown()
    {
        float randomHuehue = Random.Range(1f, 4f);
       
        transform.eulerAngles = new Vector3(0f, rotation, 0f);
        yield return new WaitForSeconds(randomHuehue);
        lockIt = true;
        speed = speed * -1;
        
       
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(1f);
            collision.gameObject.GetComponent<PlayerMove>().PlayRemoteSound(breakingClip, 2);
            GameObject p = Instantiate(rockParticles, transform.position, Quaternion.identity);
            Destroy(p, 0.6f);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Steps"))
        {
            speed = speed * -1;
        }
    }



    public void Die()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        player.PlayRemoteSound(breakingClip, 2);
       player.StartCoroutine(player.camera.Shake(0.25f));
        GameObject p = Instantiate(rockParticles, transform.position, Quaternion.identity);
        Destroy(p, 0.6f);
        Destroy(gameObject);
    }

}
