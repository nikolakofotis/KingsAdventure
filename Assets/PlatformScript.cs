using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    private float startingX, startingY;
    public float speed;
    public string moveAxis;
    public int startingDirectionY;
    private bool enterOnce;
    public int maxDistance;
  public bool run;

    void Start()
    {
        run = false;
        startingX = transform.position.x;
        startingY = transform.position.y;
        enterOnce = true;
        if(maxDistance==0)
        {
            maxDistance = 3;
        }
        //speed = 0.9f;

    }
    private void OnBecameVisible()
    {
        run = true;
    }

    private void OnBecameInvisible()
    {
        run = false;
    }

    void FixedUpdate()
    {
        if (run)
        {
            Move();
        }
    }

    private void Move()
    {
        if (startingDirectionY == 1)
        {

            if (moveAxis == "y")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                if ((int)transform.position.y == (int)startingY + maxDistance || (int)transform.position.y == (int)startingY - maxDistance)
                {
                    speed = -speed;

                }
            }
            else if (moveAxis == "x")
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                if ((int)transform.position.x == (int)startingX + maxDistance || (int)transform.position.x == (int)startingX - maxDistance)
                {
                    speed = -speed;
                }
            }
        }
        else if (startingDirectionY == -1)
        {
            if (moveAxis == "y")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                if ((int)transform.position.y == (int)startingY + maxDistance || (int)transform.position.y == (int)startingY - maxDistance)
                {
                    speed = -speed;

                }
            }
            else if (moveAxis == "x")
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                if ((int)transform.position.x == (int)startingX + maxDistance || (int)transform.position.x == (int)startingX - maxDistance)
                {
                    speed = -speed;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bomb"))
        {
            if (moveAxis == "x")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x + speed * Time.deltaTime, player.position.y, player.position.z);
            }
            else if (moveAxis == "y")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x, player.position.y + speed * Time.deltaTime, player.position.z);

            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bomb"))
        {
            if (moveAxis == "x")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x + speed * Time.deltaTime, player.position.y, player.position.z);
            }
            else if (moveAxis == "y")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x, player.position.y + speed * Time.deltaTime, player.position.z);

            }


        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bomb"))
        {
            if (moveAxis == "x")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x + speed * Time.deltaTime, player.position.y, player.position.z);
            }
            else if (moveAxis == "y")
            {
                Transform player = collision.gameObject.GetComponent<Transform>();
                player.transform.position = new Vector3(player.position.x, player.position.y + speed * Time.deltaTime, player.position.z);

            }


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player" && enterOnce)
        {
            enterOnce = false;
            StartCoroutine(DisableEnterOnce());
            collision.gameObject.GetComponent<PlayerMove>().LoseLife(6);

        }
        else if (tag == "Enemy")
        {
            collision.gameObject.GetComponent<PigScript>().takeDamage(0, 100f);
        }
        else if (tag == "ShadowPig")
        {
            collision.gameObject.GetComponent<ShadowPig>().takeDamage(0, 100f);
        }
        else if (tag == "CannonHead")
        {
            collision.gameObject.GetComponent<EnemyScript>().LoseLife(100);
        }

    }

    private IEnumerator DisableEnterOnce(){
        yield return new WaitForSeconds(1f);
        enterOnce = true;
    }





}
