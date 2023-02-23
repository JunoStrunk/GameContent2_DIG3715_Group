using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    InventoryManager inventory;
    InventoryItem heldItem;

    SpriteRenderer rend;

    private void Start()
    {
        inventory = GameObject.Find("Managers").GetComponent<InventoryManager>();
        rend = this.GetComponent<SpriteRenderer>();
    }

    private void SetActiveItem()
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetActiveItem();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.CycleActiveItem();
            SetActiveItem();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            inventory.DropActiveItem();
            SetActiveItem();
        }
    }
}
