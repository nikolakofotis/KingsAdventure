using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSawScript : MonoBehaviour
{
    private Vector3 startingPoint;
    public bool moveBackAndForth, moveUpDown;
    
    void Start()
    {
        startingPoint = transform.position;
    }

    
    void FixedUpdate()
    {
        
    }


    private void Move()
    {
        if(moveBackAndForth)
        {
           
        }
        else if(moveUpDown)
        {

        }
    }



   

}
