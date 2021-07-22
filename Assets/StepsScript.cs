using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsScript : MonoBehaviour
{
   public  bool isTrap,isPlatform;
    private float x ,time,time2;
    private bool activate,block;
    private Vector3 startingPosition;
    public AudioSource trap1Source, trap2Source,fallingSource;
    public AudioClip stones, earthQuake,fell,falling;
    public bool playSound , isGrounded,playOne;
    public GameObject light;
    
   
    

    void Start()
    {
        startingPosition = transform.position;
        trap2Source.volume = 0.5f;
        playSound = true;
        playOne = true;
        block = false;
    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;
       
        if(activate &&!block)
        {
            SetTrap();
        }

    }


  


  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTrap)
        {
            activate = true;
        }
       
    }


    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
           
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().isGrounded = true;
        }
    }


    private void SetTrap()
    {
        float del;
        StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeLight(0.4f));

        if (isPlatform)
        {
            del = 0.1f;

        }
        else del = 0.4f;
        


        x = Random.Range(-2.5f, 2.5f);
        float y = Random.Range(-2.5f,2.5f);
       
        

        if (time > 0.08f)
        {
            time = 0f;
            // transform.rotation = Quaternion.Euler(0, 0, x);
            PlayOnce();
           // transform.position = new Vector3(transform.position.x + y * Time.deltaTime, transform.position.y + x * Time.deltaTime, transform.position.z);
            time2 = time2 + Time.deltaTime;
            
            if (time2>del)
            {
                block = true;
                if (playOne)
                {
                    
                    
                    StartCoroutine(WaitTillGrounded());
                }
                Debug.Log("fell");
                SoundSystem.StopPlayingSound(stones, trap1Source);
                SoundSystem.StopPlayingSound(earthQuake, trap2Source);
                trap1Source.volume = 0.5f;
                SoundSystem.PlaySound(falling, false, trap1Source);
                activate = false;
              Rigidbody2D temp=  gameObject.GetComponent<Rigidbody2D>();
                temp.bodyType = RigidbodyType2D.Dynamic;

            }
        }        
    }


    public void ResetSteps()
    {
        if(isTrap)
        {
            transform.position=startingPosition;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            time = 0f;
            time2 = 0f;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isTrap = true;
            block = false;
           activate = false;
        }

    }

    private void PlayOnce()
    {
        if (playSound)
        {
           // SoundSystem.PlaySound(stones, false, trap1Source);
           // SoundSystem.PlaySound(earthQuake, false, trap2Source);
            playSound = false;
        }

    }


    private IEnumerator WaitTillGrounded()
    {
        playOne = false;
        yield return new WaitUntil(() => isGrounded);
        SoundSystem.PlaySound(fell, false, fallingSource);
        Debug.Log("here");

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isTrap)
        {
            light.gameObject.SetActive(false);
        }
    }
}

