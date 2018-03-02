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

    public int numCheck;

	// Use this for initialization
	void Start ()
    {
        playerList = new List<GameObject>();
        countdownText.enabled = false;
        startingScreen.enabled = true;
        Time.timeScale = 1;

        playerNames = Input.GetJoystickNames();
    }
	
	// Update is called once per frame
	void Update ()
    {

        StartGameCheck();
        if (!gameStarted)
        {
            SetUpPlayers();
            StartGame();
        }
        UIUpdate();
        CheckDead();
    }

    void UIUpdate()
    {

    }

    void SetUpPlayers()
    {
        
        countdownText.text = gameLoadTime + "...";
        numCheck = 0;
        foreach (string number in playerNames)
        {
            if (playerReady[numCheck] == false)
            {
                if (Input.GetButtonDown("StartController" + (numCheck + 1)))
                {
                    Debug.Log("Controller" + (numCheck + 1));
                    playerReady[numCheck] = true;
                    joinScreens[numCheck].text = "Player " + (numCheck + 1) + " has joined the game";
                }
            }
            else if(playerReady[numCheck] == true)
            {
                numCheck++;
            }
        }
            Debug.Log(numCheck);

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
        foreach(GameObject player in playerList)
        {
            if (player.GetComponent<PlayerController>().Health <= 0)
            {
                playerList.RemoveAt(i);
            }
        }

        if(playerList.Count == 1)
        {
#if !UNITY_EDITOR
            GameOver(); //Fix when ready to test
#endif
        }
    }

    void GameOver()
    {
        EndGameCanvas.enabled = true;
        Time.timeScale = 0;
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
