using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireScript : MonoBehaviour
{
   
   
    public ParticleSystem particles;
    public BoxCollider2D fireCollider;
    private bool blockCollision;
    public bool canTakeDamage;
    public Light2D light;
    void Start()
    {
        canTakeDamage = true;
        fireCollider.offset = new Vector3(0, 0.432184f);
        fireCollider.size = new Vector2(0.6666667f, 2.284368f);
    }

   
   
    public void StartFire()
    {
        fireCollider.offset = new Vector3(0, 0.432184f);
        fireCollider.size = new Vector2(0.6666667f, 2.284368f);
        light.gameObject.SetActive(true);
        particles.Play();
    }

    public void StopFire()
    {
        fireCollider.offset = new Vector3(0, -0.3629818f);
        fireCollider.size = new Vector2(0.6666667f, 0.6940365f);
        light.gameObject.SetActive(false);
        particles.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !blockCollision)
        {
            blockCollision = true;
            if (canTakeDamage)
            {
                collision.gameObject.GetComponent<PlayerMove>().LoseLife(6);
            }
            StartCoroutine(unblockFire());
        }
    }


    private IEnumerator unblockFire()
    {
        yield return new WaitForSeconds(0.5f);
        blockCollision = false;

    }




}
