using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private RhinoScript rhino;
    private string isLooking;
    private float speed,bulletCounter;
    private bool run,shoot,hit,blockMovement,isPushable,alreadyPushed,soundLock,startRunning;
    public Animator animator;
    public Pooling pool;
    public Transform shootingPos;
    private float force;
    public Vector3 startingPosition;
    public AudioSource audiosource;
    public AudioClip dissapearClip,landClip;

    void Start()
    {
        speed = 2f * Time.deltaTime;
        startingPosition = transform.position;
        blockMovement = false;
        isPushable = true;
        alreadyPushed = false;
        soundLock = false;
        startRunning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("hit", hit);
        bulletCounter += Time.deltaTime;
        animator.SetBool("run", run);
        animator.SetBool("shoot", shoot);
        rhino = GameObject.Find("RhinoBoss").GetComponent<RhinoScript>();
        pool = GameObject.Find("Player").GetComponent<Pooling>();
        if(startRunning)
        {
            Run();
        }
    }



    private void Run()
    {

        if (Vector3.Distance(transform.position, rhino.transform.position) > 8f)
        {

            run = true;
            shoot = false;
           
            LookTowards(rhino.gameObject);
            transform.position = Vector3.MoveTowards(transform.position, rhino.transform.position, speed);

        }
        else
        {
            run = false;
            shoot = true;
            Shoot();
        }

    }

    private void Shoot()
    {
        if (bulletCounter > 1.5f)
        {
            bulletCounter = 0;
            shoot = true;
            StartCoroutine(HelpWithShootingAnimation());
        }
        else
        {
            shoot = false;
        }
    }

    private IEnumerator HelpWithShootingAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        
        GameObject tempBullet = pool.Spawn("TreeBullet", shootingPos.position, Quaternion.identity);
        tempBullet.GetComponent<BulletScript>().TreeBulletSpawn(force);
    }
    private void LookTowards(GameObject g)
    {
        if (transform.position.x < g.transform.position.x)
        {
            force = 1f;
            isLooking = "right";
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            force = -1f;
            isLooking = "left";
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPushable && !alreadyPushed)
        {
            alreadyPushed = true;
            
        }

       else if (collision.gameObject.CompareTag("Ground")&& !blockMovement )
        {
            isPushable = false;
            
            startRunning = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground") && !blockMovement && !soundLock)
        {
            isPushable = false;
            soundLock = true;
            startRunning = true;
            Run();
            SoundSystem.PlaySound(landClip, false, audiosource);
           
        }
    }




    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            alreadyPushed = false;
            PlayerMove tempPlayer = collision.gameObject.GetComponent<PlayerMove>();
            tempPlayer.SetPush(false);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            startRunning = false;
        }

    }

    public void TakeDamage()
    {
        hit = true;
        blockMovement = true;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        soundLock = false;
        SoundSystem.PlaySound(dissapearClip, false,0.3f, audiosource);
        isPushable = true;
        GameObject g=  pool.Spawn("TreeParticles", transform.position, Quaternion.identity);
        blockMovement = false;
        transform.position = startingPosition;
        hit = false;
        print("tookdmg");
        pool.EnQ("TreeParticles", g, 1f);
    }



   
}
