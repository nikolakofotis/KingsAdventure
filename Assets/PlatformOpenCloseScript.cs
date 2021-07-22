using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOpenCloseScript : MonoBehaviour
{
    public BoxCollider2D myCollider;
    public bool open;
    public Animator animator;
    public AudioClip clip;
    public AudioSource source;

    void Start()
    {
        open = false;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("Open", open);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RockBullet") && !open)
        {
            SoundSystem.PlaySound(clip, false, source);
            open = true;
            myCollider.offset = new Vector2(-0.01189518f, 0.01727605f);
            myCollider.size = new Vector2(10.26407f, 0.2704625f);
        }
    }
}
