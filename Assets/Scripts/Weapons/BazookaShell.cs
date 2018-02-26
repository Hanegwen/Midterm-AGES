using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaShell : ShotWeapon
{
    float lifeTime = 5;

    // Use this for initialization
    void Start ()
    {
        Destroy(this.gameObject, lifeTime);
	}

    protected override void IUpdate()
    {

    }

}
