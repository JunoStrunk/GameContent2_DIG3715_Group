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

	private int evidenceFound = 0;

	void Start()
	{
		if (GameEventSys.current != null)
		{
			GameEventSys.current.onGameWon += GameWon;
			GameEventSys.current.onGameLost += GameLost;
			GameEventSys.current.onFoundEvidence += IncreaseEvidence;
		}
	}

	void OnDisable()
	{
		if (GameEventSys.current != null)
		{
			GameEventSys.current.onGameWon -= GameWon;
			GameEventSys.current.onGameLost -= GameLost;
			GameEventSys.current.onFoundEvidence -= IncreaseEvidence;

		}
	}

	void OnDestroy()
	{
		if (GameEventSys.current != null)
		{
			GameEventSys.current.onGameWon -= GameWon;
			GameEventSys.current.onGameLost -= GameLost;
			GameEventSys.current.onFoundEvidence -= IncreaseEvidence;

		}
	}

	public void GameWon(int EndingVar)
	{
		// //Whatever happens when the game is won
		// Debug.Log("GameWon");
		PlayerPrefs.SetInt("EndingVariable", EndingVar);
		LoadName("Ending");
		FadeChangeScene();
	}

	public void GameLost()
	{
		// //Whatever happens when the game is lost
		// Debug.Log("GameLost");

		PlayerPrefs.SetInt("EndingVariable", 2);
		LoadName("Ending");
		FadeChangeScene();
	}

	public void IncreaseEvidence()
	{
		evidenceFound++;
	}

	public void LoadName(string sceneName)
	{
		Time.timeScale = 1f;
		levelToLoad = sceneName;
		if (blackscreen != null)
			blackscreen.SetActive(true);

		if (animator != null)
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
