using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = this.transform.GetChild(0).transform; //Point will always be first child
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Camera.main.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
            col.transform.rotation = Quaternion.Euler(0, target.transform.rotation.y, 0);
        }
    }
}
