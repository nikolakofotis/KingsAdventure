using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowScript : MonoBehaviour
{

    private Pooling pool;
    private float frc;
    private PlayerMove player;
    public AudioClip[] rockSounds;
  
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        pool = GameObject.Find("Player").GetComponent<Pooling>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int random = Random.Range(0, 8);
        GameObject particle;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.PlayRemoteSound(rockSounds[random], 1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f * player.force, 2.5f),ForceMode2D.Impulse);
            print("okforce");
            pool.EnQ("Rocks", gameObject, 0f);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            collision.gameObject.GetComponent<PigScript>().takeDamage(0, 5f);

        }
        else if (collision.gameObject.CompareTag("Crate"))
        {
            player.PlayRemoteSound(rockSounds[random], 1);
            collision.gameObject.GetComponent<CrateScript>().hitCrate(frc);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            pool.EnQ("Rocks", gameObject, 0f);

        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            player.PlayRemoteSound(rockSounds[random], 1);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            collision.gameObject.GetComponent<BombScript>().InstantExplosion();
            pool.EnQ("Rocks", gameObject, 0f);

        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            player.PlayRemoteSound(rockSounds[random], 1);
            pool.EnQ("RockP", particle, 1f);
            pool.EnQ("Rocks", gameObject, 0f);

        }
        else if (collision.gameObject.CompareTag("CannonHead"))
        {
            collision.gameObject.GetComponent<EnemyScript>().knockForce = 0f;
            collision.gameObject.GetComponent<EnemyScript>().LoseLife(5);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f * player.force, 2f));
            pool.EnQ("Rocks", gameObject, 0f);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);


        }
        else if (collision.gameObject.CompareTag("ShadowPig"))
        {
            player.PlayRemoteSound(rockSounds[random], 1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f * player.force, 2f));
            pool.EnQ("Rocks", gameObject, 0f);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            collision.gameObject.GetComponent<ShadowPig>().takeDamage(0, 5f);
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            collision.gameObject.GetComponent<RockScript>().Die();
        }
        else if(collision.gameObject.CompareTag("Chameleon"))
        {
            if (!collision.gameObject.GetComponent<CameleonBossScript>().GetDizzy())
            {
                collision.gameObject.GetComponent<CameleonBossScript>().MakeChameleonDizzy(3f);
            }
            player.PlayRemoteSound(rockSounds[random], 1);
            pool.EnQ("Rocks", gameObject, 0f);
            particle = pool.Spawn("RockP", gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            
            
        }
        
    }



    
}
