using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
	GeneralSceneManager sceneManager;
	// Start is called before the first frame update
	void Start()
	{
		sceneManager = GameObject.Find("Managers").GetComponent<GeneralSceneManager>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Debug.Log("Loading Scene");
			sceneManager.LoadName("Midterm_Final");
			sceneManager.FadeChangeScene();
		}
	}

}
