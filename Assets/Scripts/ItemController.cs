using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /* Notes ============================
    Possible issue with two items being too close to each other

     ================================== */
    // Public
    public int id;
    public SpriteRenderer highlight;

    private void Start() //Important to listen only on start or else there will be a null reference for singleton
    {
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

    private void OnHighlightItem(int id)
    {
        if(id == this.id)
            highlight.color = Color.white;
    }

    private void OnUnHighlightItem(int id)
    {
        if(id == this.id)
            highlight.color = Color.blue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEventSys.current.ItemTriggerEnter(id);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameEventSys.current.ItemTriggerExit(id);
    }

}
