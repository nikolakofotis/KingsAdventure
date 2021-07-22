using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameleonBossScript : MonoBehaviour
{
    public Animator animator;
    private bool blockMovementAnimation,run;
    private string isLooking;
    public Transform particlesEmmisionSpot;
    private Pooling pooling;
    private float walkingTimer,attackTimer;
    private bool firstTimeWalking,firstTimeAttack;
    private bool attack;
    private bool isDizzy;
    public float life;
    public GameObject deathParticles;
    
    void Start()
    {
        
        life = 100f;
        isDizzy = false;
        firstTimeWalking = true;
        firstTimeAttack = true;
    }

    
    void FixedUpdate()
    {

        attackTimer = Time.deltaTime + attackTimer;
        walkingTimer = Time.deltaTime + walkingTimer;
        pooling = GameObject.Find("Player").GetComponent<Pooling>();
        animator.SetBool("isDizzy", isDizzy);
        animator.SetBool("run", run);
        animator.SetBool("attack", attack);
        if (!isDizzy)
        {
            Movement(GameObject.Find("Player"));
        }
    }

    private void Movement(GameObject targetz)
    {
        float speed = 3f * Time.deltaTime;

        
        if (Vector3.Distance(transform.position, targetz.transform.position) < 10f && Vector3.Distance(transform.position, targetz.transform.position) > 4f )
        {
            firstTimeAttack = true;
            attack = false;
            if (!blockMovementAnimation)
            {
                run = true;


            }
            if (transform.position.x < targetz.transform.position.x)
            {
                isLooking = "right";
                gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                isLooking = "left";
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            WalkingParticles();
            transform.position = Vector3.MoveTowards(transform.position, targetz.transform.position, speed);

        }
        else if(Vector3.Distance(transform.position, targetz.transform.position) <= 4f)
        {
            Attack();
            
        }
        else
        {
            firstTimeAttack = true;
            run = false;
            firstTimeWalking = true;


        }




    }

    private void Attack()
    {
        run = false;
        if (firstTimeAttack)
        {
            
            firstTimeAttack = false;
            attackTimer = 0;
        }
        if (attackTimer > 0.85f)
        {
            
            attackTimer = 0;
            
            firstTimeWalking = true;
            attack = true;
            firstTimeAttack = false;
            StartCoroutine(DealDamageDelay());
        }
       
    }

    private IEnumerator DealDamageDelay()
    {
        yield return new WaitForSeconds(0.25f);
        PlayerMove player = GameObject.Find("Player").GetComponent<PlayerMove>();
        Rigidbody2D playerRB = player.gameObject.GetComponent<Rigidbody2D>();
        
        if (!player.isDead)
        {
            playerRB.AddForce(new Vector2(-player.force *7f, 5f), ForceMode2D.Impulse);
            player.LoseLife(1f);
        }
       
    }

    private void WalkingParticles()
    {
        
        if(firstTimeWalking)
        {
            walkingTimer = 0.15f;
            firstTimeWalking = false;
        }
        if (walkingTimer > 0.33f)
        {
            walkingTimer = 0;
            GameObject temp=  pooling.Spawn("ChameleonWalking", particlesEmmisionSpot.position, Quaternion.identity);
            pooling.EnQ("ChameleonWalking", temp, 0.5f);
        }
    }

    public void MakeChameleonDizzy(float seconds)
    {
        StartCoroutine(MakeChamDizz(seconds));
    }

    private IEnumerator MakeChamDizz(float seconds)
    {
        isDizzy = true;

        yield return new WaitForSeconds(seconds);
        isDizzy = false;
    }

    public bool GetDizzy()
    {
        return isDizzy;
    }

   public void TakeDamage(float damage)
    {
        if(life>0)
        {
            life -= damage;
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        SpriteRenderer mySprite = gameObject.GetComponent<SpriteRenderer>();
        float alpha = 1;
        while (alpha >= 0.0999999999f)
        {
            
            mySprite.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.1f);

            alpha -= 0.1f;
        }
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject,0.3f);
       

    }
   
}
