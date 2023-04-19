using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSys : MonoBehaviour
{
	//Create singleton for event system :)
	public static GameEventSys current;

	private void OnEnable()
	{
		current = this;
	}

	//========================== Player =======================
	public event Action<bool> onPlayerHides;
	public void PlayerHides(bool state)
	{
		if (onPlayerHides != null)
			onPlayerHides(state);
	}
	//=============================-----=======================

	//======================== Detective ======================
	public event Action onFoundEvidence;
	public void FoundEvidence()
	{
		if (onFoundEvidence != null)
			onFoundEvidence();
	}

	//=========================================================

	//======================= Game State ======================
	public event Action onTimerEnded;
	public void TimerEnded()
	{
		if (onTimerEnded != null)
			onTimerEnded();
	}

	public event Action onGameWon;
	public void GameWon()
	{
		if (onGameWon != null)
			onGameWon();
	}

	public event Action onGameLost;
	public void GameLost()
	{
		if (onGameLost != null)
			onGameLost();
	}

	public event Action onPuzzleSolved;
	public void PuzzleSolved()
	{
		if (onPuzzleSolved != null)
			PuzzleSolved();
	}

	//=========================================================

	//========================== Camera =======================
	public event Action onCameraPosChange;
	public void CameraPosChange()
	{
		if (onCameraPosChange != null)
			onCameraPosChange();
	}
	//=========================================================

	//========================== Items ========================
	public event Action<string> onItemTriggerEnter;
	public void ItemTriggerEnter(string id)
	{
		if (onItemTriggerEnter != null)
			onItemTriggerEnter(id);
	}

	public event Action<string> onItemTriggerExit;
	public void ItemTriggerExit(string id)
	{
		if (onItemTriggerExit != null)
			onItemTriggerExit(id);
	}

	public event Action onItemPickUpSound;
	public void ItemPickUpSound()
	{
		if (onItemPickUpSound != null)
			onItemPickUpSound();
	}

	public event Action onItemDropSound;
	public void ItemDropSound()
	{
		if (onItemDropSound != null)
			onItemDropSound();
	}

	public event Action<GameObject, string, Sprite, Color> onItemPickUp;
	public void ItemPickUp(GameObject item, string id, Sprite sprite, Color color)
	{
		if (onItemPickUp != null)
			onItemPickUp(item, id, sprite, color);
	}

	public event Action<string> onItemInteract;
	public void ItemInteract(string id)
	{
		if (onItemInteract != null)
			onItemInteract(id);
	}

	public event Action<string> onItemInspect;
	public void ItemInspect(string id)
	{
		if (onItemInspect != null)
			onItemInspect(id);
	}

	public event Action<string> onItemDrop;
	public void ItemDrop(string id)
	{
		if (onItemDrop != null)
			onItemDrop(id);
	}

	private Func<List<GameObject>> onRequestListOfItems;
	public void SetOnRequestListOfItems(Func<List<GameObject>> returnEvent)
	{
		onRequestListOfItems = returnEvent;
	}

	public List<GameObject> RequestListofItems()
	{
		if (onRequestListOfItems != null)
			return onRequestListOfItems();

		return null;
	}
	//=========================================================


}
