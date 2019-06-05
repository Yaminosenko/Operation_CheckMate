using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseRotation : MonoBehaviour
{
	public Transform target;
	public int degrees = 10;
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (1)) 
		{    
			transform.RotateAround (target.position, Vector3.up, Input.GetAxis ("Mouse X")* degrees);
		}
		if(!Input.GetMouseButton(1))
			transform.RotateAround (target.position, Vector3.up, 0f);    
	}
}
