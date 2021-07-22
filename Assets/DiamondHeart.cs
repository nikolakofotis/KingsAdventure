using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondHeart : MonoBehaviour
{
    public string type;
    public AudioClip[] coinSound;
    public AudioClip heartTake;

   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == "Diamond" && collision.gameObject.CompareTag("Player"))
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            player.GotObject(gameObject);
            
            int random = Random.Range(0, 1);
            player.PlayRemoteSound(coinSound[random], 1);
            collision.gameObject.GetComponent<PlayerMove>().GetDiamond();
            Destroy(gameObject);

           // collision.gameObject.GetComponent<PlayerMove>().diamonds = collision.gameObject.GetComponent<PlayerMove>().diamonds + 1;


        }
        else if (type == "Heart" && collision.gameObject.CompareTag("Player"))
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            player.PlayRemoteSound(heartTake, 1);
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerMove>().usableHearts = collision.gameObject.GetComponent<PlayerMove>().usableHearts+1;

        }
    }
}
