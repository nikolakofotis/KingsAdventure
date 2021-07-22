using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    public AudioSource source;
    public PlayerMove player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      player=  GameObject.Find("Player").GetComponent<PlayerMove>();
        source = gameObject.GetComponent<AudioSource>();
        Fade();
    }

    private void Fade()
    {
        float distance;
        distance = Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position);
        if(distance>10f)
        {
            source.volume = 0;
        }
        else
        {
            //source.volume = 1 / distance;
            source.volume = (-distance/10)+1.3f;
        }
        
    
    }
}
