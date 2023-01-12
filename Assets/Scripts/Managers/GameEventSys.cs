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

	public event Action<int> onItemTriggerEnter;
	public void ItemTriggerEnter(int id)
	{
		if (onItemTriggerEnter != null)
			onItemTriggerEnter(id);
	}

	public event Action<int> onItemTriggerExit;
	public void ItemTriggerExit(int id)
	{
		if (onItemTriggerExit != null)
			onItemTriggerExit(id);
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
}
