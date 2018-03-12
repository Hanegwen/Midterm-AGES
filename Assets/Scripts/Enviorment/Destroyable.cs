using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField]
    float health = 50;

    

    AudioSource audioSource;
    
    ParticleSystem ps;

    [SerializeField]
    bool dealtDamage = false;

    [SerializeField]
    bool exploding;

    [SerializeField]
    bool explode = false;

    [SerializeField]
    bool touchingPlayer = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ps = GetComponentInChildren<ParticleSystem>();

        if(ps != null)
        {
            ps.Stop();
        }
        
    }

    private void Update()
    {
        if (health < 25)
        {
            Spark();
        }
        if (health <= 0)
        {
            if (dealtDamage == false)
            {
                exploding = true;
            }

            StartCoroutine(Waiting());

            if (explode)
            {
                

                Destroy(this.gameObject);

            }
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Spark()
    {
        
        if(ps == null)
        {
            Debug.Log("Ignore");
        }

        else
        {
            Debug.Log("Sparking");
            
            ps.Play();
            if(ps.isPlaying)
            {
                Debug.Log("Should Be Working");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        touchingPlayer = true;
        if (exploding)
        {
            if(collision.gameObject.GetComponent<IDamagable>() != null)
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(8);
                dealtDamage = true;
                exploding = false;
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);


        explode = true;
        audioSource.Play();
        yield return new WaitForSeconds(.5f);

    }


}
