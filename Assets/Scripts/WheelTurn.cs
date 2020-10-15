using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField] private string[] responseStrings;

    private int textValidCounter = 0;
    //wtf is up with like editing UI through code dude
    [SerializeField] private GameObject suspicianMeter;
    
    [SerializeField] private Text[] responseText = new Text[4];
    
    [SerializeField]
    private DriverMovement driverMovement;

    private void Start()
    {
        for (int i = 0; i < responseText.Length; i++)
        {
            responseText[i].text = " ";
        }
      // wheelMoving = false;
        rotatingLeft = false;
        rotatingRight = false;
        rotateTimer = initialTimer;
        //RectTransform suspicianTransform = suspicianMeter.GetComponent<RectTransform>();
        //suspicianTransform.position = new Vector3(0, suspicianTransform.position.y,suspicianTransform.position.z);
        //suspicianMeter.transform.position = suspicianTransform.position;
    }

    private void Update()
    {
        if (!rotatingLeft && !rotatingRight)
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
        
        //testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveTextUp();
        }
    }

    private void FixedUpdate()
    {
        if (rotateTimer > 0)
        {
            if (rotatingLeft)
            {
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,-45, 1 * Time.deltaTime));
                driverMovement.gameObject.transform.Rotate(0,Mathf.Lerp(0,-45, 1 * Time.deltaTime),0);
                rotateTimer -= Time.deltaTime;
            }

            if (rotatingRight)
            {
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,45,1 * Time.deltaTime));
                driverMovement.gameObject.transform.Rotate(0,Mathf.Lerp(0,45,1 * Time.deltaTime),0);
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


    void MoveTextUp()
    {
           
        //first time it's pressed, responseText[0] = responseStrings[0];
            // all other responseText = " ";
        //if input, push the text up the array.
        if (textValidCounter < responseStrings.Length)
        {
            switch (textValidCounter)
            { case 0:
                    responseText[0].text = responseStrings[0];
                    break;
                case 1:
                    responseText[1].text = responseStrings[0];
                    responseText[0].text = responseStrings[1];
                    break;
                case 2:
                    responseText[2].text = responseStrings[0];
                    responseText[1].text = responseStrings[1];
                    responseText[0].text = responseStrings[2];
                    break;
                case 3:
                    responseText[3].text = responseStrings[0];
                    responseText[2].text = responseStrings[1];
                    responseText[1].text = responseStrings[2];
                    responseText[0].text = responseStrings[3];
                    break;
                default: //after setting the first four
                    for (int i = 0; i < responseText.Length; i++)
                    {
                        responseText[i].text = responseStrings[textValidCounter - i];
                    }
                    break;
            }

            textValidCounter++;   
        }
        else
        {
            print("Process done.");
        }
    }
}
