using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{

#region Fields

    [SerializeField]
    GameObject bazookaBullet;

    [SerializeField]
    GameObject Launcher;

    [SerializeField]
    GameObject heldGrenade;

    [SerializeField]
    GameObject shotGrenade;

    [SerializeField]
    Transform gunLocation;

    Rigidbody rigidbody;

    [SerializeField]
    float playerSpeed;

    [SerializeField]
    float health;

    public float Health
    {
        get
        {
            return health;
        }
    }


    [SerializeField]
    int playerNumber; //With Multiple Characters remove number

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
    float chargeRate;

    [SerializeField]
    float minCharge;

    [SerializeField]
    float maxCharge;

    [SerializeField]
    float currentCharge;

    enum Weapons {Grenade, Bazooka};
    [SerializeField]
    Weapons ActiveWeapon;

    [SerializeField]
    bool canShoot;

    [SerializeField]
    float shootSpeed;

    GameObject activeWeapon;

#endregion

    // Use this for initialization
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        ActiveWeapon = Weapons.Bazooka;

        canShoot = true;

        transform.parent = null;

        activeWeapon = Instantiate(Launcher, gunLocation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotatePlayerController();
        MovementController();
        //MovementKeyboard();

        SwitchWeapon();

        if (canShoot)
        {
            ShootWeapon();
        }

        else
        {
            StartCoroutine(CanShoot());
        }
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

        Vector3 movement = new Vector3(moveHortizontal, 0.0f, moveVertical );
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

                ActiveWeapon = Weapons.Bazooka;
                Destroy(activeWeapon);
                activeWeapon = Instantiate(Launcher, gunLocation);

            }

            else if (ActiveWeapon == Weapons.Bazooka)
            {
                
                ActiveWeapon = Weapons.Grenade;
                Destroy(activeWeapon);
                activeWeapon = Instantiate(heldGrenade, gunLocation);

            }

        }

        if(Input.GetButtonDown("WeaponRightController" + playerNumber))
        {
            if (ActiveWeapon == Weapons.Bazooka)
            {
                ActiveWeapon = Weapons.Grenade;
                
                Destroy(activeWeapon);
                activeWeapon = Instantiate(heldGrenade, gunLocation);
            }

 
            else if (ActiveWeapon == Weapons.Grenade)
            {
                ActiveWeapon = Weapons.Bazooka;
                Destroy(activeWeapon);
                activeWeapon = Instantiate(Launcher, gunLocation);
            }
  
        }

    }

    void ShootWeapon()
    {
        if(currentCharge > maxCharge * 1.2)
        {
            Explode();
        }

        if(Input.GetButtonDown("WeaponShootController" + playerNumber))
        {
            currentCharge = 0;
        }

        if(Input.GetButton("WeaponShootController" + playerNumber))
        {
            currentCharge += chargeRate;

        }

        if(Input.GetButtonUp("WeaponShootController" + playerNumber))
        {
            if(currentCharge < minCharge)
            {
                currentCharge = minCharge;
            }
            if(currentCharge > maxCharge)
            {
                currentCharge = maxCharge;
            }
            Fire();
        }
    }

    void Fire()
    {
        //Fire weapon based on charge

        if(ActiveWeapon == Weapons.Bazooka)
        {
            GameObject bazookaAmmo = Instantiate(bazookaBullet, gunLocation, false);

            bazookaAmmo.GetComponent<BazookaShell>().CurrentCharge = currentCharge;

        }

        if(ActiveWeapon == Weapons.Grenade)
        {
            GameObject grenades = Instantiate(shotGrenade, gunLocation, false);
            grenades.GetComponent<Grenade>().CurrentCharge = currentCharge;
        }

        currentCharge = minCharge;
        canShoot = false;

        //StartCoroutine(CanShoot());
    }

    void Explode()
    {
        canShoot = false;
        //StartCoroutine(CanShoot());
        //Weapon Explodes and hurts player
    }

    IEnumerator CanShoot()
    {
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
