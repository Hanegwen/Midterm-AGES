using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rigidbody;

    [SerializeField]
    float playerSpeed;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovementKeyboard();
	}

    void MovementController()
    {
        float moveHortizontal = Input.GetAxis("HorizontalController1");
        float moveVertical = Input.GetAxis("VerticalController1");

        Vector3 movement = new Vector3(moveHortizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * playerSpeed * Time.deltaTime;
    }

    void MovementKeyboard()
    {
        float moveHorizontal = Input.GetAxis("HorizontalKeyboard1");
        float moveVertical = Input.GetAxis("VerticalKeyboard1");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * playerSpeed * Time.deltaTime;
    }
}
