using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSceneManager : MonoBehaviour
{
    //public static SwapScenes Instance;
    void Start()
    {
        
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
