using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapUI;
    public GameObject floor1;
    public GameObject floor2;
    public static bool mapExit = false;

    public GameObject Map_closed_BTN;
    public GameObject Map_opened_BTN;

    [HideInInspector] public bool mapOn = false;
    //public int floorNum = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (mapOn == false)
            {
                turnOn();
            }
            else if (mapOn == true)
            {
                turnOff();
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (mapOn == false)
            {
                turnOn();
            }
            else if (mapOn == true)
            {
                turnOff();
            }
        }

        if (mapOn == true)
        {            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                floor1.SetActive(true);
                floor2.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                floor1.SetActive(false);
                floor2.SetActive(true);
            }
        }     
    }
    void turnOn()
    {
        mapUI.SetActive(true);
        mapOn = true;
    }
    void turnOff()
    {
        StartCoroutine(removeMap());
        mapOn = false;
    }
    public void openMap()
    {
        if (mapOn == false)
        {
            turnOn();
            Map_closed_BTN.SetActive(false);
            Map_opened_BTN.SetActive(true);
        }
        else if (mapOn == true)
        {
            turnOff();
            Map_closed_BTN.SetActive(true);
            Map_opened_BTN.SetActive(false);
        }
    }
    public void mapUp()
    {
        floor1.SetActive(false);
        floor2.SetActive(true);            
        
    }
    public void mapDown()
    {
        floor1.SetActive(true);
        floor2.SetActive(false);        
    }
    IEnumerator removeMap()
    {
        mapExit = true;
        yield return new WaitForSecondsRealtime(.5f);
        mapUI.SetActive(false);
        mapExit = false;
    }
}

