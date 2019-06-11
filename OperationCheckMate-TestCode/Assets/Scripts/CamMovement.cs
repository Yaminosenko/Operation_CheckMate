using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
	public float _speed = 5f;
	public float camZoomMin = 1f;
	public float camZoomMax = 10f;
	public GameObject cam;

	void Update ()
	{
			CamMove();
	}
	void CamMove()
	{
		if(Input.GetAxis ("Horizontal") < 0f)
		{
			transform.Translate(-transform.right * _speed, Space.World);
		}
		if(Input.GetAxis ("Horizontal") > 0f)
		{
			transform.Translate(transform.right * _speed, Space.World);
		}
		if(Input.GetAxis ("Vertical") < 0f)
		{
			transform.Translate(-transform.forward * _speed, Space.World);
		}
		if(Input.GetAxis ("Vertical") > 0f)
		{
			transform.Translate(transform.forward * _speed, Space.World);
		}
		
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		
		/*if (gameObject.GetComponentInChildren<Camera>().orthographicSize <= camZoomMin)
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize = camZoomMin;
		}
		else
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize -= scroll;
		}

		if (gameObject.GetComponentInChildren<Camera>().orthographicSize >= camZoomMax)
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize = camZoomMax;
		}
		else
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize -= scroll;
		}*/
		//
		if (scroll > 0)
		{
			transform.position += cam.transform.forward * Mathf.Clamp(scroll, camZoomMin, camZoomMax) * Time.deltaTime * _speed * 1000;
		}
		if (scroll < 0)
		{
			transform.position -= cam.transform.forward * Mathf.Clamp(scroll, camZoomMin, camZoomMax) * Time.deltaTime * _speed * 1000;
		}
		/*
		if (transform.position.z <= camZoomMin)
		{
			transform.Translate(transform.forward * 0f);
		}
		else
		{
			transform.Translate(transform.forward * -scroll, Space.World);
		}

		if (transform.position.z >= camZoomMax)
		{
			transform.Translate(transform.forward * 0f);
		}
		else
		{
			transform.Translate(transform.forward * -scroll, Space.World);
		}*/
	}
}
