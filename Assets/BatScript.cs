using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    private string direction;
    private GameObject target;
    public Animator animator;
    private float timer,shootTimer;
    public BoxCollider2D playerCollider,shootCollider;
    private bool wakeUp;
    private bool startMoving;
    public bool trigger;



    void Start()
    {
        trigger = false;
        wakeUp = false;
        startMoving = false;
    }


    void FixedUpdate()
    {
        direction = "right";
        animator.SetBool("Wake Up", wakeUp);
        animator.SetBool("Start Moving", startMoving);
        timer = timer + Time.deltaTime;
        shootTimer = shootTimer + Time.deltaTime;
        TriggerSet();
        if (startMoving)
        {
            Movement();
        }
    }

    private void TriggerSet()
    {
        if (!trigger)
        {
            shootCollider.enabled = false;
            //shootCollider.size = new Vector2(1.029694f, 0.279557f);
           // shootCollider.offset = new Vector2(-0.7126579f, 0.05718696f);
        }
        else if(trigger)
        {
            shootCollider.enabled = true;
            shootCollider.size = new Vector2(15.39005f, 0.279557f);
            shootCollider.offset = new Vector2(-7.892836f, 0.05718696f);
        }
    }

    public void Movement()
    {
        target = GameObject.Find("Player");
        if (target.gameObject.GetComponent<PlayerMove>().GetDirection() == "right")
        {
            direction = "right";
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
        else if (target.gameObject.GetComponent<PlayerMove>().GetDirection() == "left")
        {
            direction = "left";
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        }


            if (Vector3.Distance(transform.position, target.transform.position) > 1.5f)
            {
                float speed = (Vector3.Distance(transform.position, target.transform.position)+1.5f) * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 1, 0), speed);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        GameObject g = collision.gameObject;
            if (collision.gameObject.CompareTag("Player"))
            {
                wakeUp = true;
                StartCoroutine(StartMoving());
                playerCollider.enabled = false;
            }
            else if (collision.gameObject.CompareTag("Bunny"))
            {
            if (g.GetComponent<BossesScript>().isLooking != direction)
            {
                print(direction);
                Shoot();
            }
            }
        }


    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (collision.gameObject.CompareTag("Bunny"))
        {
            if (g.GetComponent<BossesScript>().isLooking != direction)
            {
                print(direction);
                Shoot();
            }
        }
    }

    private IEnumerator StartMoving()
        {
            yield return new WaitForSeconds(0.5f);
            startMoving = true;
            

        }

        private void Shoot()
        {
            float delay = 0.5f;
            if(shootTimer>delay)
            {
                shootTimer = 0;
                target.gameObject.GetComponent<Pooling>().Spawn("BatBullet", transform.position, Quaternion.identity);
                

            }


    }

    }



