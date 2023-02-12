using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapCanvas;
    public GameObject floor1;
    public GameObject floor2;
    public bool mapOn = false;
    public int floorNum = 1;

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
        mapCanvas.SetActive(true);
        mapOn = true;
    }
    void turnOff()
    {
        mapCanvas.SetActive(false);
        mapOn = false;
    }
}
