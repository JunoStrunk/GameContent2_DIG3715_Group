using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Audio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip MusicPhase1;
    public AudioClip MusicPhase2;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MusicPhase1;
        audioSource.loop = true;
        audioSource.Play();
        GameEventSys.current.onTimerEnded += bgmAudio;  
    }
    public void bgmAudio()
    {
        print("test");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MusicPhase2;
        audioSource.loop = true; 
        audioSource.Play();
    }
}