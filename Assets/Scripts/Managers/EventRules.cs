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
	int foundEvidence = 0;

    void Start()
    {
		enemiesParent = GameObject.Find("Enemies");
        if(enemiesParent != null)
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

    private void EndPhaseOne()
    {
        //Set enemies active
        // Debug.Log("End of Phase One");
        if(enemiesParent != null)
        {
			enemiesParent.SetActive(true);
		}
    }

    private void FoundEvidence()
    {
        foundEvidence++;

        //check if number of evidence correct to stop the game
        if(foundEvidence == LosingBound)
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
            default:
                break;
        }

    }

    private void TestItem()
    {
        if (_inventory.GetActiveItem() != null && _inventory.GetActiveItem().itemName == "Item")
            _inventory.DropActiveItem(true); //true means the item is destroyed when used.
    }
}
