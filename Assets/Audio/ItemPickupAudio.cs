using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip pickup;
    public AudioClip drop;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameEventSys.current.onItemPickUpSound += playClipPickup;  
        GameEventSys.current.onItemDropSound += playClipDrop;  
    }
    private void OnDisable()
    {
        GameEventSys.current.onItemPickUpSound -= playClipPickup;
        GameEventSys.current.onItemDropSound -= playClipDrop;  
    }
    private void OnDestroy()
    {
        GameEventSys.current.onItemPickUpSound -= playClipPickup;
        GameEventSys.current.onItemDropSound -= playClipDrop;  
    }
    public void playClipPickup()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(pickup);
    }
    public void playClipDrop()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drop);
    }
}
