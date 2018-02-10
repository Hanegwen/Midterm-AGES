using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    string[] playerNames;
    
    
    public bool[] playerReady = new bool[4];

    bool startGame = false;

    float gameLoadTime = 5;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerNames = Input.GetJoystickNames();
        
        SetUpPlayers();

        StartGameCheck();

	}

    void SetUpPlayers()
    {
        int i = 1;
        foreach(string number in playerNames)
        {
            if(Input.GetButtonDown("StartController" + i))
            {
                playerReady[i - 1] = true;
                
            }
            i++;
        }
    }

    void StartGameCheck()
    {
        int i = 1;
        foreach(bool ready in playerReady)
        {
            if(Input.GetButtonDown("SelectController" + i))
            {
                if(playerReady[i-1])
                {
                    if(startGame)
                    {
                        //Start Game Here
                    }

                    StartCoroutine(BeginGame());
                }
            }
            i++;
        }
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(gameLoadTime);

        startGame = true;
    }
}
