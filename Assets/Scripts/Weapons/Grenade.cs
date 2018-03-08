using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ShotWeapon
{
    float lifeTime = 6;

    CapsuleCollider capsuleCollider;


    // Use this for initialization
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        Destroy(this.gameObject, lifeTime);
    }

    protected override void IUpdate()
    {
        wait += Time.deltaTime;
        if(wait * Time.deltaTime > .8f)
        {
            capsuleCollider.enabled = true;
        }
    }

}