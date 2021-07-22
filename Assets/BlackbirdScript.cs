using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackbirdScript : MonoBehaviour
{
    private GameObject target;
    private float time;
    private Vector3 posToMove;
    private Vector3 startingPosition;
    private bool enterOnce;
    private bool blockMovement;
    private Animator animator;
    public AudioSource source;
    public AudioClip deathClip;
    private Transform player;
    private bool run;

    
    void Start()
    {
        run = false;
        enterOnce = true;
        blockMovement = false;
        target = gameObject;
        posToMove = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startingPosition = posToMove;
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();

    }

   
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (run)
        {
            if (Vector2.Distance(transform.position, player.position) < 6)
            {
                Movement();
            }
        }
       
    }

    private void OnBecameVisible()
    {
        run = true;
    }
    private void OnBecameInvisible()
    {
        run = false;
    }

    private void Movement()
    {
        if (!blockMovement)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMove, 4.5f * Time.deltaTime);
        }
        if (time>0.5f)
        {

            CheckRotation();
           
            time = 0f;
            target = GameObject.Find("Player");
            posToMove = target.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3,3),0);
            
        }

        
    }

    private void CheckRotation()
    {
        if (transform.position.x < target.transform.position.x)
        {
            //isLooking = "right";
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            //isLooking = "left";
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& enterOnce)
        {
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(1f);
            enterOnce = false;
            StartCoroutine(ExitHelp());
        }
    }


    private IEnumerator ExitHelp()
    {
        yield return new WaitForSeconds(0.5f);
        enterOnce = true;
    }

    public void Die()
    {
        SoundSystem.PlaySound(deathClip, false,0.5f, source);
        blockMovement = true;
        animator.SetBool("death", true);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
        Destroy(gameObject, 1f);
    }
}
