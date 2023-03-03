using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
	//public variables
	// public bool followPlayer = false;

	private Transform target;
	// private Transform originalTarget;
	private Vector3 rot;

	void Start()
    {
        target = this.transform.GetChild(0).transform; //Point will always be first child
		// originalTarget = target;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Camera.main.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
            col.transform.rotation = Quaternion.Euler(0, target.transform.rotation.y, 0);
            Camera.main.transform.SetParent(target);
            
            // if(followPlayer)
            // {
				
			// }
		}
    }

    // void OnTriggerExit(Collider col)
    // {
    //     if(col.CompareTag("Player"))
    //     {
	// 		target.position = originalTarget.position;
	// 	}
    // }
}
