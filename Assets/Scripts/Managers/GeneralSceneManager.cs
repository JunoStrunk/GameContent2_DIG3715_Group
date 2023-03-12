using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSceneManager : MonoBehaviour
{
    //public static SwapScenes Instance;
    public Animator animator;
    private string levelToLoad;
    public GameObject blackscreen;
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

    public void LoadName(string sceneName)
    {
        Time.timeScale = 1f;
        levelToLoad = sceneName;
        blackscreen.SetActive(true);
        animator.SetTrigger("FadeOut");
    }
    public void FadeChangeScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void FadeQuit()
    {
        Application.Quit();
        print("quit");
    }

    public void QuitGame()
    {
        blackscreen.SetActive(true);
        animator.SetTrigger("FadeQuit");
    }
}
