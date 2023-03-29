using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerExit : MonoBehaviour
{
    Animator myAnimator;
    private bool exitAnim;
    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    

    void Update()
    {
        exitAnim = PauseScript.bannerExit;
        myAnimator.SetBool("exit", exitAnim);
    }
}
