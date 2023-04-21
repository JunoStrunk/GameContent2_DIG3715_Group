using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	/* Notes ============================
     * This script should be placed on any interactables.
     * It controls if the item is selected, or interacted with.
     * This script tells when to add the item to the inventory
     * and what the item's sprite and name is.
     * 
     * Possible issue with two items being too close to each other
     ================================== */
	// Public

	public SpriteRenderer highlight;
	public bool canPickUp = true;
	public bool isHidingSpot = false;
	public bool hideOnStart = false;

	// Private
	string id;
	SpriteRenderer sr;
	bool selected = false;
	bool inHidingSpot = false;

	private void Awake() //Important to listen only on start or else there will be a null reference for singleton
	{
		id = this.name;
		sr = this.GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		GameEventSys.current.onItemTriggerEnter += OnHighlightItem;
		GameEventSys.current.onItemTriggerExit += OnUnHighlightItem;

	}

	private void OnEnable()
	{
		if (GameEventSys.current != null)
		{
			GameEventSys.current.onItemTriggerEnter += OnHighlightItem;
			GameEventSys.current.onItemTriggerExit += OnUnHighlightItem;
		}
		else
			StartCoroutine(DelayedEnable());
	}

	private void Update()
	{
		if (selected && Input.GetKeyDown(KeyCode.E))
		{
			if (canPickUp)
			{
				PickUpItemControl();
			}
			else
			{
				if (isHidingSpot)
				{
					if (inHidingSpot)
					{
						GameEventSys.current.PlayerHides(false);
						inHidingSpot = false;
					}
					else
					{
						GameEventSys.current.PlayerHides(true);
						inHidingSpot = true;
					}
				}
				else
					GameEventSys.current.ItemInteract(id);
			}
		}
	}
	public void PickUpItemControl()
	{
		GameEventSys.current.ItemPickUp(this.gameObject, id, sr.sprite, sr.color);
		GameEventSys.current.ItemTriggerExit(id);
		// this.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		GameEventSys.current.onItemTriggerEnter -= OnHighlightItem;
		GameEventSys.current.onItemTriggerExit -= OnUnHighlightItem;
	}

	private void OnDestroy()
	{
		GameEventSys.current.onItemTriggerEnter -= OnHighlightItem;
		GameEventSys.current.onItemTriggerExit -= OnUnHighlightItem;
	}

	public string GetID()
	{
		return id;
	}

	public Sprite GetSprite()
	{
		return sr.sprite;
	}

	public Color GetColor()
	{
		return sr.color;
	}

	private void OnHighlightItem(string id)
	{
		if (id == this.id)
		{
			// if (highlight != null)
			// 	highlight.color = Color.white;
			selected = true;
		}
	}

	private void OnUnHighlightItem(string id)
	{
		if (id == this.id && !inHidingSpot)
		{
			if (highlight != null)
				highlight.color = sr.color;
			selected = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// Debug.Log("trigger entered");
		if (other.gameObject.CompareTag("Player"))
			GameEventSys.current.ItemTriggerEnter(id);
	}

	private void OnTriggerExit(Collider other)
	{
		// Debug.Log("trigger exited");
		if (other.gameObject.CompareTag("Player"))
			GameEventSys.current.ItemTriggerExit(id);
	}

	public void DeInteract()
	{
		//EndDialogue
		GameEventSys.current.ItemTriggerExit(id);

	}

	IEnumerator DelayedEnable()
	{
		yield return new WaitForSeconds(0.2f);
		GameEventSys.current.onItemTriggerEnter += OnHighlightItem;
		GameEventSys.current.onItemTriggerExit += OnUnHighlightItem;
		StopCoroutine(DelayedEnable());
	}
}
