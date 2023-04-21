using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndingControls : MonoBehaviour
{
	public Image EndingA;
	public Image EndingB;
	public Image EndingC;

	// Start is called before the first frame update
	void Start()
	{
		EndingA.gameObject.SetActive(false);
		EndingB.gameObject.SetActive(false);
		EndingC.gameObject.SetActive(false);
	}

	void SetEnding()
	{
		switch (PlayerPrefs.GetInt("EndingVariable"))
		{
			case 0:
				EndingA.gameObject.SetActive(true);
				break;
			case 1:
				EndingB.gameObject.SetActive(true);
				break;
			case 2:
				EndingC.gameObject.SetActive(true);
				break;
			default:
				break;
		}
	}
}
