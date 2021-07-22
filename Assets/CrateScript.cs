using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public GameObject bCrate1, bCrate2, bCrate3, bCrate4;
    public PlayerMove player;
    private Vector2 forceVector;
    public GameObject diamond;
    public AudioClip[] breakSound;
    public BoxCollider2D myCollider;
    


    public void DashHit()
    {
        myCollider.isTrigger = true;
    }
    
   

    public void hitCrate(float force)
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        int random = Random.Range(0, 1);
        player.PlayRemoteSound(breakSound[random], 2);
        forceVector = new Vector2(force/5, 0.1f);
        Destroy(gameObject);
        Instantiate(diamond, transform.position, Quaternion.identity);
        Instantiate(diamond, transform.position, Quaternion.identity);


        GameObject fakeCrate1=  Instantiate(bCrate1,transform.position,Quaternion.identity);
        GameObject fakeCrate2 = Instantiate(bCrate2, transform.position, Quaternion.identity);
        GameObject fakeCrate3 = Instantiate(bCrate3, transform.position, Quaternion.identity);
        GameObject fakeCrate4 = Instantiate(bCrate4, transform.position, Quaternion.identity);
        fakeCrate1.tag = "Ground";
        fakeCrate2.tag = "Ground";
        fakeCrate3.tag = "Ground";
        fakeCrate4.tag = "Ground";


        fakeCrate1.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        fakeCrate2.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        fakeCrate3.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        fakeCrate4.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);


        Destroy(fakeCrate1,1f);
        Destroy(fakeCrate2, 1f);
        Destroy(fakeCrate3, 1f);
        Destroy(fakeCrate4, 1f);

    }

    

}
