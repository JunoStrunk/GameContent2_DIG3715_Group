using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip walking;
    private bool isPlaying;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (PlayerMovement.isMoving == true)
        {
            if (isPlaying) return;
            audioSource.clip = walking;
            audioSource.loop = true;
            audioSource.Play();
            isPlaying = true;
        }
        else
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
}
