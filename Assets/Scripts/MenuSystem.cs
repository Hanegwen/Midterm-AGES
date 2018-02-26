using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{

    public void Level()
    {
        SceneManager.LoadScene("Level");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
