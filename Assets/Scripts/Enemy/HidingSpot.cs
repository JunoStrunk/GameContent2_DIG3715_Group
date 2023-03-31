using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			// GameEventSys.current.PlayerHides(true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			// GameEventSys.current.PlayerHides(false);
		}
	}
}
