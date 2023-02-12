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
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.CycleActiveItem();
            SetActiveItem();
        }
    }
}
