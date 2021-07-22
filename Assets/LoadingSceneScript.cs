using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    public PlayerMove player;
    public Animator animation;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        
        PlayAnim();
    }

    void PlayAnim()
    {
        bool play = player.load;
        
        animation.SetBool("PlayAnim", player.load);

    }
}
