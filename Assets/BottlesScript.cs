using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesScript : MonoBehaviour
{
    public int bottleCode;
    public AudioClip clip;
    public GameObject particles;
    public string color;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            collision.gameObject.GetComponent<PlayerMove>().bulletType = color;
            if(color=="purple")
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().purpleUI.SetActive(true);
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().purpleUI.SetActive(false);
            }
            collision.gameObject.GetComponent<PlayerMove>().PlayRemoteSound(clip, 2);
            
        }
    }

    

   



}
