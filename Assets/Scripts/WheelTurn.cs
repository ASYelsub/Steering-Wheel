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

    [SerializeField] private string[] goodJobResponse;

    [SerializeField] private string[] badJobResponse;

    [SerializeField] private string[] crashResponse;

    private int textValidCounter = 0;
    //wtf is up with like editing UI through code dude
    [SerializeField] private GameObject suspicianMeter;
    
    [SerializeField] private Text[] responseText = new Text[4];
    
    [SerializeField] private AudioClip[] roachSounds = new AudioClip[6];
    [SerializeField] private  AudioSource roachSource = new AudioSource();
    private int randomAudioInt;
    
    [SerializeField]
    private DriverMovement driverMovement;

    private float driverRotateTimer;
    private float suspicianMax = 1.450774f;
    private float susTimer = 0f;
    private void Start()
    {
        //gonna use this to compare rotation timers between roach and car
        driverRotateTimer = driverMovement.rotateTimer;

        
        for (int i = 0; i < responseText.Length; i++)
        {
            responseText[i].text = " ";
        }
        // wheelMoving = false;
        rotatingLeft = false;
        rotatingRight = false;
        rotateTimer = initialTimer;
        RectTransform suspicianTransform = suspicianMeter.GetComponent<RectTransform>();
        suspicianTransform.sizeDelta = new Vector2(0,0.05785748f);
        //suspicianTransform.position = new Vector3(0, suspicianTransform.position.y,suspicianTransform.position.z);
        //suspicianMeter.transform.position = suspicianTransform.position;
    }

    private void Update()
    {
        driverRotateTimer = driverMovement.rotateTimer;
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
        RectTransform suspicianTransform = suspicianMeter.GetComponent<RectTransform>();
        suspicianTransform.sizeDelta = new Vector2(susTimer, 0.05785748f);
        //suspicianTransform.rect.width = new Vector2(0.5735f,-0.06800008f);
        if (suspicianTransform.sizeDelta.x < suspicianMax)
        {
            susTimer += Time.deltaTime;
        }
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

        //print(driverRotateTimer + " over " + rotateTimer);
        //they both start at 2 and decrease to 0 and then reset to 2
        if (rotateTimer - driverRotateTimer > 1f && roachSource.isPlaying == false)
        {
            randomAudioInt = UnityEngine.Random.Range(0, 6);
            roachSource.PlayOneShot(roachSounds[randomAudioInt]);
            //print("ITS HAPPENING");
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
