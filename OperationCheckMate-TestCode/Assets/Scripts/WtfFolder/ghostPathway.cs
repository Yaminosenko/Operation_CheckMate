using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostPathway : MonoBehaviour
{
    // public Color _color1;
		public Color _color2;
    private Renderer rend;
		public Vector3 finalDestination;

    void Start()
    {
		// rend.material.color = _color1;
    }

	public void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "grid")
		{
			rend = col.gameObject.GetComponent<Renderer>();
			rend.material.SetColor ("_TintColor", _color2);
		}
		/*if (col.gameObject.tag == "moused")
		{
			Destroy(gameObject, 0.1f);
		}
		if (col.gameObject.tag == "ghost")
		{
			Destroy(gameObject, 0f);
		}
		if (col.gameObject.tag == "Player")
		{
			Destroy(gameObject, 0f);
		}*/
	}

	/*public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "ghostMoveZone")
		{
			finalDestination = transform.position;
			Destroy(gameObject, 0f);
		}
	}*/
}
