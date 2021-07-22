using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public Rigidbody2D bomb;
    public bool isActive;
    public Animator animator;
    private bool explosion;
    private bool isGrounded;
    public CircleCollider2D bombRadius;
    private Pooling pool;
    public AudioSource bombSource;
    public AudioClip[] bombBoomSound;
    public AudioClip dropSound,fuseSound;



    private void Start()
    {
       // isActive = false;
        explosion = false;
        pool = GameObject.Find("Player").GetComponent<Pooling>();
    }
    private void FixedUpdate()
    {
        
        animator.SetBool("IsOn", isActive);
        animator.SetBool("Exploded", explosion);
        if (isActive)
        {
            StartCoroutine(ExplodeWhenActive());
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMove>().isDead == false)
            {
                explosion = true;
                int rand = Random.Range(0, 2);
                SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
                float forceDirection = -collision.gameObject.GetComponent<PlayerMove>().force;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDirection * 5, 5f), ForceMode2D.Impulse);
                collision.gameObject.GetComponent<PlayerMove>().LoseLife(3f);
                //Destroy(gameObject,0.5f);
                pool.EnQ("Bomb", gameObject, 0.5f);
            }
          
        }

        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Quaternion enemyTransform = collision.gameObject.GetComponent<Transform>().rotation;
            int rand = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);


            if (enemyTransform.y == -1)
            {
                collision.gameObject.GetComponent<PigScript>().blockMovement = true;
                collision.gameObject.GetComponent<PigScript>().takeDamage(-0.5f, 100f);



            }
            else if (collision.gameObject.transform.rotation.y == 0)
            {
                collision.gameObject.GetComponent<PigScript>().blockMovement = true;
                collision.gameObject.GetComponent<PigScript>().takeDamage(0.5f, 100f);



            }
           
            }
        else if (collision.gameObject.CompareTag("ShadowPig"))
        {
            Quaternion shadowTransform = collision.gameObject.GetComponent<Transform>().rotation;
            int random = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[random], false, bombSource);
            Debug.Log("pig");

            if (shadowTransform.y == -1)
            {
                collision.gameObject.GetComponent<ShadowPig>().blockMovement = true;
                collision.gameObject.GetComponent<ShadowPig>().takeDamage(-0.5f, 100f);



            }
            else if (collision.gameObject.transform.rotation.y == 0)
            {
                collision.gameObject.GetComponent<ShadowPig>().blockMovement = true;
                collision.gameObject.GetComponent<ShadowPig>().takeDamage(0.5f, 100f);



            }

            explosion = true;
           
            //Destroy(gameObject, 0.5f);
            pool.EnQ("Bomb", gameObject, 0.7f);



        }
        else if(collision.gameObject.CompareTag("Rhino"))
        {
            
            int rand = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
            explosion = true;

            collision.gameObject.GetComponent<RhinoScript>().LoseLife(Random.Range(10, 20));
            pool.EnQ("Bomb", gameObject, 0.7f);
        }
        
    }
    
    public void AddForceToBomb(float force,string isFacing)
    {
        if (isFacing =="left")
        {
            force = force * -1f;
            bomb.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);
        }
        else if(isFacing=="right")
        {
            bomb.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);
        }
    }

    private IEnumerator ExplodeWhenActive()
    {
        
        
            isActive = false;
            SoundSystem.PlaySound(fuseSound, false, bombSource);
            yield return new WaitForSeconds(4f);
            SoundSystem.StopPlayingSound(fuseSound, bombSource);
            int rand = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
            explosion = true;
            bombRadius.radius = 1f;
            pool.EnQ("Bomb", gameObject, 0.5f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMove>().isDead == false)
            {
                explosion = true;
                int rand = Random.Range(0, 2);
                SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
                float forceDirection = -collision.gameObject.GetComponent<PlayerMove>().force;
               
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDirection * 5, 5f), ForceMode2D.Impulse);
                collision.gameObject.GetComponent<PlayerMove>().LoseLife(3f);
               
                pool.EnQ("Bomb", gameObject, 0.5f);


            }

        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Quaternion enemyTransform = collision.gameObject.GetComponent<Transform>().rotation;
            int rand = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
            if (enemyTransform.y == -1)
            {
                collision.gameObject.GetComponent<PigScript>().blockMovement = true;
                collision.gameObject.GetComponent<PigScript>().takeDamage(-0.5f, 100f);



            }
            else if (collision.gameObject.transform.rotation.y == 0)
            {
                collision.gameObject.GetComponent<PigScript>().blockMovement = true;
                collision.gameObject.GetComponent<PigScript>().takeDamage(0.5f, 100f);



            }


        }
        else if (collision.gameObject.CompareTag("ShadowPig"))
        {
            Quaternion shadowTransform = collision.gameObject.GetComponent<Transform>().rotation;
            int random = Random.Range(0, 2);
            SoundSystem.PlaySound(bombBoomSound[random], false, bombSource);
            Debug.Log("pig");

            if (shadowTransform.y == -1)
            {
                collision.gameObject.GetComponent<ShadowPig>().blockMovement = true;
                collision.gameObject.GetComponent<ShadowPig>().takeDamage(-0.5f, 100f);



            }
            else if (collision.gameObject.transform.rotation.y == 0)
            {
                collision.gameObject.GetComponent<ShadowPig>().blockMovement = true;
                collision.gameObject.GetComponent<ShadowPig>().takeDamage(0.5f, 100f);



            }
        }
       
    }

    
    public void InstantExplosion()
    {
        int rand = Random.Range(0, 2);
        SoundSystem.PlaySound(bombBoomSound[rand], false, bombSource);
        explosion = true;
        bombRadius.radius = 1f;
        pool.EnQ("Bomb", gameObject, 0.5f);
    }

    public IEnumerator GroundWait()
    {
        if(!isGrounded)
        {
            yield return new WaitUntil(() => isGrounded);
            SoundSystem.PlaySound(dropSound, false, bombSource);
        }
    }

}




