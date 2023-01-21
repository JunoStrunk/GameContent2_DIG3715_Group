using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> InventoryList = new List<InventoryItem>();

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
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
