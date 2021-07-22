using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Pooling pool;
    private float frc;
    private PlayerMove player;
    public AudioSource bulletSource;
    public AudioClip[] explodeSound;
    public string type;
    
    

    void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

   
    
    
   public  void OnSpawn(float force)
    {
        force = force / Mathf.Abs(force);
        frc = force;
        pool = GameObject.Find("Player").GetComponent<Pooling>();


        Rigidbody2D bullet = gameObject.GetComponent<Rigidbody2D>();
        
        bullet.AddForce(new Vector2(force*10f,0f), ForceMode2D.Impulse);
        if (type != "purple")
        {
            StartCoroutine(DeleteAfterSecs(3f));
        }

    }

    public void TreeBulletSpawn(float force)
    {
        
        


        Rigidbody2D bullet = gameObject.GetComponent<Rigidbody2D>();

        bullet.AddForce(new Vector2(force , 0f), ForceMode2D.Impulse);
        StartCoroutine(DeleteAfterSecs(3f));
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        switch (type)
        {
            case "normal":
                DealDamage(enemy, "Bullet", "BulletParticle", explodeSound[0], player.weaponDamage);
                break;
            case "blue":
                DealDamage(enemy, "BlueBullet", "BlueBulletParticle", explodeSound[0], 50f);
                break;

        }
        
    }


   private  IEnumerator DeleteAfterSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
        if (gameObject.CompareTag("TreeBullet"))
        {
            pool.EnQ("TreeBullet", gameObject, 0f);
        }
        else
        {
            pool.EnQ("Bullet", gameObject, 0f);
        }

    }


    private void DealDamage( Collision2D enemy,string objToSpawn, string particles,AudioClip audioclip,float damage)
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        pool = GameObject.Find("Player").GetComponent<Pooling>();
        int random = Random.Range(0, 2);
        GameObject particle;
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            enemy.gameObject.GetComponent<PigScript>().takeDamage(0, damage);

        }
        else if (enemy.gameObject.CompareTag("Crate"))
        {
            player.PlayRemoteSound(audioclip, 1);
            enemy.gameObject.GetComponent<CrateScript>().hitCrate(frc);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            pool.EnQ(objToSpawn, gameObject, 0f);

        }
        else if (enemy.gameObject.CompareTag("Bomb"))
        {
            player.PlayRemoteSound(audioclip, 1);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            enemy.gameObject.GetComponent<BombScript>().InstantExplosion();
            pool.EnQ(objToSpawn, gameObject, 0f);

        }
        else if (enemy.gameObject.CompareTag("Ground") || enemy.gameObject.CompareTag("SideWalls"))
        {
            player.PlayRemoteSound(audioclip, 1);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            SoundSystem.PlaySound(audioclip, false, bulletSource);
            pool.EnQ(particles, particle, 1f);
            pool.EnQ(objToSpawn, gameObject, 0f);

        }
        else if (enemy.gameObject.CompareTag("CannonHead"))
        {
            enemy.gameObject.GetComponent<EnemyScript>().knockForce = 0f;
            enemy.gameObject.GetComponent<EnemyScript>().LoseLife((int)damage);
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);


        }
        else if (enemy.gameObject.CompareTag("ShadowPig"))
        {
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
            enemy.gameObject.GetComponent<ShadowPig>().takeDamage(0, damage);
        }
        else if (enemy.gameObject.CompareTag("Rock"))
        {
            enemy.gameObject.GetComponent<RockScript>().Die();



        }
        else if (enemy.gameObject.CompareTag("Chameleon"))
        {

            int randX = Random.Range(0, 0);
            SoundSystem.PlaySound(player.meleeSounds[randX], false, player.hitSource);
            enemy.gameObject.GetComponent<CameleonBossScript>().TakeDamage(10f);
            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.force + 15f, 10f), ForceMode2D.Impulse);
        }
        else if (enemy.gameObject.CompareTag("Rhino"))
        {
            if (gameObject.CompareTag("TreeBullet"))
            {
                enemy.gameObject.GetComponent<RhinoScript>().LoseLife(5f);
                pool.EnQ("TreeBullet", gameObject, 0);

            }
            else
            {
                pool.EnQ(particles, gameObject, 0);
            }
        }
        else if(enemy.gameObject.CompareTag("Bunny"))
        {
            enemy.gameObject.GetComponent<BossesScript>().CheckLife(10f);
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
        }
        else if(enemy.gameObject.CompareTag("Ghost"))
        {
            enemy.gameObject.GetComponent<GhostScript>().LoseLife(10);
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
        }
        else if(enemy.gameObject.CompareTag("DashCrate"))
        {
            player.PlayRemoteSound(audioclip, 1);
            pool.EnQ(objToSpawn, gameObject, 0f);
            particle = pool.Spawn(particles, gameObject.transform.position, Quaternion.identity);
            pool.EnQ("RockP", particle, 1f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Blackbird"))
        {
            switch (type)
            {
               
                case "normal":
                    player.PlayRemoteSound(explodeSound[0], 1);
                    GameObject p = pool.Spawn("BulletParticle", gameObject.transform.position, Quaternion.identity);
                    //SoundSystem.PlaySound(audioclip, false, bulletSource);
                    pool.EnQ("BulletParticle", p, 1f);
                    pool.EnQ("Bullet", gameObject, 0f);
                    break;

                case "blue":
                    player.PlayRemoteSound(explodeSound[0], 1);
                    GameObject pa = pool.Spawn("BlueBulletParticle", gameObject.transform.position, Quaternion.identity);
                    //SoundSystem.PlaySound(audioclip, false, bulletSource);
                    pool.EnQ("BlueBulletParticle", pa, 1f);
                    pool.EnQ("BlueBullet", gameObject, 0f);
                    break;


            }

            enemy.gameObject.GetComponent<BlackbirdScript>().Die();

        }
    }


    private void PurpleBulletBehavior()
    {
      

    }

    private IEnumerator PurpleCountdown()
    {
        yield return new WaitForSeconds(3f);
        //enq purple
        
    }
}

   


