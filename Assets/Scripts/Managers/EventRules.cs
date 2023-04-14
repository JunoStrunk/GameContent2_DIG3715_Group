using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRules : MonoBehaviour
{
	/* Notes =============================================
     * This script is to hold all the different rules for
     * when certain events should happen.
     * i.e. Player is holding banana and knife and interacted
     * with the cutting board. Bananna is cut. etc.
     * 
     ====================================================*/

	//Public variables
	public int LosingBound = 3;

	//Private variables
	InventoryManager _inventory;
	GameObject enemiesParent;
	Transform _playerPosition;
	int foundEvidence = 0;

	public GameObject _computerScreen;

	Dictionary<string, ItemController> itemsInWorld = new Dictionary<string, ItemController>();
	Dictionary<string, GameObject> notItems = new Dictionary<string, GameObject>();

	void Start()
	{
		_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
		if (_computerScreen != null)
			_computerScreen.SetActive(false);
		GameObject itemsParent = GameObject.Find("Items");
		for (int itemsIter = 0; itemsIter < itemsParent.transform.childCount; itemsIter++)
		{
			// Debug.Log(itemsIter);
			ItemController itemChild = itemsParent.transform.GetChild(itemsIter).GetComponent<ItemController>();
			// Debug.Log("Added " + itemChild.GetID());
			if (itemChild != null)
			{
				itemsInWorld.Add(itemChild.GetID(), itemChild);
				if (itemChild.hideOnStart)
					itemChild.gameObject.SetActive(false);
			}
			else
			{
				notItems.Add(itemsParent.transform.GetChild(itemsIter).gameObject.name, itemsParent.transform.GetChild(itemsIter).gameObject);
			}
		}

		enemiesParent = GameObject.Find("Enemies");
		if (enemiesParent != null)
			enemiesParent.SetActive(false);

		_inventory = this.GetComponent<InventoryManager>();
		foundEvidence = 0;

		GameEventSys.current.onItemInteract += OnItemInteract;
		GameEventSys.current.onFoundEvidence += FoundEvidence;
		GameEventSys.current.onTimerEnded += EndPhaseOne;
	}

	private void OnDisable()
	{
		GameEventSys.current.onItemInteract -= OnItemInteract;
		GameEventSys.current.onFoundEvidence -= FoundEvidence;
		GameEventSys.current.onTimerEnded -= EndPhaseOne;
	}

	private void OnDestroy()
	{
		GameEventSys.current.onItemInteract -= OnItemInteract;
		GameEventSys.current.onFoundEvidence -= FoundEvidence;
		GameEventSys.current.onTimerEnded -= EndPhaseOne;
	}

	private void FreezePlayer()
	{
		_playerPosition.GetComponent<PlayerMovement>().disabled = true;
	}

	private void UnFreezePlayer()
	{
		_playerPosition.GetComponent<PlayerMovement>().disabled = false;
	}

	private void EndPhaseOne()
	{
		//Set enemies active
		// Debug.Log("End of Phase One");
		if (enemiesParent != null)
		{
			enemiesParent.SetActive(true);
		}
	}

	private void FoundEvidence()
	{
		foundEvidence++;

		//check if number of evidence correct to stop the game
		if (foundEvidence == LosingBound)
		{
			GameEventSys.current.GameLost();
		}
	}

	private void OnItemInteract(string id)
	{
		switch (id)
		{
			case "EventItem":
				TestItem();
				break;
			case "MoneyBags":
				CutMBHalf();
				break;
			case "LockedDoor":
				LockedDoor();
				break;
			case "Laptop":
				Laptop();
				break;
			case "PaperShredder":
				PaperShredder();
				break;
			case "Mat&Sheets":
				CleanSheets();
				break;
			case "Painting_Genevieve":
				FindKey();
				break;
			case "Painting_Paul":
			case "Painting_Mother":
			case "Painting_Patrick":
			case "Painting_Annalise":
				PaintingDialogue(id);
				break;
			default:
				break;
		}

	}

	private void TestItem()
	{
		if (_inventory.GetActiveItem() != null)
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
	}

	private void CutMBHalf()
	{
		if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "Scissors")
		{
			itemsInWorld["MB_Head"].gameObject.SetActive(true);
			itemsInWorld["MB_Legs"].gameObject.SetActive(true);
			itemsInWorld["MoneyBags"].gameObject.SetActive(false);
			Ray groundCheckRay = new Ray(_playerPosition.position, Vector3.down);
			RaycastHit groundHitInfo;

			if (Physics.Raycast(groundCheckRay, out groundHitInfo, Mathf.Infinity, 1 << 3)) //1 << 3 ground layermask
			{
				itemsInWorld["MB_Head"].transform.position = new Vector3(_playerPosition.position.x, groundHitInfo.point.y + .5f, _playerPosition.position.z);
				itemsInWorld["MB_Legs"].transform.position = new Vector3(_playerPosition.position.x + 1f, groundHitInfo.point.y + .5f, _playerPosition.position.z);
			}
		}
		else
		{
			itemsInWorld["MoneyBags"].PickUpItemControl();
		}
	}

	private void LockedDoor()
	{
		//if player has the key, then unlock door.
		if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "Key")
		{
			// Debug.Log("OpenDoor");
			// itemsInWorld["LockedDoor"].gameObject.SetActive(false); //Destroy Locked Door
			itemsInWorld["LockedDoor"].transform.GetChild(0).gameObject.SetActive(false);//Destroy Mesh
			itemsInWorld["LockedDoor"].GetComponent<BoxCollider>().enabled = false; //Disable trigger
			_inventory.DropActiveItem(true);
			itemsInWorld["LockedDoor"].DeInteract();
			StartCoroutine(DelayLawSpawn());
		}
		else
		{
			itemsInWorld["LockedDoor"].GetComponent<DialogueTriggerNotItem>().ShowDialogue(itemsInWorld["LockedDoor"].GetID());
		}
	}

	private void Laptop()
	{
		DialogueTriggerNotItem laptopDialogue = itemsInWorld["Laptop"].GetComponent<DialogueTriggerNotItem>();
		if (!laptopDialogue.hasBeenUsed)
			laptopDialogue.ShowDialogue(itemsInWorld["Laptop"].GetID());
		else
		{
			FreezePlayer();
			_computerScreen.SetActive(true);
		}
	}

	public void LaptopClose()
	{
		UnFreezePlayer();
		_computerScreen.SetActive(false);
	}

	private void PaperShredder()
	{
		if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "Scissors")
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
		else if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "MoneyBags")
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
		else if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "MB_Head")
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
		else if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "MB_Legs")
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
		else
			itemsInWorld["PaperShredder"].GetComponent<DialogueTriggerNotItem>().ShowDialogue(itemsInWorld["PaperShredder"].GetID());

	}

	private void CleanSheets()
	{
		if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "WhiteOut")
		{
			notItems["Matress"].GetComponent<ChangeSheets>().CleanSheets();
			notItems["Sheets"].GetComponent<ChangeSheets>().CleanSheets();
			itemsInWorld["Mat&Sheets"].gameObject.SetActive(false);
		}
		else
		{
			itemsInWorld["Mat&Sheets"].GetComponent<DialogueTriggerNotItem>().ShowDialogue(itemsInWorld["Mat&Sheets"].GetID());

		}
	}

	private void FindKey()
	{
		if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "PaintingNote")
		{
			_inventory.AddItem(itemsInWorld["Key"].gameObject);
			_inventory.DropActiveItem(true); //true means the item is destroyed when used.
		}
		else
		{
			itemsInWorld["Painting_Genevieve"].GetComponent<DialogueTriggerNotItem>().ShowDialogue(itemsInWorld["Painting_Genevieve"].GetID());
		}

	}

	private void PaintingDialogue(string id)
	{
		itemsInWorld[id].GetComponent<DialogueTriggerNotItem>().ShowDialogue(itemsInWorld[id].GetID());
	}

	IEnumerator DelayLawSpawn()
	{
		yield return new WaitForSeconds(5f);
		GameEventSys.current.TimerEnded();
	}

}
