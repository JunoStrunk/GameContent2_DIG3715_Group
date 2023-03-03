using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsExit : MonoBehaviour
{
    Animator myAnimator;
    private bool exitAnim;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        exitAnim = PauseScript.controlsExit;
        myAnimator.SetBool("exit", exitAnim);
    }
}
