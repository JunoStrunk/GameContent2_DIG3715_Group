using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	Vector3 rot;
	// Update is called once per frame
	void Update()
	{
		// transform.LookAt(Camera.main.transform.position, Vector3.up);
		rot = transform.position - Camera.main.transform.position;
		rot.y = 0;
		transform.rotation = Quaternion.LookRotation(rot);
	}
}
