using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaShell : ShotWeapon
{
    float lifeTime = 5;
    MeshCollider meshCollider;

    // Use this for initialization
    void Start ()
    {
        meshCollider = GetComponent<MeshCollider>();
        Destroy(this.gameObject, lifeTime);
	}

    protected override void IUpdate()
    {
        wait += Time.deltaTime;
        if (wait * Time.deltaTime > .8f)
        {
            meshCollider.enabled = true;
        }
    }

}
