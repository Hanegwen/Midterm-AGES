using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWeapon : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    LayerMask playerLayer;

    protected float currentCharge;
    protected float maxY = 3;
    protected float minY = 1;

    protected bool hitMax = false;

    public float CurrentCharge
    {
        set
        {
            currentCharge = value;
        }
    }
    private void Update()
    {
        if (transform.parent != null)
            transform.parent = null;
        MovementArch();
        IUpdate();
    }

    protected virtual void IUpdate()
    {

    }

    void MovementArch()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * currentCharge * Time.deltaTime);

        if (!hitMax)
        {
            if (this.gameObject.transform.position.y < maxY)
            {
                GetComponent<Rigidbody>().AddRelativeForce(transform.up * currentCharge * Time.deltaTime);
                
            }
        }

        if (this.gameObject.transform.position.y > maxY)
        {
            hitMax = true;
            GetComponent<Rigidbody>().AddRelativeForce(-transform.up * currentCharge * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}