using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawnScript : MonoBehaviour
{

    public GameObject shadowCloud;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            Transform playerTransform = collision.gameObject.GetComponent<Transform>();
           GameObject pig= collision.gameObject.GetComponent<Pooling>().Spawn("ShadowPig", new Vector3(playerTransform.position.x+1.25f*playerMove.PlayerIsLooking(),playerTransform.position.y,playerTransform.position.z),Quaternion.identity);
            pig.gameObject.GetComponent<ShadowPig>().SetLives(100f);
             GameObject cloud=Instantiate(shadowCloud, pig.transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfter(0.3f, cloud));



        }

    }

   private IEnumerator DestroyAfter(float seconds,GameObject destroy)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        Destroy(destroy);
    }


}
