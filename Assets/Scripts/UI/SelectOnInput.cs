﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start()
    {
        //A bug auto reloads game
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("VerticalController"));
        if (Input.GetAxisRaw("VerticalController") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

        if (Input.GetAxisRaw("VerticalKeyboard1") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}