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
    Transform _playerPosition;
    HeldItem _heldItem;

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
        _heldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        inventoryLayout = GameObject.Find("Slots").GetComponent<HorizontalLayoutGroup>();
        
        for(int invSetupIter = 0; invSetupIter < InventoryLimit; invSetupIter++)
        {
            GameObject newSlot = Instantiate(InventorySlot, transform.position, transform.rotation);
            newSlot.transform.SetParent(inventoryLayout.transform, false);
            Image newItemSlot = newSlot.transform.GetChild(0).GetComponent<Image>(); //Child will always be index 0, second child will be highlight.
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

        if(activeItem != null)
		    inventorySlots[activeItemIndex].color = Color.white; //highlihgt

		_heldItem.SetActiveItem();
    }

    public InventoryItem GetActiveItem()
    {
        return activeItem;
    }

    public void CycleActiveItem()
    {
        // Debug.Log(activeItemIndex);
        if(activeItem != null)
            inventorySlots[activeItemIndex].color =  activeItem.color;
        
        activeItemIndex++;
        SetActiveItem(activeItemIndex);
    }

    public void DropActiveItem(bool destroys)
    {
        RemoveItem(activeItem);
        
        if(!destroys)
        {
            //Drop the item on the ground again
            // GameObject droppedItem = Instantiate(activeItem.item, _playerPosition.position, _playerPosition.rotation);
            activeItem.item.SetActive(true);
            

            //Make sure the player is on the ground
            Ray groundCheckRay = new Ray(_playerPosition.position, Vector3.down);
            RaycastHit groundHitInfo;

            if(Physics.Raycast(groundCheckRay, out groundHitInfo, Mathf.Infinity, 1 << 3)) //1 << 3 ground layermask
            {
                activeItem.item.transform.position = new Vector3(_playerPosition.position.x, groundHitInfo.point.y+.5f, _playerPosition.position.z); 
            }

            GameEventSys.current.ItemTriggerEnter(activeItem.itemName);
        }

        activeItem = null;
        CycleActiveItem();
        UpdateInventory();
    }

    public void InspectActiveItem()
    {
        if(activeItem != null)
            activeItem.ShowDialogue();
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

    public void AddItem(GameObject item, string id, Sprite sprite, Color color)
    {
        if(!(InventoryList.Count >= InventoryLimit))
        {
            //Create and add item to inventory
            InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
            newItem.SetValues(item, id, sprite, color);
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
                // Debug.Log("Called??");
                SetActiveItem(0);
            }
        }
    }

    public bool RemoveItem(InventoryItem itemToRemove)
    {
        return InventoryList.Remove(itemToRemove);
    }
}
