using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public string portalType;
    private PlayerMove player;
    public AudioClip spawnClip, portalClip;
    public AudioSource source;
    private bool playOnce;
    
    void Start()
    {
        playOnce = true;
        source = gameObject.GetComponent<AudioSource>();
        source.clip = spawnClip;
        source.Play();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        player.SetNewPortals(gameObject);
        
    }

    
    void FixedUpdate()
    {
        if(!source.isPlaying && playOnce)
        {
            playOnce = false;
            source.clip = portalClip;
            source.loop = true;
            source.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && portalType=="in")
        {
           player.teleportPortals();
        }

    }





    



}
