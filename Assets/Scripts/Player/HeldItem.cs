using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
	InventoryManager inventory;
	InventoryItem heldItem;
	DialogueManager dialoguemanager;

	SpriteRenderer rend;
	GameObject interactButton;

	private void Start()
	{
		inventory = GameObject.Find("Managers").GetComponent<InventoryManager>();
		dialoguemanager = GameObject.Find("Managers").GetComponent<DialogueManager>();

		rend = this.GetComponent<SpriteRenderer>();
		interactButton = transform.GetChild(0).gameObject;

		GameEventSys.current.onItemTriggerEnter += ShowInteractButton;
		GameEventSys.current.onItemTriggerExit += HideInteractButton;
		GameEventSys.current.onPlayerHides += HideHeldItem;
	}

	void OnDisable()
	{
		GameEventSys.current.onItemTriggerEnter -= ShowInteractButton;
		GameEventSys.current.onItemTriggerExit -= HideInteractButton;
	}

	void OnDestroy()
	{
		GameEventSys.current.onItemTriggerEnter -= ShowInteractButton;
		GameEventSys.current.onItemTriggerExit -= HideInteractButton;
	}

	private void ShowInteractButton(string id)
	{
		interactButton.SetActive(true);
	}

	private void HideInteractButton(string id)
	{
		interactButton.SetActive(false);
	}

	void HideHeldItem(bool isHidden)
	{
		rend.enabled = !isHidden;
	}

	public void SetActiveItem()
	{
		if (inventory.GetActiveItem() != null)
		{
			heldItem = inventory.GetActiveItem();
			rend.sprite = heldItem.itemSprite;
			rend.color = heldItem.color;
		}
		else
		{
			heldItem = null;
			rend.sprite = null;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			inventory.CycleActiveItem();
			SetActiveItem();
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			inventory.InspectActiveItem();
		}
		if (inventory.GetActiveItem() != null && Input.GetKeyDown(KeyCode.E) && dialoguemanager.isInDialouge)
		{
			inventory.GetActiveItem().dialogue.AdvanceDialogue();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			inventory.DropActiveItem(false); //false means that the item is not destroyed upon dropping
			SetActiveItem();
		}
	}
}
