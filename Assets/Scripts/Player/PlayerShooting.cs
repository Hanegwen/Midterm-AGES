﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
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
    Slider shootingArrow;

    [SerializeField]
    float chargeRate;

    [SerializeField]
    float minCharge;

    [SerializeField]
    float maxCharge;

    [SerializeField]
    float currentCharge;

    enum Weapons { Grenade, Bazooka };
    [SerializeField]
    Weapons ActiveWeapon;

    [SerializeField]
    bool canShoot;

    [SerializeField]
    float shootSpeed;

    GameObject activeWeapon;

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

    bool holdShoot = false;



    // Use this for initialization
    void Start ()
    {
        shootingArrow.minValue = minCharge;
        shootingArrow.maxValue = maxCharge;
        canShoot = true;
        ActiveWeapon = Weapons.Bazooka;
        activeWeapon = Instantiate(Launcher, gunLocation);

        shootingArrow.value = currentCharge;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log("ShootingINUPDATE: " + (Input.GetAxis("WeaponShootController" + playerNumber)));
        shootingArrow.value = currentCharge;

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

    void SwitchWeapon()
    {
        if (Input.GetButtonDown("WeaponLeftController" + playerNumber))
        {
            if (ActiveWeapon == Weapons.Grenade)
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

        if (Input.GetButtonDown("WeaponRightController" + playerNumber))
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
        Debug.Log("Shooting" + (Input.GetAxis("WeaponShootController" + playerNumber)));
        //if (currentCharge > maxCharge * 1.2)
        //{
        //    Explode();
        //}
        if (Input.GetAxis("WeaponShootController" + playerNumber) < -.7 && !holdShoot)
        {
            currentCharge = 0;
            holdShoot = true;
        }

        if (Input.GetAxis("WeaponShootController" + playerNumber) < -.7 && holdShoot)
        {
            Debug.Log(("Shooting" + playerNumber));
            currentCharge += chargeRate;
            shootingArrow.value = currentCharge;
        }

        if (Input.GetAxis("WeaponShootController" + playerNumber) > -.2 && holdShoot)
        {
            if (currentCharge < minCharge)
            {
                currentCharge = minCharge;
            }
            if (currentCharge > maxCharge)
            {
                currentCharge = maxCharge;
            }
            Fire();
            holdShoot = false;
        }
    }

    void Fire()
    {
        //Fire weapon based on charge

        if (ActiveWeapon == Weapons.Bazooka)
        {
            GameObject bazookaAmmo = Instantiate(bazookaBullet, gunLocation, false);

            bazookaAmmo.GetComponent<BazookaShell>().CurrentCharge = currentCharge;

        }

        if (ActiveWeapon == Weapons.Grenade)
        {
            GameObject grenades = Instantiate(shotGrenade, gunLocation, false);
            grenades.GetComponent<Grenade>().CurrentCharge = currentCharge;
        }

        currentCharge = 0;
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

}
