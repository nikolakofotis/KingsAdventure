using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    public GameObject[] levels;
    private PlayerMove player;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }


    public void OpenSelectCanvas()
    {
        SoundSystem.PlaySound(clip, false, source);
        gameObject.SetActive(true);
        player= GameObject.Find("Player").GetComponent<PlayerMove>();
        CheckLevels();
    }


    public void CloseSelectCanvas()
    {
        gameObject.SetActive(false);
    }


    private void CheckLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

            for (int i = 0; i < player.hasKeyForLevels; i++)
        {
            print(levels.Length);
            levels[i].SetActive(true);
            
        }
    }
}
