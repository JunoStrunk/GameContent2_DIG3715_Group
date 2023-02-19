using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /* Notes ============================
     * This script should be placed on any interactables.
     * It controls if the item is selected, or interacted with.
     * This script tells when to add the item to the inventory
     * and what the item's sprite and name is.
     * 
     * Possible issue with two items being too close to each other
     ================================== */
    // Public
    
    public SpriteRenderer highlight;
    public bool canPickUp = true;

    // Private
    string id;
    SpriteRenderer sr;
    bool selected = false;

    private void Update()
    {
        if(selected && Input.GetKeyDown(KeyCode.E))
        {
            if (canPickUp)
            {
                GameEventSys.current.ItemPickUp(id, sr.sprite, sr.color);
                Destroy(this.gameObject);
            }
            else
            {
                GameEventSys.current.ItemInteract(id);
            }
        }
    }

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
        id = this.name;
        sr = this.GetComponent<SpriteRenderer>();

        GameEventSys.current.onItemTriggerEnter += OnHighlightItem;
        GameEventSys.current.onItemTriggerExit += OnUnHighlightItem;
    }

    private void OnDisable()
    {
        GameEventSys.current.onItemTriggerEnter -= OnHighlightItem;
        GameEventSys.current.onItemTriggerExit -= OnUnHighlightItem;
    }

    private void OnDestroy()
    {
        GameEventSys.current.onItemTriggerEnter -= OnHighlightItem;
        GameEventSys.current.onItemTriggerExit -= OnUnHighlightItem;
    }

    private void OnHighlightItem(string id)
    {
        if (id == this.id)
        {
            highlight.color = Color.white;
            selected = true;
        }
    }

    private void OnUnHighlightItem(string id)
    {
        if (id == this.id)
        {
            highlight.color = sr.color;
            selected = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEventSys.current.ItemTriggerEnter(id);
    }

    private void OnTriggerExit(Collider other)
    {
        GameEventSys.current.ItemTriggerExit(id);
    }

}
