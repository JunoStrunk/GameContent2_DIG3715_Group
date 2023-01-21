using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /* Notes ============================
    Possible issue with two items being too close to each other

     ================================== */
    // Public
    
    public SpriteRenderer highlight;
    public bool selected = false;

    // Private
    string id;
    SpriteRenderer sr;

    private void Update()
    {
        if(selected && Input.GetKeyDown(KeyCode.E))
        {
            GameEventSys.current.ItemPickUp(id, sr.sprite);
            Destroy(this.gameObject);
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
            highlight.color = Color.blue;
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
