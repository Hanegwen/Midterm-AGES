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
    Text winnerText;

    [SerializeField]
    int winner;

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

    [SerializeField]
    AudioClip playerJoinedAudio;

    [SerializeField]
    AudioSource Noises;

    int numReady = 0;

    [SerializeField]
    Text controllerCheck;

	// Use this for initialization
	void Start ()
    {
        playerList = new List<GameObject>();
        countdownText.enabled = false;
        startingScreen.enabled = true;
        Time.timeScale = 1;

        playerNames = Input.GetJoystickNames();
        controllerCheck.text = "Controllers Connected: " + playerNames.Length;
        EndGameCanvas.GetComponentInChildren<SelectOnInput>().enabled = false;
    }
	// Update is called once per frame
	void Update ()
    {
        
        if (numReady > 1)
        {
            StartGameCheck();
        }
        if (!gameStarted)
        {
            SetUpPlayers();
            StartGame();
        }
        if (gameStarted)
        {
            CheckDead();
        }
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
                    numReady++;
                    Noises.clip = playerJoinedAudio;
                    Noises.Play();
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
                    GameObject player = Instantiate(playerPrefab, PlayerSpawnLocations[i]);
                    playerList.Add(player);
                    playerList[i].GetComponent<IPlayerNumber>().PlayerNumberSet = i + 1;
                    //Instantiate Players
                }
                i++;
            }
            //Start Game Here
        }
    }
    void CheckDead()
    {
        Debug.Log("Player Alive: " + playerList.Count);
        foreach(GameObject player in playerList)
        {
            Debug.Log("For Each Loop Called");
            Debug.Log(player.GetComponent<PlayerHealth>().Health);
            if (player.GetComponent<PlayerHealth>().Health <= 0)
            {
                Debug.Log("Player DEAD");
                if (player.GetComponent<IPlayerNumber>().PlayerNumberSet >= 0)
                {
                    //playerList.RemoveAt(i);
                    player.GetComponent<PlayerHealth>().Kill = true;
                }
            }
        }

        for(int i = 0; i < playerList.Count; i++)
        {
            if(playerList[i].GetComponent<PlayerHealth>().Kill)
            {
                playerList.RemoveAt(i);
            }
        }
        if(playerList.Count == 1)
        {
            winner = playerList[0].GetComponent<IPlayerNumber>().PlayerNumberSet;
            GameOver(); //Fix when ready to test
        }
    }

    void GameOver()
    {
        winnerText.text = "Player " + winner + " won!";
        EndGameCanvas.enabled = true;
        EndGameCanvas.GetComponentInChildren<SelectOnInput>().enabled = true;
        Time.timeScale = 0;
    }


    IEnumerator CountDownAndBeginGame()
    {
        bool cancel = false;
        countdownText.enabled = true;
        for(int i = 0; i < gameLoadTime; i+=0)
        {
            yield return new WaitForSeconds(1);
            gameLoadTime--;
            if (Input.GetButton("CancelController"))
            {
                cancel = true;
                
            }
        }
        if (cancel)
        {
            countdownText.enabled = false;
            gameLoadTime = 5;
            foreach (string number in playerNames)
            {
                playerReady[numCheck] = false;
                numCheck++;
            }
            cancel = false;
        }
        else
        {
            startGame = true;
        }
        yield return null; //Calls BeginGame to Start The Game
    }
}
