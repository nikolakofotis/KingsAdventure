using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Pooling pool;
    public float knockForce;
    public  bool rangedAttack, normalAttack;
    public Transform shootPos;
    private float time,time2;
     public TargetJoint2D target;
    private float xDirection;
    private float speed;
    private bool shoot, blockMovement,isDead,isGrounded,attack,isMoving;
    public Animator animator;
    private int life;
    public AudioSource takeDmgSource, hitSource, footstepsSource,explosionSource;
    public AudioClip[] takeDmgSounds;
    public AudioClip hitSound, footsteps,fireSound;
    private float timeNew;


    void Start()
    {
        
        shoot = false;
        speed = 2f;
        life = 100;//min ksexaso na to pao 100
        blockMovement = false;//min ksexaso na to kano false

    }

   
    void FixedUpdate()
    {

        pool = GameObject.Find("Player").GetComponent<Pooling>();
        animator.SetBool("Attack", attack);
        animator.SetBool("Shoot", shoot);
        animator.SetBool("Move", isMoving);
        animator.SetBool("Dead", isDead);
       
        time = Time.deltaTime + time;
        time2 = Time.deltaTime + time2;
        timeNew = Time.deltaTime + timeNew;
        
        Move();
        
    }

    private void Attack(Vector3 playerPos, float direction ,float forceΧ,float forceY)
    {


        int offset = Random.Range(0, 2);
        RaycastHit2D topRay = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + offset), new Vector2(transform.position.x + 20, transform.position.y + offset), 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawLine(new Vector2(transform.position.x*xDirection, transform.position.y + offset), new Vector2(transform.position.x * xDirection + 20, transform.position.y + offset));
        if (topRay.collider != null)
        {

            if (topRay.collider.gameObject.CompareTag("Player"))
            {

                if (time > 1f)
                {


                    time = 0;
                    explosionSource.volume = 0.5f;
                    SoundSystem.PlaySound(fireSound, false, explosionSource);
                    GameObject bullet = pool.Spawn("CannonHeadBullets", shootPos.position, Quaternion.identity);
                    GameObject boom = pool.Spawn("CannonHeadBulletsParticle", shootPos.position, Quaternion.identity);
                    shoot = true;
                    target = bullet.GetComponent<TargetJoint2D>();
                    target.target = playerPos;
                    target.maxForce = 2f;
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2((1.8f + (forceΧ / 1.6f)) * direction, (1f + forceY)), ForceMode2D.Impulse);


                    pool.EnQ("CannonHeadBullets", bullet, 2f);
                    pool.EnQ("CannonHeadBulletsParticle", boom, 2f);



                }
                else shoot = false;
            }
        }
    }


    private void Move()
    {
        if (!blockMovement)
        {


            GameObject player = GameObject.Find("Player");
            Vector3 playerPos = player.GetComponent<Transform>().position;
            if (playerPos.x > transform.position.x)
            {
                xDirection = 1;
                transform.eulerAngles = new Vector2(0, 180);
            }
            else if (transform.position.x >= playerPos.x+0.5f)
            {
                xDirection = -1;
                transform.eulerAngles = new Vector2(0, 0f);

            }

            float x = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(playerPos.x));
            if (x <= 15f)
            {
                
                if (x >= 3f)
                {
                   
                    isMoving = false;
                    float yPos = Mathf.Abs(playerPos.y)-Mathf.Abs(transform.position.y);
                    Attack(playerPos, xDirection, x, yPos + x / 2);
                    knockForce = 2.5f;

                }
                else
                {
                    knockForce = 4f;
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, transform.position + new Vector3(1f * xDirection, -0.1f, 0), 1 << LayerMask.NameToLayer("Player"));
                    RaycastHit2D ray2 = Physics2D.Linecast(transform.position, transform.position + new Vector3(1f * xDirection, 1f, 0), 1 << LayerMask.NameToLayer("Player"));

                    Debug.DrawLine(transform.position, transform.position + new Vector3(1f * xDirection, 1f, 0));
                    
                    if (ray==false && ray2==false)
                    {
                      
                       
                        
                            isMoving = true;
                        PlayFootsteps();
                            transform.position = new Vector3(transform.position.x + speed * xDirection * Time.deltaTime, transform.position.y, transform.position.z);
                           
                        
                    }
                    else if (ray)
                    {
                        if (ray.collider.gameObject.CompareTag("Player"))
                        {
                            isMoving = false;
                        
                            if (time2 > 1.5f)
                            {

                                time2 = 0;
                                SoundSystem.PlaySound(hitSound, false, hitSource);
                                attack = true;
                                StartCoroutine(AttackDisable());
                                player.GetComponent<PlayerMove>().LoseLife(1);
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection * 1.5f, 0), ForceMode2D.Impulse);

                            }
                        }


                    }
                }

            }
        }
        
        




    }
    IEnumerator AttackDisable()
    {
        yield return new WaitForSeconds(0.5f);
        attack = false;
    }

    public void LoseLife(int lifeToLose)
    {
        life = life - lifeToLose;
        int x = Random.Range(0, 3);
        SoundSystem.PlaySound(takeDmgSounds[x], false, takeDmgSource);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockForce*(-xDirection), 2f), ForceMode2D.Impulse);
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        blockMovement = true;
        gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(0, 0.2f);
       
        Destroy(gameObject, 1f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void PlayFootsteps()
    {
        if (timeNew > 0.3f && isGrounded)
        {
            timeNew = 0;
            footstepsSource.volume = 0.4f;
            SoundSystem.PlaySound(footsteps, false, footstepsSource);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            Attack(collision.transform.position, xDirection, 0, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            Attack(collision.transform.position, xDirection, 0, 0.5f);
        }
    }



}
