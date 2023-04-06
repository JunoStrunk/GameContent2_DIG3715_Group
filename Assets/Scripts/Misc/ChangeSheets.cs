using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSheets : MonoBehaviour
{
	public Material cleanMat;

	public void CleanSheets()
	{
		this.GetComponent<MeshRenderer>().material = cleanMat;
	}
}
