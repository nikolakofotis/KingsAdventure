using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string greekSentence,englishSentence;
    private PlayerMove player;
    public TMPro.TMP_FontAsset greek, english;
    void Start()
    {
        StartCoroutine(StartProccess());


    }

    private void OnEnable()
    {
        StartCoroutine(StartProccess());
    }


    void FixedUpdate()
    {
        
    }

    private IEnumerator StartProccess()
    {
        yield return new WaitForSeconds(0.1f);
        text = gameObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();


        if (player.language == "el")
        {
            text.text = greekSentence;
            text.fontStyle = FontStyles.Bold;
            text.font = greek;
            
        }
        else
        {
            text.font = english;
            text.text = englishSentence;
            text.fontStyle = FontStyles.Normal;

        }
    }
}
