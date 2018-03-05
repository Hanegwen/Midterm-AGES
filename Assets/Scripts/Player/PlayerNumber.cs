using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour, IPlayerNumber
{
    PlayerMovement pm;
    PlayerHealth ph;
    PlayerShooting ps;
    [SerializeField]
    int playerNumber;
    public int PlayerNumberSet
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
    // Use this for initialization
    void Start ()
    {
        pm = GetComponent<PlayerMovement>();
        ph = GetComponent<PlayerHealth>();
        ps = GetComponent<PlayerShooting>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        pm.PlayerNumber = playerNumber;
        ph.PlayerNumber = playerNumber;
        ps.PlayerNumber = playerNumber;
	}
}
