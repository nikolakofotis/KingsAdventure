using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBulletS : MonoBehaviour
{
   private Transform player;
    private Vector3 tempTransform;
    public AudioSource source;
    public AudioClip explosionClip;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        tempTransform = player.position+ new Vector3(0, Random.Range(-1.5f, 1.5f), 0);
       

    }

   
    void FixedUpdate()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, tempTransform , 5.5f * Time.deltaTime);
        if(transform.position==tempTransform)
        {
            SoundSystem.PlaySound(explosionClip, false, source);
            GameObject temp = GameObject.Find("Player").GetComponent<Pooling>().Spawn("PlantBulletBoom",transform.position,Quaternion.identity);
            GameObject.Find("Player").GetComponent<Pooling>().EnQ("PlantBulletBoom", temp, 1f);
            GameObject.Find("Player").GetComponent<Pooling>().EnQ("PlantBullet", gameObject, 0f);
            

        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundSystem.PlaySound(explosionClip, false, source);
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(1f);
            GameObject temp = GameObject.Find("Player").GetComponent<Pooling>().Spawn("PlantBulletBoom", transform.position, Quaternion.identity);
            GameObject.Find("Player").GetComponent<Pooling>().EnQ("PlantBulletBoom", temp, 1f);
            GameObject.Find("Player").GetComponent<Pooling>().EnQ("PlantBullet", gameObject, 0f);
        }
       
    }


}
