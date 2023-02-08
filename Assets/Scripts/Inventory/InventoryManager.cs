using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> InventoryList = new List<InventoryItem>();
    GameObject inventorySlots;

    InventoryItem activeItem = null;
    int activeItemIndex = 0;

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
        inventorySlots = GameObject.Find("Slots");
        
        GameEventSys.current.onItemPickUp += AddItem;
    }

    private void OnDisable()
    {
        GameEventSys.current.onItemPickUp -= AddItem;
    }

    private void OnDestroy()
    {
        GameEventSys.current.onItemPickUp -= AddItem;
    }

    private void SetActiveItem(int index)
    {
        //Debug.Log("Index: " + index);
        //Debug.Log("Count: " + InventoryList.Count);
        if (index >= InventoryList.Count)
            index = 0;
        if(InventoryList.Count > 0)
            activeItem = InventoryList[index];
        activeItemIndex = index;
    }

    public InventoryItem GetActiveItem()
    {
        return activeItem;
    }

    public void CycleActiveItem()
    {
        activeItemIndex++;
        SetActiveItem(activeItemIndex);
    }

    public void AddItem(string id, Sprite sprite)
    {
        InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
        newItem.SetValues(id, sprite);
        InventoryList.Add(newItem);
        Debug.Log("Added item: " + newItem.itemName + ", Inventory Count: " + InventoryList.Count);
    }

    public bool RemoveItem(InventoryItem itemToRemove)
    {
        return InventoryList.Remove(itemToRemove);
    }
}
