using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAnimController : MonoBehaviour
{
    [SerializeField] private GameObject blackscreen;
    public void onAnimFinish()
    {
        blackscreen.SetActive(false);
    }
}