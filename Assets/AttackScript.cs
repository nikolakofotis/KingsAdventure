using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private PlayerMove player;
    private CameraScript cameraz;
    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        cameraz = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pooling pool = player.gameObject.GetComponent<Pooling>();
        
        print("entered collision");
        int randX = Random.Range(0, 0);
        if (collision.gameObject.tag == "Crate")
        {
            StartCoroutine(cameraz.Shake(0.25f));
            GameObject p = pool.Spawn("PlayerAttack", transform.position + new Vector3(1 * player.force, 0, 0), Quaternion.identity);
            pool.EnQ("PlayerAttack", p, 1f);
            SoundSystem.PlaySound(player.crateSounds[randX], false, player.hitSource);
           CrateScript crate = collision.gameObject.GetComponent<CrateScript>();
            crate.hitCrate(player.force);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(cameraz.Shake(0.25f));
            Debug.Log("hitEnemy");
            GameObject p = pool.Spawn("PlayerAttack", transform.position + new Vector3(1*player.force, 0, 0), Quaternion.identity);
            pool.EnQ("PlayerAttack", p, 1f);
            SoundSystem.PlaySound(player.meleeSounds[randX], false, player.hitSource);
            PigScript enemy = collision.gameObject.GetComponent<PigScript>();
            enemy.takeDamage(player.force,player.weaponDamage);
        }
        else if (collision.gameObject.CompareTag("CannonHead"))
        {
           
            StartCoroutine(cameraz.Shake(0.25f));
            GameObject p = pool.Spawn("PlayerAttack", transform.position + new Vector3(1 * player.force, 0, 0), Quaternion.identity);
            pool.EnQ("PlayerAttack", p, 1f);
            SoundSystem.PlaySound(player.meleeSounds[randX], false, player.hitSource);
            collision.gameObject.GetComponent<EnemyScript>().LoseLife((int)player.weaponDamage);
        }
        else if (collision.gameObject.CompareTag("ShadowPig"))
        {
            StartCoroutine(cameraz.Shake(0.25f));
            GameObject p = pool.Spawn("PlayerAttack", transform.position + new Vector3(1 * player.force, 0, 0), Quaternion.identity);
            pool.EnQ("PlayerAttack", p, 1f);
            SoundSystem.PlaySound(player.meleeSounds[randX], false, player.hitSource);
            ShadowPig enemy1 = collision.gameObject.GetComponent<ShadowPig>();
            enemy1.takeDamage(player.force, player.weaponDamage);
        }
        else if(collision.gameObject.CompareTag("Chameleon"))
        {
            StartCoroutine(cameraz.Shake(0.25f));
            GameObject p = pool.Spawn("PlayerAttack", transform.position + new Vector3(1 * player.force, 0, 0), Quaternion.identity);
            pool.EnQ("PlayerAttack", p, 1f);
            SoundSystem.PlaySound(player.meleeSounds[randX], false, player.hitSource);
            collision.gameObject.GetComponent<CameleonBossScript>().TakeDamage(10f);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.force+15f, 10f), ForceMode2D.Impulse);
        }
    }
}
