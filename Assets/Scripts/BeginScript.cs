using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginScript : MonoBehaviour
{

    [SerializeField]
    private AudioSource roachSource;
    [SerializeField]
    private AudioClip[] roachSounds = new AudioClip[6];

    private float soundTimer = 0f;
    private int randomInt;
    
    private void Start()
    {
        //pick a sound
        soundTimer = .1f;
        //soundTimer = UnityEngine.Random.Range(0.2f, .6f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }

    void FixedUpdate()
    {
        if (soundTimer < 3f)
        {
            soundTimer+= Time.deltaTime;
        }
        else
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        randomInt = UnityEngine.Random.Range(0, 7);
        roachSource.PlayOneShot(roachSounds[randomInt]);
        soundTimer = 0;
        soundTimer = UnityEngine.Random.Range(0.2f, .6f);
    }
}
