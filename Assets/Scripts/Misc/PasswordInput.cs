using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordInput : MonoBehaviour
{
	TMP_InputField passwordInput;
	public GameObject ComputerScreen;
	public GameObject Laptop;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		passwordInput = this.GetComponent<TMP_InputField>();
	}

	public void checkInput()
	{
		if (passwordInput.text == "Dorothy")
		{
			ComputerScreen.SetActive(false);
			Laptop.GetComponent<DialogueTriggerNotItem>().UnFreezePlayer();
			Laptop.SetActive(false);
			GameEventSys.current.PuzzleSolved();
			GameEventSys.current.ItemTriggerExit("Laptop");
		}
		else
		{
			passwordInput.text = "Incorrect";
		}
	}
}
