using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCannonWeapon : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().weapon = 3;
            collision.gameObject.GetComponent<PlayerMove>().animator.SetInteger("Weapon", 3);
            Destroy(gameObject);
        }
    }
}
