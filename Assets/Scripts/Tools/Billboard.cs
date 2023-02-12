using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    /* Notes ===============================
    - This makes any sprite look at the camera at all times.
    - Drop on anything that is a billboard :)
    =======================================*/
    
    public bool omitXAxis = false;
    
    Vector3 rot;
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
        if(omitXAxis)
        {
            rot = transform.rotation.eulerAngles;
            rot.x = 0;
            transform.eulerAngles = rot;
        }   
    }
}
