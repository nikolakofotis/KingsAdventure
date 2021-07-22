using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoScript : MonoBehaviour
{
    private float life,speed;
    private bool run,isCharged,hitEnemy,lockHit,move,isHit,lockedEnemy,death;
    private string isLooking;
    private PlayerMove target;
    public Animator animator;
    public GameObject hasLocked;
    public GameObject enable, disable;
    public AudioClip clip;
    



    void Start()
    {
        life = 100f;
        speed = 3.7f*Time.deltaTime;
        isCharged = true;
        run = false;
        lockHit = false;
        move = false;
        isHit = false;
        death = false;
        
        

    }

   
    void FixedUpdate()
    {
        animator.SetBool("death", death);
        animator.SetBool("isHit", isHit);
        target = GameObject.Find("Player").GetComponent<PlayerMove>();
        animator.SetBool("run", run);
        animator.SetBool("hit", hitEnemy);
        if (move && hasLocked!=null)
        {
            Run(hasLocked);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!lockedEnemy)
        {
            if (collision.gameObject.CompareTag("TreeTrunks") && isCharged)
            {
                run = true;
                hasLocked = collision.gameObject;
                lockedEnemy = true;
                move = true;


                
            }
            else if (collision.gameObject.CompareTag("Player") && isCharged)
            {

                run = true;
                hasLocked = collision.gameObject;
                lockedEnemy = true;
                move = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
         if (collision.gameObject.CompareTag("TreeTrunks") && isCharged)
            {
            run = false; 
            hasLocked = null;
            lockedEnemy = false;
            move = false;


                
            }
            else if (collision.gameObject.CompareTag("Player") && isCharged)
            {

            run = false;
            hasLocked = null;
            lockedEnemy = false;
            move = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if(collision.gameObject.CompareTag("TreeTrunks") && !lockHit)
        {
            Rigidbody2D obj = collision.gameObject.GetComponent<Rigidbody2D>();
            collision.gameObject.GetComponent<TreeScript>().TakeDamage();
            AddForce(obj);
            move = false;
            hitEnemy = true;
            lockHit = true;
            
        }

         else if (collision.gameObject.CompareTag("Player") && !lockHit)
        {
            Rigidbody2D obj = target.gameObject.GetComponent<Rigidbody2D>();
            AddForce(obj);
            move = false;
            hitEnemy = true;
            lockHit = true;
            target.LoseLife(1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("TreeTrunks"))
        {
            StartCoroutine(ExitHelp());
            
        }
    }


    private IEnumerator ExitHelp()
    {
        yield return new WaitForSeconds(0.5f);
        hitEnemy = false;
        move = true;
        lockHit = false;
    }

    private void Run(GameObject followIt)
    {
        if (Vector3.Distance(transform.position, followIt.transform.position) > 0.01f)
        {

            
            if (transform.position.x < followIt.transform.position.x)
            {
                isLooking = "right";
                gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                isLooking = "left";
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            transform.position = Vector3.MoveTowards(transform.position, followIt.transform.position, speed);

        }
       

    }

    private void AddForce(Rigidbody2D obj)
    {
        if(target.GetDirection()==isLooking)
        {
            obj.AddForce(new Vector2(target.force * 15, 3f), ForceMode2D.Impulse);
        }
        else
        {
            obj.AddForce(new Vector2(-target.force * 15, 3f), ForceMode2D.Impulse);
        }
    }

    public void LoseLife(float lostLife)
    {
        print(life);
        if (life > 0)
        {
            isCharged = false;
            isHit = true;
            life -= lostLife;
            StartCoroutine(Recharge());
        }
    else
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().ChangeMusic(clip);
            death = true;
            enable.SetActive(true);
            disable.SetActive(false);
            Destroy(gameObject, 1.5f);
        }
    }


    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(0.4f);
        isHit = false;
        isCharged = true;
    }


    
}
