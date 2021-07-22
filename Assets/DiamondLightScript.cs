using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DiamondLightScript : MonoBehaviour
{
    public List<GameObject> candles;
    private bool entered,enableTrans;
    public Light2D fotaki;
    private Pooling pool;
    public AudioSource source;
    public AudioClip start, boom;
    public GameObject[] obstacleToDisable;

    void Start()
    {
        entered = false;
        enableTrans = false;
    }

    
    void FixedUpdate()
    {
       
        pool = GameObject.Find("Player").GetComponent<Pooling>();
        if (enableTrans)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f * Time.deltaTime, transform.position.z);

        }
        CheckPlayerPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !entered)
        {
            entered = true;
            SoundSystem.PlaySound(start, false, source);
            TurnOnLights();
        }
    }


    private void TurnOnLights()
    {
        foreach(GameObject g in candles)
        {
             g.SetActive(true);
            
        }
        foreach(GameObject g in obstacleToDisable)
        {
            g.SetActive(false);
        }

        enableTrans = true;
        StartCoroutine(Boom());
        

    }


    private IEnumerator Boom()
    {
        while(fotaki.pointLightOuterRadius>0)
        {
           
            yield return new WaitForSeconds(0.1f);
            fotaki.pointLightOuterRadius -= 0.5f;
        }
        SoundSystem.PlaySound(boom, false, source);
        GameObject g=  pool.Spawn("DiamondLightParticles", transform.position, Quaternion.identity);
        pool.EnQ("DiamondLightParticles", g, 5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        Destroy(gameObject,2f);
        //boom
    }


    private void CheckPlayerPos()
    {
        PlayerMove player = GameObject.Find("Player").GetComponent<PlayerMove>();
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance>10)
        {
            fotaki.gameObject.SetActive(false);
        }
        else
        {
            fotaki.gameObject.SetActive(true);
            fotaki.pointLightOuterRadius = 10/ distance;
        }
        
    }

}
