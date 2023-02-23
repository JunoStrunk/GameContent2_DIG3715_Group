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
    InventoryManager inventory;
    int foundEvidence = 0;

    void Start()
    {
        inventory = GameObject.Find("Managers").GetComponent<InventoryManager>();
        foundEvidence = 0;

        GameEventSys.current.onItemInteract += OnItemInteract;
        GameEventSys.current.onFoundEvidence += FoundEvidence;
    }

    private void OnDisable()
    {
        GameEventSys.current.onItemInteract -= OnItemInteract;
        GameEventSys.current.onFoundEvidence -= FoundEvidence;
    }

    private void OnDestroy()
    {
        GameEventSys.current.onItemInteract -= OnItemInteract;
        GameEventSys.current.onFoundEvidence -= FoundEvidence;
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
        if (inventory.GetActiveItem() != null && inventory.GetActiveItem().itemName == "Item")
            Debug.Log("Event Occurs!");
    }
}
