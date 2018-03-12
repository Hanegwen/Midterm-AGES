using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotWeapon : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    LayerMask playerLayer;

    protected float currentCharge;
    protected float maxY = 1.5f;
    protected float minY = 1;

    protected bool hitMax = false;

    protected float wait = 0;

    public float CurrentCharge
    {
        set
        {
            currentCharge = value;
        }
    }
    private void Update()
    {
        wait++;
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
            if(this.gameObject.transform.position.y < minY)
            {
                hitMax = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
            Debug.Log("Damage: " + damage);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (wait > 1)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                transform.position = Vector3.Lerp(this.gameObject.transform.position, other.transform.position, 10);
            }
        }
    }
}