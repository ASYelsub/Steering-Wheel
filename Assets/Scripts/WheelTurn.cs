using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private  AudioClip[] computerSounds = new AudioClip[5];
    [SerializeField] private  AudioSource roachSource = new AudioSource();
    [SerializeField] private AudioSource computerSource = new AudioSource();
    private int randomAudioInt;
    
    [SerializeField]
    private DriverMovement driverMovement;

    private float driverRotateTimer;
    private float suspicianMax = 1.450774f;
    private float susTimer = 0f;


    private int goodResponseCounter = 0;
    private int badResponsecounter = 0;
    private int testerInt = 0;

    private float soundTimer = .1f;

    private bool bufferActivated = false;
    private float bufferTimer = 2f;
    
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
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
         //   MoveTextUp(badJobResponse[badResponsecounter]);
          //  badResponsecounter++;
        //}
    }

    private void FixedUpdate()
    {
        if (testerInt == 0)
        {
            testerInt = 1;
        }
        driverRotateTimer = driverMovement.rotateTimer;
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
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,-45, Time.deltaTime));
                driverMovement.gameObject.transform.Rotate(0,Mathf.Lerp(0,-45, Time.deltaTime),0);
                rotateTimer -= Time.deltaTime;
            }

            if (rotatingRight)
            {
                gameObject.transform.Rotate(0,0,Mathf.Lerp(0,45,  Time.deltaTime));
                driverMovement.gameObject.transform.Rotate(0,Mathf.Lerp(0,45, Time.deltaTime),0);
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
        if (rotateTimer - driverRotateTimer > 1.5f && computerSource.isPlaying == false && testerInt > 0
            && bufferActivated == false)
        {
            BadFunc();
        }
        if (rotateTimer - driverRotateTimer < 1.5f && computerSource.isPlaying == false)
            if((driverMovement.rotatingRight && rotatingLeft)|| (driverMovement.rotatingLeft && rotatingRight))
            {
                GoodFunc();
                print("this is happening");
            }

        //The rotate timer additional condition is in here because
        //the roach could have very quickly shifted from left to right
        if (driverMovement.rotatingLeft && rotatingLeft && computerSource.isPlaying == false)
        {
            BadFunc();
            bufferActivated = true;
        }

        if (driverMovement.rotatingRight && rotatingRight && computerSource.isPlaying == false)
        {
            BadFunc();
            bufferActivated = true;
        }
        
        
        if (soundTimer < 3f)
        {
            soundTimer += Time.deltaTime;
        }
        else
        {
            PlaySound();
        }

        if (bufferActivated == true)
        {
            bufferTimer -= Time.deltaTime;
        }

        if (bufferTimer < 0f)
        {
            bufferActivated = false;
            bufferTimer = 2f;
        }
    }


    void PlaySound()
    {
        int randomInt = UnityEngine.Random.Range(0, 7);
        roachSource.PlayOneShot(roachSounds[randomInt]);
        soundTimer = 0;
        soundTimer = UnityEngine.Random.Range(0.2f, .6f);
    }

    void BadFunc()
    {
        //print("ITS HAPPENING");
        if (badResponsecounter >= badJobResponse.Length - 1)
        {
            SceneManager.LoadScene(2);
            print("GAME OVER");
        }
        MoveTextUp(badJobResponse[badResponsecounter]);
        badResponsecounter++;
    }


    void GoodFunc()
    {
        if (goodResponseCounter >= goodJobResponse.Length - 1)
        {
            SceneManager.LoadScene(3);
            print("GAME WON");
        }
        MoveTextUp(goodJobResponse[goodResponseCounter]);
        goodResponseCounter++;
    }
    

    void MoveTextUp(string inputtedString)
    {
        randomAudioInt = UnityEngine.Random.Range(0, 5);
        computerSource.PlayOneShot(computerSounds[randomAudioInt]);
        switch (textValidCounter)
        {
            case 0:
                responseText[0].text = inputtedString;
                break;
            case 1:
                responseText[1].text = responseText[0].text;
                responseText[0].text = inputtedString;
                break;
            case 2:
                responseText[2].text = responseText[1].text;
                responseText[1].text = responseText[0].text;
                responseText[0].text = inputtedString;
                break;
            default:
                responseText[3].text = responseText[1].text;
                responseText[2].text = responseText[2].text;
                responseText[1].text = responseText[0].text;
                responseText[0].text = inputtedString;
                break;
        }

        textValidCounter++;
    }
}
