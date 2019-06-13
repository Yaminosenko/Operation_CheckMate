using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeColor : MonoBehaviour
{
	public Color _color1;
	public Color _color2;
	public Color _color3;
  	public Renderer rend;
	public bool isHovered = false;
	public Gradient gradient1;
	public Gradient gradient2;
	public bool insideZone = false;

	void Start()
	{
		rend.material.color = _color1;
	}
/*
	public void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "longMoveZone")
		{
			// rend.material.color = _color2;
			rend.material.SetColor ("_TintColor", _color2);
		}
		if (col.gameObject.tag == "moveZone")
		{
			// rend.material.color = _color2;
			rend.material.SetColor ("_TintColor", _color2);
		}
	}

	public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "moveZone")
		{
			// rend.material.color = _color1;
			rend.material.SetColor ("_TintColor", _color1);
		}
		if (col.gameObject.tag == "longMoveZone")
		{
			// rend.material.color = _color1;
			rend.material.SetColor ("_TintColor", _color1);
		}
	}
	*/
	public void BlueReset ()
	{
		rend.material.SetColor ("_TintColor", _color2);
	}
	public void BlackReset ()
	{
		rend.material.SetColor ("_TintColor", _color1);
	}
	
	void OnMouseOver()
	{
		var trail = gameObject.GetComponent<ParticleSystem>().trails;
		if (insideZone == true)
			trail.colorOverLifetime = gradient1;
		else
			trail.colorOverLifetime = gradient2;

		gameObject.GetComponent<ParticleSystem>().Play();

		if (isHovered == false)
		{
			//gameObject.tag = "moused";
			isHovered = true;
		}
	}

	void OnMouseExit()
	{
	// if (isHovered == true)
	// {
		gameObject.GetComponent<ParticleSystem>().Stop();
		//gameObject.tag = "grid";
		isHovered = false;
		gameObject.GetComponent<ParticleSystem>().Clear();
	// }
	}
}
