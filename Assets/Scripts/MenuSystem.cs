using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Level()
    {
        SceneManager.LoadScene("Level");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
