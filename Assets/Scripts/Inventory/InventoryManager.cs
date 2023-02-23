using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int InventoryLimit = 5;
    public List<InventoryItem> InventoryList = new List<InventoryItem>();
    
    [SerializeField]
    GameObject InventorySlot;

    //Private variables
    // List<Image> inventorySlots = new List<Image>();
    List<Image> inventorySlots = new List<Image>();
    HorizontalLayoutGroup inventoryLayout;
    InventoryItem activeItem = null;
    int activeItemIndex = 0;

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
        inventoryLayout = GameObject.Find("Slots").GetComponent<HorizontalLayoutGroup>();
        
        for(int invSetupIter = 0; invSetupIter < InventoryLimit; invSetupIter++)
        {
            GameObject newSlot = Instantiate(InventorySlot, transform.position, transform.rotation);
            newSlot.transform.SetParent(inventoryLayout.transform);
            Image newItemSlot = newSlot.transform.GetChild(0).GetComponent<Image>(); //Child will always be index 0, only one child.
            inventorySlots.Add(newItemSlot);
        }
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
        // Debug.Log(activeItemIndex);
        activeItemIndex++;
        SetActiveItem(activeItemIndex);
    }

    public void DropActiveItem()
    {
        RemoveItem(activeItem);
        activeItem = null;
        CycleActiveItem();
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        for(int invIter = 0; invIter < InventoryLimit; invIter++)
        {
            //go through slots
            if(inventorySlots[invIter].sprite != null) //if there is an item in it,
            {
                if(invIter < InventoryList.Count)//check if the slots index is within InventoryLists range
                {
                    //if it is set it to the correct sprite and color
                    inventorySlots[invIter].sprite = InventoryList[invIter].itemSprite;
                    inventorySlots[invIter].color = InventoryList[invIter].color;
                }
                else
                {
                    //if it is not clear it
                    inventorySlots[invIter].sprite = null;
                    inventorySlots[invIter].color = Color.clear;
                }
            }
        }
    }

    public void AddItem(string id, Sprite sprite, Color color)
    {
        if(!(InventoryList.Count >= InventoryLimit))
        {
            //Create and add item to inventory
            InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
            newItem.SetValues(id, sprite, color);
            InventoryList.Add(newItem);
            // Debug.Log("Added item: " + newItem.itemName + ", Inventory Count: " + InventoryList.Count);

            //Cycle through current inventory to find next slot
            for(int invSlotCheck = 0; invSlotCheck < InventoryLimit; invSlotCheck++)
            {
                //show sprite in inventory
                if(inventorySlots[invSlotCheck].sprite == null)
                {
                    inventorySlots[invSlotCheck].sprite = sprite;
                    inventorySlots[invSlotCheck].color = color;
                    break;
                }
            }

            if(InventoryList.Count == 1) //if this is the first item
            {
                SetActiveItem(0);
            }
        }
    }

    public bool RemoveItem(InventoryItem itemToRemove)
    {
        return InventoryList.Remove(itemToRemove);
    }
}
