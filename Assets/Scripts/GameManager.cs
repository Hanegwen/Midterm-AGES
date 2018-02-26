using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Canvas EndGameCanvas;

    [SerializeField]
    Canvas startingScreen;

    [SerializeField]
    Text countdownText;

    [SerializeField]
    string[] playerNames;

    [SerializeField]
    List<GameObject> playerList;
    
    public bool[] playerReady = new bool[4]; //Max Player Count

    [SerializeField]
    Text[] joinScreens; //Same number as Max Player Count

    [SerializeField]
    Transform[] PlayerSpawnLocations; //Same number as Max Player Count

    [SerializeField]
    bool startGame = false;

    [SerializeField]
    bool gameStarted = false;

    float gameLoadTime = 5;

    [SerializeField]
    GameObject playerPrefab;

    bool endGame = false;

	// Use this for initialization
	void Start ()
    {
        playerList = new List<GameObject>();
        countdownText.enabled = false;
        startingScreen.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerNames = Input.GetJoystickNames();

        StartGameCheck();

        SetUpPlayers();

        if (!gameStarted)
        {
            StartGame();
        }

        CheckDead();
    }

    void SetUpPlayers()
    {
        countdownText.text = gameLoadTime + "...";
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
                    StartCoroutine(CountDownAndBeginGame()); //Calls countdown to change countdown text
                }
            }
            i++;
        }
    }

    void StartGame()
    {
        if (startGame)
        {
            startingScreen.enabled = false;
            gameStarted = true;

            int i = 0;
            foreach(bool ready in playerReady)
            {
                if(playerReady[i])
                {
                    Instantiate(playerPrefab, PlayerSpawnLocations[i]);
                    playerList.Add(playerPrefab);
                    playerList[i].GetComponent<PlayerController>().PlayerNumber = i + 1;
                    //Instantiate Players
                }
                i++;
            }
            //Start Game Here
        }

    }

    void CheckDead()
    {
        int i = 0;
        foreach(float health in playerList)
        {
            if (health <= 0)
            {
                playerList.RemoveAt(i);
            }
        }

        if(playerList.Count == 1)
        {

        }
    }

    void GameOver()
    {
        EndGameCanvas.enabled = true;
    }


    IEnumerator CountDownAndBeginGame()
    {
        countdownText.enabled = true;

        for(int i = 0; i < gameLoadTime; i+=0)
        {
            yield return new WaitForSeconds(1);
            gameLoadTime--;
        }

        startGame = true;
        yield return null; //Calls BeginGame to Start The Game
    }
}
