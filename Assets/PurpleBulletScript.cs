using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBulletScript : MonoBehaviour
{

    private bool stop;
    private Pooling pool;
    private string portalDoor;
    
    void Start()
    {
        stop = false;
       // StartCoroutine(OpenPortal());
    }

    
    void FixedUpdate()
    {
        pool = GameObject.Find("Player").GetComponent<Pooling>();
    }


    private IEnumerator OpenPortal()
    {

        yield return new WaitForSeconds(4f);
        GameObject g = pool.Spawn("Portal", transform.position, Quaternion.identity);
         g.GetComponent<PortalScript>().portalType = portalDoor;
        pool.EnQ("PurpleBullet", gameObject, 0);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SideWalls") && !stop)
        {
            stop = true;
            GameObject g = pool.Spawn("Portal", transform.position, Quaternion.identity);
            g.GetComponent<PortalScript>().portalType = portalDoor;
            pool.EnQ("PurpleBullet", gameObject, 0);
        }
    }

    public void SetPortalDoor(string value)
    {
        portalDoor = value;
    }
}
