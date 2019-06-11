using UnityEngine;
using System.Collections;

public class RenderBallisticPath : MonoBehaviour
{
	//public float initialVelocity = 10.0f;
	public float timeResolution = 0.02f;
	public float maxTime = 10.0f;
	public LayerMask layerMask = -1;

	private GameObject explosionDisplayInstance;

	private LineRenderer lineRenderer;
	public GrenadeAimingSystem GAS;

	// Use this for initialization
	void Start ()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 velocityVector = GAS.Vo;
		lineRenderer.SetVertexCount((int)(maxTime/timeResolution));

		int index = 0;

		//Vector3 currentPosition = transform.position;
		Vector3 currentPosition = GAS.shootPoint.position;

		for(float t = 0.0f; t < maxTime; t += timeResolution)
		{
			lineRenderer.SetPosition(index,currentPosition);

			RaycastHit hit;

			if(Physics.Raycast(currentPosition,velocityVector,out hit,velocityVector.magnitude*timeResolution,layerMask))
			{
				lineRenderer.SetVertexCount(index+2);

				lineRenderer.SetPosition(index+1,hit.point);

				break;
			}

			currentPosition += velocityVector*timeResolution;
			velocityVector += Physics.gravity*timeResolution;
			index++;
		}
	}
}