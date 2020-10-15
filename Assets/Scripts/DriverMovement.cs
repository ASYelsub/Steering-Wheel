using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script has the driver turn its arms expecting the wheel to turn
//it also makes the entire apparatus move forward
public class DriverMovement : MonoBehaviour
{
    private bool rotatingLeft;
    private bool rotatingRight;
    public float initialTimer;
    private float rotateTimer;


    [SerializeField]
    private GameObject arms;

    private int randomInt;
    private float movementFloat;
    
    private void Start()
    {
        ResetRandoms();
    }

    private void FixedUpdate()
    {
        //Code for dug arms movement
        if (movementFloat > 0)
        {
            movementFloat -= Time.deltaTime;
        }
        else
        {
            if (randomInt > 1)
            {
                rotatingLeft = true;
            }

            else if (randomInt <= 1)
            {
                rotatingRight = true;
            }
        }
        if (rotateTimer > 0)
        {
            if (rotatingLeft)
            {
                arms.transform.Rotate(0, Mathf.Lerp(0, -45, 1 * Time.deltaTime), 0);
                rotateTimer -= Time.deltaTime;
            }

            if (rotatingRight)
            {
                arms.transform.Rotate(0, Mathf.Lerp(0, 45, 1 * Time.deltaTime), 0);
                rotateTimer -= Time.deltaTime;
            }
        }
        else
        {
            rotateTimer = initialTimer;
            rotatingLeft = false;
            rotatingRight = false;
            ResetRandoms();
        }
        
        //code for entire car movement
        MoveForward();
    }

    void ResetRandoms()
    {
        //used to determine left or right
        randomInt = UnityEngine.Random.Range(0, 3);

        //how long it will take for dug to turn
        movementFloat = UnityEngine.Random.Range(2, 5);
        print(randomInt);
        //print(rotatingLeft);
        //print(rotatingRight);
    }

    void MoveForward()
    {
        gameObject.transform.Translate(new Vector3(0,0,-.1f));
    }
}
