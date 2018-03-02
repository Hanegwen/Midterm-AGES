﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    int playerNumber = 1;
    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }
        set
        {
            playerNumber = value;
        }
    }
    [SerializeField]
    float playerSpeed;

    Rigidbody rigidbody1;

    // Use this for initialization
    void Start ()
    {
        transform.parent = null;
        rigidbody1 = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        RotatePlayerController();
        MovementController();
        //MovementKeyboard();
        


    }

    void RotatePlayerController()
    {
        gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("HorizontalControllerRight" + playerNumber),
            Input.GetAxis("VerticalControllerRight" + playerNumber)) * 360 / Mathf.PI, 0);
    }

    void MovementController()
    {
        float moveHortizontal = Input.GetAxis("HorizontalController" + playerNumber);
        float moveVertical = Input.GetAxis("VerticalController" + playerNumber);

        Vector3 movement = new Vector3(moveHortizontal, 0.0f, moveVertical * -1);
        if (movement != new Vector3(0, 0, 0))
        {
            Debug.Log(Input.GetJoystickNames()[(playerNumber - 1)] + " is moved");
        }
        rigidbody1.velocity = movement * playerSpeed * Time.deltaTime;
    }

    void MovementKeyboard()
    {
        float moveHorizontal = Input.GetAxis("HorizontalKeyboard" + playerNumber);
        float moveVertical = Input.GetAxis("VerticalKeyboard" + playerNumber);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody1.velocity = movement * playerSpeed * Time.deltaTime;
    }

}
