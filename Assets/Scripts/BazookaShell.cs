using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaShell : MonoBehaviour {

    float lifeTime = 5;

    [SerializeField]
    float damage;

    [SerializeField]
    LayerMask playerLayer;

	// Use this for initialization
	void Start ()
    {
        
        Destroy(this.gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.parent != null)
        transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
