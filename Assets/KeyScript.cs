using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public int isKeyfor;
    public AudioClip keyAudio;
    public AudioSource keySource;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().SetKeyLevel(isKeyfor);
            SoundSystem.PlaySound(keyAudio, false, keySource);
            transform.position = new Vector3(100, 150, 0);
            Destroy(gameObject,0.251f);
        }
    }
}
