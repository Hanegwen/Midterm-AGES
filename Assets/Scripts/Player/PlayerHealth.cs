using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
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


    // Use this for initialization
    void Start ()
    {
        startingHealth = health;
    }

    // Update is called once per frame
    void Update ()
    {
        SetHealthUI();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage Took: " + damage);
        health -= damage;

        ParticleSystem explosion = Instantiate(hitExplosion, this.gameObject.transform);
        
    }

    void SetHealthUI()
    {
        healthSlider.value = health;

        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / startingHealth);
    }



}
