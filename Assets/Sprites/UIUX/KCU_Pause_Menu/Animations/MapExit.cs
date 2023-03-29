using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExit : MonoBehaviour
{
    Animator myAnimator;
    private bool exitAnim;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        exitAnim = MapManager.mapExit;
        myAnimator.SetBool("exit", exitAnim);
    }
}
