using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofPack : MonoBehaviour
{
    public GameObject[] roofParts;
	private Renderer rend;

	public void showAll ()
	{
		foreach(GameObject roof in roofParts)
		{
			rend = roof.GetComponent<Renderer>();
			rend.enabled = true;
		}
	}

	public void hideAll ()
	{
		foreach(GameObject roof in roofParts)
		{
			rend = roof.GetComponent<Renderer>();
			rend.enabled = false;
		}
	}
}
