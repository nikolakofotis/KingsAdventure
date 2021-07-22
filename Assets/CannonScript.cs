using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject bomb;
    private float timeNow,timeNow1;
    public Transform firingPoint;
    public Animator animator;
    public string isFacing="left";
    public bool shootCannon, triggered;
    public Transform player;
    private Pooling pool;
    public AudioSource cannonSource;
    public AudioClip[] boomClip;


    void Start()
    {
      
    }

  
    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        pool = GameObject.Find("Player").GetComponent<Pooling>();

        if(isFacing=="left")
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if(isFacing=="right")
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }

        animator.SetBool("Shoot", triggered);
        timeNow = Time.deltaTime + timeNow;
        timeNow1 = Time.deltaTime + timeNow1;
        if (timeNow > 2f &&shootCannon )
        {
            timeNow = 0f;
            StartCoroutine(Shoot());
        }

       /* int offset = Random.Range(0, 2);
        int offsetY = Random.Range(-20, 20);
        RaycastHit2D topRay = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + offset), new Vector2(transform.position.x + offsetY , transform.position.y + offset), 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawLine(new Vector2(transform.position.x , transform.position.y + offset), new Vector2(transform.position.x  + offsetY, transform.position.y + offset));
        if (topRay.collider != null)
        {

            if (topRay.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(AttackPlayer());
            }
        }*/
       
       
        
    }

    public IEnumerator Shoot()
    {
        triggered = true;
        int x = Random.Range(0, 3);
        SoundSystem.PlaySound(boomClip[x], false, cannonSource);
        GameObject tempBomb =    Instantiate(bomb, firingPoint.position, Quaternion.identity);
        tempBomb.GetComponent<BombScript>().isActive = true;
        tempBomb.GetComponent<BombScript>().AddForceToBomb(10f,isFacing);
        yield return new WaitForSeconds(0.3f);
        triggered = false;
        shootCannon = false;
    }

    public IEnumerator AttackPlayer()
    {
        print("in attack");
        if((player.position.x>transform.position.x && isFacing=="right") ||(player.position.x < transform.position.x && isFacing == "left"))
        {
            Debug.DrawLine(transform.position, player.position);
            if (Mathf.Abs(transform.position.x ) - Mathf.Abs(player.position.x) <=10 )
            {
                if (timeNow1 > 4f)
                {
                    timeNow1 = 0;
                    triggered = true;
                    int x = Random.Range(0, 3);
                    //Debug.Log(x);
                    SoundSystem.PlaySound(boomClip[x], false, cannonSource);
                    // GameObject tempBomb = Instantiate(bomb, firingPoint.position, Quaternion.identity);
                    GameObject tempBomb = pool.Spawn("Bomb", firingPoint.position, Quaternion.identity);
                    tempBomb.GetComponent<BombScript>().isActive = true;
                    tempBomb.GetComponent<BombScript>().AddForceToBomb(10f, isFacing);
                    yield return new WaitForSeconds(0.3f);
                    triggered = false;
                    shootCannon = false;
                }
            }

            
           
        }
        


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackPlayer());
            
        }
    }

}
