using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSceneManager : MonoBehaviour
{
    //public static SwapScenes Instance;
    void Start()
    {
        GameEventSys.current.onGameWon += GameWon;
        GameEventSys.current.onGameLost += GameLost;
    }

    void OnDisable() 
    {
        GameEventSys.current.onGameWon -= GameWon;
        GameEventSys.current.onGameLost -= GameLost;
    }

    void OnDestroy()
    {
        GameEventSys.current.onGameWon -= GameWon;
        GameEventSys.current.onGameLost -= GameLost;
    }

    public void GameWon()
    {
        //Whatever happens when the game is won
        Debug.Log("GameWon");
    }

    public void GameLost()
    {
        //Whatever happens when the game is lost
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScenes(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadWhilePaused(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game... ");
        Application.Quit();
    }
}
