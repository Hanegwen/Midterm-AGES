using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject bazookaBullet;

    [SerializeField]
    Transform gunLocation;

    Rigidbody rigidbody;

    [SerializeField]
    float playerSpeed;

    int playerNumber = 1; //With Muultiple Characters remove number

    [SerializeField]
    float minCharge;

    [SerializeField]
    float maxCharge;

    [SerializeField]
    float currentCharge;

    enum Weapons {Grenade, Bazooka, test1, test2 };
    [SerializeField]
    Weapons ActiveWeapon;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        ActiveWeapon = Weapons.Bazooka;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotatePlayerController();
        MovementController();
        //MovementKeyboard();

        SwitchWeapon();
        ShootWeapon();
	}

    void RotatePlayerController()
    {
        gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("HorizontalController" + playerNumber) , 
            Input.GetAxis("VerticalController" + playerNumber)) * 180  / Mathf.PI, 0);
    }

    void MovementController()
    {
        float moveHortizontal = Input.GetAxis("HorizontalController" + playerNumber);
        float moveVertical = Input.GetAxis("VerticalController" + playerNumber);

        Vector3 movement = new Vector3(moveHortizontal, 0.0f, moveVertical * -1);
        rigidbody.velocity = movement * playerSpeed * Time.deltaTime;
    }

    void MovementKeyboard()
    {
        float moveHorizontal = Input.GetAxis("HorizontalKeyboard" + playerNumber);
        float moveVertical = Input.GetAxis("VerticalKeyboard" + playerNumber);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * playerSpeed * Time.deltaTime;
    }

    void SwitchWeapon()
    {
        if(Input.GetButtonDown("WeaponLeftController" + playerNumber))
        {
            if(ActiveWeapon == Weapons.Grenade)
            {
                ActiveWeapon = Weapons.test2;
                
            }

            else if (ActiveWeapon == Weapons.Bazooka)
            {
                ActiveWeapon = Weapons.Grenade;
                
            }

            else if (ActiveWeapon == Weapons.test1)
            {
                ActiveWeapon = Weapons.Bazooka;
            }

            else if(ActiveWeapon == Weapons.test2)
            {
                ActiveWeapon = Weapons.test1;
            }
        }

        if(Input.GetButtonDown("WeaponRightController" + playerNumber))
        {
            if (ActiveWeapon == Weapons.test2)
            {
                ActiveWeapon = Weapons.Grenade;
            }

            else if (ActiveWeapon == Weapons.test1)
            {
                ActiveWeapon = Weapons.test2;
            }

            else if (ActiveWeapon == Weapons.Bazooka)
            {
                ActiveWeapon = Weapons.test1;
            }

            else if (ActiveWeapon == Weapons.Grenade)
            {
                ActiveWeapon = Weapons.Bazooka;
            }
  
        }

    }

    void ShootWeapon()
    {
        if(currentCharge > maxCharge)
        {
            Explode();
        }

        if(Input.GetButtonDown("WeaponShootController" + playerNumber))
        {

        }

        if(Input.GetButtonUp("WeaponShootController" + playerNumber))
        {

            Fire();
        }
    }

    void Fire()
    {
        //Fire weapon based on charge

        if(ActiveWeapon == Weapons.Bazooka)
        {
            Instantiate(bazookaBullet, gunLocation, false);
        }
    }

    void Explode()
    {
        //Weapon Explodes and hurts player
    }
}
