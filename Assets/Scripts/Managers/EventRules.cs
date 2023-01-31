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

    InventoryManager inventory;

    void Start()
    {
        inventory = GameObject.Find("Managers").GetComponent<InventoryManager>();
        GameEventSys.current.onItemInteract += OnItemInteract;
    }

    private void OnDisable()
    {
        GameEventSys.current.onItemInteract -= OnItemInteract;
    }

    private void OnDestroy()
    {
        GameEventSys.current.onItemInteract -= OnItemInteract;
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
        if (inventory.GetActiveItem().itemName == "Item")
            Debug.Log("Event Occurs!");
    }
}
