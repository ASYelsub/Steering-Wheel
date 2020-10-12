using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The intent of this script is to be the input where the player presses w or d and the wheel moves
//left or right over an amount of time which then turns the car left or right
//theres also the "suspicion" meter the player must keep track of, that if they dont turn the wheel at the proper time
//the human in the car will notice that they're not imapcting anything.
public class WheelTurn : MonoBehaviour
{
    //private bool wheelMoving;
    private bool rotatingLeft;
    private bool rotatingRight;
    public float initialTimer;
    private float rotateTimer;

    private void Start()
    {
      //  wheelMoving = false;
        rotatingLeft = false;
        rotatingRight = false;
        rotateTimer = initialTimer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotatingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        { 
            rotatingLeft = true;
        }
    }

    private void FixedUpdate()
    {
        if (rotateTimer > 0)
        {
            if (rotatingLeft)
            {
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,-45, 1 * Time.deltaTime));
                rotateTimer -= Time.deltaTime;
            }

            if (rotatingRight)
            {
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,45,1 * Time.deltaTime));
                rotateTimer -= Time.deltaTime;
            }
        }
        else
        {
            rotateTimer = initialTimer;
            rotatingLeft = false;
            rotatingRight = false;
        }
        
        
    }
}
