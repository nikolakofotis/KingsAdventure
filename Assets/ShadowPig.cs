using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPig : MonoBehaviour




{
    public Rigidbody2D pig;
    public Transform start, back, front, stopFront, stopBack, startIgnore, endIgnore;
    private Transform player;
    private float speed, lives, timeNow, force;
    public bool isMoving, attack, setFire;
    public Animator animator;
    private Vector2 forceVector;
    private RaycastHit2D seePlayerFront, seePlayerBack, checkSrnd, checkForCannon;
    public bool sawCannon, isGrounded;
    public CrateScript crate;
    public bool blockMovement, isDead;
    public string pigFacing;
    public RectTransform healthBar;
    public float x, y;
    public AudioSource takeDmgSource, hitSource, footstepsSource,poofSource;
    public AudioClip[] takeDmgSounds;
    public AudioClip poofSound;
    public AudioClip hitSound, footsteps;
    private float timeNew;
    public GameObject shadowCloud;
    public bool dissapearLock;
    private Pooling p;
    private int dir;
    private float timer;
    public bool run;








    void Start()
    {
        run = false;
        speed = 3.5f;
        lives = 100f;
        isMoving = false;
        attack = false;
        sawCannon = false;
        blockMovement = false;
        isDead = false;
        dissapearLock = false;
        player = GameObject.Find("Player").GetComponent<Transform>();
        p = player.gameObject.GetComponent<Pooling>();



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
            timer += Time.deltaTime;
            if (dissapearLock == false && timer > 0.5f)
            {
                seePlayerFront = Physics2D.Linecast(start.position, front.position, 1 << LayerMask.NameToLayer("Player"));
                seePlayerBack = Physics2D.Linecast(start.position, back.position, 1 << LayerMask.NameToLayer("Player"));
                checkForCannon = Physics2D.Linecast(start.position, stopFront.position, 1 << LayerMask.NameToLayer("Cannon"));
                checkSrnd = Physics2D.Linecast(startIgnore.position, stopFront.position, 1 << LayerMask.NameToLayer("AttackableOBJ"));
                timer = 0;
                RandomSpawn();
            }
            timeNew = timeNew + Time.deltaTime;


            animator.SetBool("Run", isMoving);
            animator.SetBool("Attack", attack);
            animator.SetBool("TookDMG", false);
            animator.SetBool("IsDead", isDead);
            animator.SetBool("SetFire", setFire);
            timeNow = Time.deltaTime + timeNow;



            // ManageHealthBar(lives);


            if (transform.rotation.y == 0)
            {
                pigFacing = "left";
                force = -2f;
            }
            else if (transform.rotation.y == -1f)
            {
                pigFacing = "right";
                force = 2f;
            }



            if (blockMovement == false)
            {
                AutoMove();
                CannonCheck();
            }
        }


    }

    void AutoMove()
    {
        if (checkSrnd.collider == null)
        {
            if (seePlayerFront)
            {
                player = GameObject.Find("Player").GetComponent<Transform>();
                if (transform.position.x < player.position.x)
                {
                    MoveTowards(1f, stopFront);
                    dir = 1;



                }
                else if (player.position.x < transform.position.x)
                {

                    MoveTowards(-1f, stopFront);
                    dir = -1;


                }
            }
            else if (seePlayerBack)
            {

                player = GameObject.Find("Player").GetComponent<Transform>();

                if (player.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector2(0f, 0f);




                }
                else
                {
                    transform.eulerAngles = new Vector2(0f, 180f);


                }


            }
            else
            {
                player = GameObject.Find("Player").GetComponent<Transform>();
                if (!checkIfUp())
                {
                    isMoving = false;
                    attack = false;
                }
            }
        }
        else if (checkSrnd.collider.gameObject.CompareTag("Crate"))
        {


            crate = checkSrnd.collider.gameObject.GetComponent<CrateScript>();
            crate.hitCrate(0.2f);
            attack = true;
            StartCoroutine(attackWait(0.3f));



        }
        else if (checkSrnd.collider.CompareTag("Enemy"))
        {
            isMoving = false;
        }





    }

    void MoveTowards(float direction, Transform check)
    {
        RaycastHit2D stopMoving = Physics2D.Linecast(start.position, check.position, 1 << LayerMask.NameToLayer("Player"));


        if (stopMoving == false)
        {

            isMoving = true;
            if (sawCannon == false)
            {
                PlayFootsteps();
                transform.position = new Vector2(transform.position.x + speed * direction * Time.fixedDeltaTime, transform.position.y);
            }
        }

        if (stopMoving)
        {
            if (stopMoving.collider.CompareTag("Player"))
            {

                float delay = 1.2f;
                isMoving = false;


                if (timeNow > delay)
                {
                    attack = true;
                    SoundSystem.PlaySound(hitSound, false, hitSource);
                    stopMoving.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(force * 1.5f, 0), ForceMode2D.Impulse);
                    stopMoving.collider.gameObject.GetComponent<PlayerMove>().LoseLife(1f);
                    timeNow = 0;
                    StartCoroutine(AttackHelp());


                }
                //else attack = false;


            }



        }


    }

    private IEnumerator AttackHelp()
    {
        yield return new WaitForSeconds(0.4f);
        attack = false;
    }

    public void takeDamage(float force, float damage)
    {

        lives = lives - damage;
        int x = Random.Range(0, 3);
        SoundSystem.PlaySound(takeDmgSounds[x], false, takeDmgSource);
        animator.SetBool("hit", true);
        StartCoroutine(HitDisable());


        if (force >= 0)
        {
            forceVector = new Vector2(force * 2.5f, force * 4.5f);
        }
        else if (force < 0)
        {
            forceVector = new Vector2(force * 2.5f, -force * 4.5f);
        }

        pig.AddForce(forceVector, ForceMode2D.Impulse);

        if (lives <= 0)
        {
            isDead = true;
            blockMovement = true;
            GameObject temp =Instantiate(shadowCloud,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
            SoundSystem.PlaySound(poofSound, false, poofSource);
            p.EnQ("ShadowPig", gameObject, 0.2f);
            //Destroy(gameObject, 0.2f);
            Destroy(temp, 0.5f);
        }


    }

    private IEnumerator HitDisable()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("hit", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else isGrounded = false;
    }

    IEnumerator attackWait(float secs)
    {
        yield return new WaitForSeconds(secs);
        attack = false;
    }

    private void CannonCheck()
    {
        if (checkForCannon.collider != null)
        {
            if (checkForCannon.collider.CompareTag("Cannon"))
            {
                isMoving = false;
                sawCannon = true;

                CannonScript cannon = checkForCannon.collider.gameObject.GetComponent<CannonScript>();
                string direction = cannon.isFacing;
                if (pigFacing == direction)
                {
                    setFire = true;
                    cannon.shootCannon = true;
                    if (cannon.triggered)
                    {
                        setFire = false;
                    }

                }


            }



        }
        else
        {
            sawCannon = false;
            setFire = false;

        }




    }

    private void ManageHealthBar(float lives)
    {

        healthBar.sizeDelta = new Vector2(lives / 100, 0.0526f);
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


    private void RandomSpawn()
    {
        float x = Random.value;
        RaycastHit2D frontRay = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + 10, transform.position.y), 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D backRay = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x - 10, transform.position.y), 1 << LayerMask.NameToLayer("Player"));

        if (x<0.05f && (PlayerCheck(frontRay)|| PlayerCheck(backRay) ))
        {
            
            SoundSystem.PlaySound(poofSound, false, poofSource);
            GameObject temp = Instantiate(shadowCloud, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            dissapearLock = true;
            StartCoroutine(player.GetComponent<PlayerMove>().RemoteReappear(gameObject,lives));
            Destroy(temp, 0.5f);
            SetLives(100f);
            p.EnQ("ShadowPig", gameObject, 0.2f);
           // gameObject.SetActive(false);

           
            


        }


    }

    public void SetLives(float live)
    {
        lives = live;
    }

   
    private bool PlayerCheck(RaycastHit2D ray)
    {
       
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + 7, transform.position.y));
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x - 7, transform.position.y));
        if (ray.collider==null)
        {
            return false;
        }
        else if(ray.collider!= null )
        {
            if (ray.collider.CompareTag("Player"))
            {
                return true;
                
            }
            else return false;
            
        }
        else return false;

    }


    private bool checkIfUp()
    {



        RaycastHit2D detect1 = Physics2D.Linecast(transform.position, transform.position + new Vector3(1 * dir, 1, 0), 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D detect2 = Physics2D.Linecast(transform.position, transform.position + new Vector3(1 * -dir, 1, 0), 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawLine(transform.position, transform.position + new Vector3(1 * dir, 1, 0));
        Debug.DrawLine(transform.position, transform.position + new Vector3(1 * -dir, 1, 0));

        if (detect1.collider != null || detect2.collider != null)
        {



            if (dir == 1)
            {
                transform.eulerAngles = new Vector2(0f, 0f);
            }
            else if (dir == -1)
            {
                transform.eulerAngles = new Vector2(0f, 180f);
            }


            transform.position = new Vector2(transform.position.x + 2.5f * (-dir) * Time.fixedDeltaTime, transform.position.y);
            isMoving = true;

            return true;



        }
        else return false;



    }

}
