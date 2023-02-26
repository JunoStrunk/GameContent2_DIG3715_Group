using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsUI;
    public static bool bannerExit = false;
    public static bool controlsExit = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {              
                StartCoroutine(resumeGame());
            } 
            else
            {
                Pause();
            }
        }
    }
    public void buttonResume ()
    {
        StartCoroutine(resumeGame());
    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);        
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void openControls()
    {
        StartCoroutine(removePauseMenu());
        controlsUI.SetActive(true);
    }
    public void closeControls()
    {
        pauseMenuUI.SetActive(true);
        StartCoroutine(removeControls());
    }
    IEnumerator resumeGame()
    {
        bannerExit = true; 
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        bannerExit = false;
    }
    IEnumerator removePauseMenu()
    {
        bannerExit = true;
        yield return new WaitForSecondsRealtime(.5f);        
        pauseMenuUI.SetActive(false);
        bannerExit = false;
    } 
    IEnumerator removeControls()
    {
        controlsExit = true;
        yield return new WaitForSecondsRealtime(.5f);        
        controlsUI.SetActive(false);
        controlsExit = false;
    } 

}
