using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CandleScript : MonoBehaviour
{
    public Light2D light;
    float radius;
    private float time;
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        time = Time.fixedDeltaTime + time;
        LightBehavior();
    }

    private void LightBehavior()
    {
        if (time > 0.1f)
        {
            time = 0f;
            radius = Random.Range(0.9f, 1f);
            light.intensity = radius;
        }
    }
}
