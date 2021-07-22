using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject whatToFollow;
    public float offsetX, offsetY;
    public bool shakeIt,shakeItLight;
   public float cameraF;
    private PlayerMove p;

    private void Start()
    {
        shakeIt = false;
        shakeItLight = false;
        whatToFollow = GameObject.Find("Player");
        p = whatToFollow.GetComponent<PlayerMove>();
    }
    void FixedUpdate()
    {
        //whatToFollow = GameObject.Find("Player");
        CameraFloat();
        FollowPlayer();

    }


    private void FollowPlayer()
    {
        
            
            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);
        if (shakeIt)
        {
            transform.position = new Vector3(whatToFollow.transform.position.x + x + cameraF+offsetX * Time.fixedDeltaTime, whatToFollow.transform.position.y + y + offsetY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(whatToFollow.transform.position.x + cameraF+offsetX * Time.fixedDeltaTime, whatToFollow.transform.position.y + offsetY, transform.position.z);
        }

        if(shakeItLight)
        {
            transform.position = new Vector3(whatToFollow.transform.position.x + Random.Range(-0.01f, 0.01f) + cameraF + offsetX * Time.fixedDeltaTime, whatToFollow.transform.position.y + Random.Range(-0.01f, 0.01f) + offsetY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(whatToFollow.transform.position.x + cameraF + offsetX * Time.fixedDeltaTime, whatToFollow.transform.position.y + offsetY, transform.position.z);
        }
    
        
    }


    public IEnumerator Shake(float duration)
    {
        shakeIt = true;
        yield return new WaitForSeconds(duration);
        
        shakeIt = false;
        

        
    }
    public IEnumerator ShakeLight(float duration)
    {
        shakeItLight = true;
        yield return new WaitForSeconds(duration);

        shakeItLight = false;



    }



    private void CameraFloat()
    {
        
        float force = p.force / Mathf.Abs(p.force);
        
        if(p.move && Mathf.Abs(cameraF) < 2)
        {
            cameraF = cameraF + 0.02f * force;
        }
        else if(!p.move && CheckPos())
        {
           cameraF = cameraF - 0.02f*MoveC();
        }


        

        
    }

    private bool CheckPos()
    {
        int myPos= (int)gameObject.transform.position.x;
        int playerPos = (int)whatToFollow.transform.position.x;
        if (myPos == playerPos)
        {
            return false;
        }
        else return true;

    }

    private int MoveC()
    {
        if (whatToFollow.transform.position.x > gameObject.transform.position.x)
        {
            return -1;
        }
        else if (whatToFollow.transform.position.x < gameObject.transform.position.x)
        {
            return 1;
        }
        else return 0;
    }






}


    

