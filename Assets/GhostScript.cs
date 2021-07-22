using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GhostScript : MonoBehaviour
{
   
    
   
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector3 movement;
    private float timeLeft;
    private PlayerMove player;
    private float x, y;
    private float timeNow,attackTime;
    private float beforeX, afterX;
    public AudioSource source;
    public AudioClip clip;
    private int attackModes;
    private float yUp, yDown,balader,speed;
    private int health;
    public Animator animator;
    private float shootingTime;
    public GameObject spawn;
    public Light2D light;
    public PolygonCollider2D coll;
    private bool once;
    void Start()
    {
        once = true;
        animator.SetBool("appear", false);
        light.pointLightOuterRadius = 0f;
        health = 250;
        speed = 0.5f;
        source.volume = 0.1f;
        attackModes = 1;
        yUp = transform.position.y +2f;
        yDown = transform.position.y - 2f;
        balader = yUp;
        shootingTime = 1.3f;
        spawn.gameObject.SetActive(false);

    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        attackTime += Time.deltaTime;
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        x = player.transform.position.x/ Mathf.Abs(player.transform.position.x);
        y= player.transform.position.y / Mathf.Abs(player.transform.position.y);
        if (timeLeft <= 0)
        {
            
            timeLeft += accelerationTime;
        }
    }

    void FixedUpdate()
    {
        timeNow = Time.fixedDeltaTime + timeNow;
        //AppearDissapear();
        CheckS();
        Movement();
        
    }

    private void AppearDissapear()
    {
        if (Vector2.Distance(GameObject.Find("Player").GetComponent<Transform>().position, transform.position)<12f)
        {
           
        }
        else
        {
            animator.SetBool("appear", false);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(2555, 255, 255, 0);
        }
    }

    private void CheckS()
    {
        
        if(timeNow>0.1f)
        {
            timeNow = 0f;
            afterX = transform.position.x;
            if(beforeX<afterX)
            {
                
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

        }
        else
        {
            beforeX = transform.position.x;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (once)
            {
                coll.isTrigger = false;
                once = false;
                light.pointLightOuterRadius = 4.06f;
                animator.SetBool("appear", true);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(2555, 255, 255, 215);
            }
            Attack();
        }
    }

    private void Attack()
    {
        if (attackTime > shootingTime)
        {
            switch (attackModes)
            {
                case 0:
                    
            SoundSystem.PlaySound(clip, false, source);
            attackTime = 0;
           
            GameObject g3 = player.gameObject.GetComponent<Pooling>().Spawn("GhostBullets", transform.position, Quaternion.identity);
            GameObject g4 = player.gameObject.GetComponent<Pooling>().Spawn("GhostBullets", transform.position, Quaternion.identity);
            
            GameObject g7 = player.gameObject.GetComponent<Pooling>().Spawn("GhostBullets", transform.position, Quaternion.identity);
            
           
          
            g3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3f, 3f), ForceMode2D.Impulse);
            g4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3f, -3f), ForceMode2D.Impulse);
           
          
            g7.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3f, 0), ForceMode2D.Impulse);
            
                   
                    break;

                case 1:
                    attackTime = 0f;
                    GameObject g9= player.gameObject.GetComponent<Pooling>().Spawn("GhostBullets", transform.position, Quaternion.identity);
                    g9.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                    print("here"); 
                    break;
                    
        }

        }
    }


    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, balader, transform.position.z),speed * Time.fixedDeltaTime);

        if (balader == yUp)
        {
            if ((int)transform.position.y == (int)yUp)
            {
                balader=yDown;
            }

        }
        else if(balader==yDown)
        {
            if ((int)transform.position.y == (int)yDown)
            {
                balader = yUp;
            }
        }
       
        
    }


    public void LoseLife(int lifeToLose)
    {
        health -=  lifeToLose;

        if(health>0)
        {
            animator.SetBool("hit", true);
            StartCoroutine(MakeFalse("hit"));
            speed += 0.25f;
            shootingTime -= 0.07f;
            if(health<=50)
            {
                attackModes = 0;
            }

        }
        else if(health<=0)
        {
            animator.SetBool("die", true);
            StartCoroutine(MakeFalse("die"));
            spawn.gameObject.SetActive(true);
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        once = true;
        coll.isTrigger = true;
        animator.SetBool("appear", false);
        light.pointLightOuterRadius = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(2555, 255, 255, 0);
    }

    private IEnumerator MakeFalse(string s)
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(s, false);

    }



}
