using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    AudioClip hitClip;

    [SerializeField]
    AudioSource hitSource;

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
    Image fillImage;

    [SerializeField]
    float health;

    [SerializeField]
    float startingHealth;

    public float Health
    {
        get
        {
            return health;
        }
    }

    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    Color fullHealthColor = Color.green;

    [SerializeField]
    Color zeroHealthColor = Color.red;

    [SerializeField]
    ParticleSystem hitExplosion;

    bool kill = false;

    public bool Kill
    {
        get
        {
            return kill;
        }

        set
        {
            kill = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        startingHealth = health;
    }

    // Update is called once per frame
    void Update ()
    {
        SetHealthUI();

        if(kill)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage Took: " + damage);
        health -= damage;

        ParticleSystem explosion = Instantiate(hitExplosion, this.gameObject.transform);
        hitSource.clip = hitClip;
        hitSource.Play();
        
    }

    void SetHealthUI()
    {
        healthSlider.value = health;

        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / startingHealth);
    }



}
