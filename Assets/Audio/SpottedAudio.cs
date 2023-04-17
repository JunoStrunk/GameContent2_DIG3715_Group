using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip spotted;
    private bool isPlaying;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (EnemyStateChase.seePlayerAudio == true)
        {
            if (isPlaying) return;
            audioSource.clip = spotted;
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
