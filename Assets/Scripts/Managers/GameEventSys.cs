using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSys : MonoBehaviour
{
	//Create singleton for event system :)
	public static GameEventSys current;

	private void Awake()
	{
		current = this;
	}
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

	public event Action<string, Sprite, Color> onItemPickUp;
	public void ItemPickUp(string id, Sprite sprite, Color color)
    {
		if (onItemPickUp != null)
			onItemPickUp(id, sprite, color);
    }

	public event Action<string> onItemInteract;
	public void ItemInteract(string id)
    {
		if (onItemInteract != null)
			onItemInteract(id);
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
