using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public List<InventoryItem> InventoryList = new List<InventoryItem>();
=======
    //Public Variables
    public int InventoryLimit = 5;

    [SerializeField]
    GameObject InventorySlot;
>>>>>>> Stashed changes

    public List<InventoryItem> InventoryList = new List<InventoryItem>();
    
    //Private Variables
    List<Image> inventorySlots = new List<Image>();
    HorizontalLayoutGroup inventoryLayout;
    InventoryItem activeItem = null;
    int activeItemIndex = 0;

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
<<<<<<< Updated upstream
=======
        inventoryLayout = GameObject.Find("Slots").GetComponent<HorizontalLayoutGroup>();
        
        for(int invSetupIter = 0; invSetupIter < InventoryLimit; invSetupIter++)
        {
            GameObject newSlot = Instantiate(InventorySlot, transform.position, transform.rotation);
            newSlot.transform.SetParent(inventoryLayout.transform);
            Image newItemSlot = newSlot.transform.GetChild(0).GetComponent<Image>(); //Child will always be index 0, only one child.
            inventorySlots.Add(newItemSlot);
        }

>>>>>>> Stashed changes
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
        //Highlight??
        Debug.Log(activeItemIndex);
        activeItemIndex++;
        SetActiveItem(activeItemIndex);
    }

    public void AddItem(string id, Sprite sprite, Color color)
    {
        if(!(InventoryList.Count >= InventoryLimit))
        {
            //Create and add item to inventory
            InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
            newItem.SetValues(id, sprite, color);
            InventoryList.Add(newItem);
            Debug.Log("Added item: " + newItem.itemName + ", Inventory Count: " + InventoryList.Count);

            //Cycle through current inventory to find next slot
            for(int invSlotCheck = 0; invSlotCheck < inventorySlots.Count; invSlotCheck++)
            {
                //show sprite in inventory
                if(inventorySlots[invSlotCheck].sprite == null)
                {
                    inventorySlots[invSlotCheck].sprite = sprite;
                    inventorySlots[invSlotCheck].color = color;
                    break;
                }
            }
        }
    }

    public bool RemoveItem(InventoryItem itemToRemove)
    {
        return InventoryList.Remove(itemToRemove);
    }
}
