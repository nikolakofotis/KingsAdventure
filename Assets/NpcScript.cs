using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcScript : MonoBehaviour
{
    

    public DialogueCheck check;
    private PlayerMove player;
    
    private bool enterOnce;
    void Start()
    {
        enterOnce = false;

        StartCoroutine(SetActive());
        check.GetNpc(this);
    }


    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);



        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enterOnce && collision.gameObject.CompareTag("Player"))
        {
            
            if (enterOnce)
            {
                enterOnce = false;
                check.PlayDialogue();
            }

        }
    }

    

    private IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.5f);
        enterOnce = true;
    }


}
