using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallHack : MonoBehaviour
{
	public string myTag;

	public void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == myTag)
		{
			// Color colorA = other.transform.gameObject.GetComponent<Renderer>().material.color;
			// colorA.a = 0f;
			// other.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorA);
			other.transform.gameObject.GetComponent<Renderer>().enabled = false;
			if (other.gameObject.GetComponent<RoofPack>() != null)
				other.gameObject.GetComponent<RoofPack>().hideAll();
		}
	}
	public void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == myTag)
		{
			// Color colorB = other.transform.gameObject.GetComponent<Renderer>().material.color;
			// colorB.a = 1f;
			// other.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorB);
			other.transform.gameObject.GetComponent<Renderer>().enabled = true;
			if (other.gameObject.GetComponent<RoofPack>() != null)
				other.gameObject.GetComponent<RoofPack>().showAll();
		}
	}
}