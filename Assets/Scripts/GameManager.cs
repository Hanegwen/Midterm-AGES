using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Canvas startingScreen;

    [SerializeField]
    string[] playerNames;
    
    
    public bool[] playerReady = new bool[4]; //Max Player Count

    [SerializeField]
    Text[] joinScreens; //Same number as Max Player Count

    bool startGame = false;

    float gameLoadTime = 5;


	// Use this for initialization
	void Start ()
    {
        startingScreen.enabled = true;
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
                int j = i - 1;
                playerReady[i - 1] = true;
                joinScreens[i - 1].text = "Player " + i + " has joined the game";
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

                    StartCoroutine(CountDown()); //Calls countdown to change countdown text
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

    IEnumerator CountDown()
    {
        yield return null; //Calls BeginGame to Start The Game
    }
}
