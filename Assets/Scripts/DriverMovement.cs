using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script has the driver turn its arms expecting the wheel to turn
//it also makes the entire apparatus move forward
public class DriverMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject arms;

    private void FixedUpdate()
    {
        MoveForward();
    }

    void MoveForward()
    {
        gameObject.transform.Translate(new Vector3(0,0,-.1f));
    }
}
