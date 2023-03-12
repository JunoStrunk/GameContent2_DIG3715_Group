using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    public Image thisButton;
    public Image detective;
    private int buttonNum;
    private int detectiveNum;

    public Sprite[] buttonArray;
    public Sprite[] detectiveArray;

    // Start is called before the first frame update
    public void onPointerEnter()
    {
        buttonNum = 1;
        thisButton.sprite = buttonArray[buttonNum];
        detectiveNum = 1;
        detective.sprite = detectiveArray[detectiveNum];
        
    }
    public void onPointerExit()
    {
        buttonNum = 0;
        thisButton.sprite = buttonArray[buttonNum];
        detectiveNum = 0;
        detective.sprite = detectiveArray[detectiveNum];
        
    }

}
