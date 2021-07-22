using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PigScript : MonoBehaviour
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
    public AudioSource takeDmgSource, hitSource, footstepsSource;
    public AudioClip[] takeDmgSounds;
    public AudioClip hitSound, footsteps;
    private float timeNew;

    private int dir;








    void Start()
    {
        speed = 2f;
        lives = 100f;
        isMoving = false;
        attack = false;
        sawCannon = false;
        blockMovement = false;
        isDead = false;



    }


    void FixedUpdate()
    {
        timeNew = timeNew + Time.deltaTime;
        player = GameObject.Find("Player").GetComponent<Transform>();
        animator.SetBool("Run", isMoving);
        animator.SetBool("Attack", attack);
        animator.SetBool("TookDMG", false);
        animator.SetBool("IsDead", isDead);
        animator.SetBool("SetFire", setFire);
        timeNow = Time.deltaTime + timeNow;
        Debug.DrawLine(startIgnore.position, stopFront.position);
        seePlayerFront = Physics2D.Linecast(start.position, front.position, 1 << LayerMask.NameToLayer("Player"));
        seePlayerBack = Physics2D.Linecast(start.position, back.position, 1 << LayerMask.NameToLayer("Player"));
        checkForCannon = Physics2D.Linecast(start.position, stopFront.position, 1 << LayerMask.NameToLayer("Cannon"));
        checkSrnd = Physics2D.Linecast(startIgnore.position, stopFront.position, 1 << LayerMask.NameToLayer("AttackableOBJ"));

        //ManageHealthBar(lives);


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

    void AutoMove()
    {
        if (checkSrnd.collider == null)
        {
            if (seePlayerFront)
            {

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


        if (force > 0)
        {
            forceVector = new Vector2(force * 6.5f, force * 4.5f);
        }
        else if (force < 0)
        {
            forceVector = new Vector2(force * 6.5f, -force * 4.5f);
        }

        pig.AddForce(forceVector, ForceMode2D.Impulse);

        if (lives <= 0)
        {
            isDead = true;
            blockMovement = true;
            Destroy(gameObject, 1f);
        }


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


    private bool checkIfUp()
    {
        
       

        RaycastHit2D detect1= Physics2D.Linecast(transform.position, transform.position + new Vector3(1 * dir, 1, 0), 1 << LayerMask.NameToLayer("Player"));
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

