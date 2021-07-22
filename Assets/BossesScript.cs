using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesScript : MonoBehaviour
{
    public string isLooking;
    public string bossType;
    public Animator animator;
    public GameObject target;
    public bool run,isGrounded,isFalling;
    public bool blockMovementAnimation,isJumping;
    private float jumpTime;
    private float temp1Jump, temp2Jump, attackTimeCounter;
    public BoxCollider2D triggerCollider;
    private float life;
    private BatScript bat;
    public GameObject exitDoor;
    private GameObject player;
    private bool blockMove;
    public AudioSource source;
    public AudioClip footstepsClip,hit;
    private float time;

    void Start()
    {
        isLooking = "left";
        life = 200f;
        isFalling = false;
        blockMovementAnimation = false;
    }

    
    void FixedUpdate()
    {
        time += Time.deltaTime;
        player = GameObject.Find("Player");
        jumpTime = Time.deltaTime + jumpTime;
        attackTimeCounter = Time.deltaTime + attackTimeCounter;
        bat = GameObject.Find("King's Bat").GetComponent<BatScript>();
        animator.SetBool("run", run);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("jump", isJumping);
        target = GameObject.Find("Player");
        //Movement(target);
        CheckDir();
        CheckRay();
        JumpCheck();
    }


    
   private void CheckDir()
    {
        if (transform.position.x < player.transform.position.x)
        {
            isLooking = "right";
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            isLooking = "left";
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
    private void CheckRay()
    {
        RaycastHit2D ray;
        if (isLooking == "right")
        {
             ray = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 1.75f, transform.position.y));
            Debug.DrawLine(transform.position, new Vector2(transform.position.x + 1.75f, transform.position.y));
        }
        else
        {
             ray = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 1.75f, transform.position.y));
        }

        if(ray.collider.CompareTag("Ground"))
        {
            run = false;
            isJumping = false;
            blockMove = true;
        }
        else
        {
            
            blockMove = false;
        }
    }
    private void Movement(GameObject targetz)
    {
        float speed = 3.7f * Time.deltaTime;
        if (!blockMove)
        {
            AttackModes();



            if (Vector3.Distance(transform.position, targetz.transform.position) > 0.01f)
            {
                
                if (!blockMovementAnimation)
                {
                    run = true;
                    if(time>0.5)
                    {
                        print("ok");
                        time = 0;
                        source.clip = footstepsClip;
                        source.volume = 0.8f;
                        source.Play();
                    }


                }
                float Direction = Mathf.Sign(targetz.transform.position.x - transform.position.x);
                Vector2 MovePos = new Vector2(
                    transform.position.x + Direction * speed, //MoveTowards on 1 axis
                    transform.position.y
                );
                transform.position = MovePos;
                // transform.position = Vector3.MoveTowards(transform.position, targetz.transform.position, speed);

            }
            else
            {
                run = false;


            }
        }
        
       



    }

    

    private void AttackModes()
    {


        float delay = 1f;
        if (attackTimeCounter > delay)
        {
            attackTimeCounter = 0;
            if (isGrounded && Vector3.Distance(transform.position, target.transform.position) < 6f)
            {
                triggerCollider.offset = new Vector2(0.1704988f, -3.712025f);
                triggerCollider.size = new Vector2(27.57693f, 11.52405f);
                blockMovementAnimation = true;
                isJumping = true;
                run = false;

                
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,375f), ForceMode2D.Impulse);
                StartCoroutine(TriggerJump());
            }
        }
              
        
    }



    private IEnumerator TriggerJump()
    {
        // print("before trigger");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(()=>isGrounded);
        
        blockMovementAnimation = false;
        isJumping = false;
        triggerCollider.offset = new Vector2(0.1704988f, -0.3073664f);
        triggerCollider.size = new Vector2(27.57693f, 4.714733f);
        StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraScript>().Shake(0.25f));
        
    }


    private void JumpCheck()
    {
        float delay = 0.025f;

        if (!isGrounded)
        {

            if (jumpTime > delay)
            {
                jumpTime = 0f;
                temp2Jump = transform.position.y;
                if (temp1Jump > temp2Jump)
                {
                    isFalling = true;

                }

            }

            temp1Jump = temp2Jump;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bat.trigger = true;
            Movement(target);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bat.trigger = true;
            Movement(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bat.trigger = false;
            run = false;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SideWalls"))
        {
            isGrounded = true;
            isFalling = true;
            blockMovementAnimation = false;
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            PlayerMove tempPlayer = collision.gameObject.GetComponent<PlayerMove>();
            tempPlayer.LoseLife(6f);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SideWalls"))
        {
            isGrounded = false;
            run = false;
            blockMovementAnimation = true;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SideWalls"))
        {
            isGrounded = true;
            isFalling = false;
            
        }
    }
    public void CheckLife(float loseLife)
    {
        if(life>loseLife)
        {
            
            life = life - loseLife;
            print(life);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Pooling>().Spawn("BunnyBossParticles", transform.position, Quaternion.identity);
            exitDoor.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        
       
          

             
    }

    

    public void ResetLives()
    {
        life = 100f;
    }

    
   
}
