using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_oklm : MonoBehaviour
{
	public float vue_obstacle = 5;
	public float vitesse_swat = 1f;
	public float guet_vitesse = 1f;

	public bool aggro = false;
	public bool actionTime = false;
	public bool jeBouge = false;
	public bool jeRegarde = false;
	
	private Rigidbody rig;
	
	public float tempsMortTime = 1f;
	public float tempsClairTime = 4f;
	public float observe_time = 1f;
	public float sens = 1f;
	
	public bool decision_prise = false;

	void Start ()
	{
		rig = GetComponent<Rigidbody>();
		// InvokeRepeating("tourne", 0.0f, Random.Range(1f, 3f));
	}
	
	public void isAggro ()
	{
		vitesse_swat += 15;
	}
	
	public void RAS ()
	{
		vitesse_swat -= 15;
	}

	public void bouge (float vitesse_swat)
	{
		transform.Translate(0, 0, Time.deltaTime * vitesse_swat);
	}
	public void tourne (float gd)
	{
		transform.Rotate(0, Random.Range(15f, 90f) * gd * Time.deltaTime * guet_vitesse , 0);
	}

	public void observe (float sens)
	{
		transform.Rotate(0, Time.deltaTime * guet_vitesse * sens * 100, 0);
	}
	
	public void stopGuet ()
	{
		jeRegarde = false;
	}

	public void tempsMort ()
	{
		Invoke ("tempsClair", tempsClairTime);
	}
	public void tempsClair ()
	{
		actionTime = false;
		decision_prise = false;
		jeRegarde = false;
		jeBouge = false;
	}
	
	public void decision ()
	{
		if (Random.Range(0f, 100f) >= 30f)
		{
			jeBouge = true;
		}
		else
		{
			jeRegarde = true;
		}
		
		if (Random.Range(0f, 100f) >= 50f)
		{
			sens = 1;
		}
		else
		{
			sens = -1;
		}
		decision_prise = true;
	}

	void Update ()
	{
		Ray ray = new Ray (transform.position, transform.forward);
		Ray rayG = new Ray (transform.position, transform.right);
		Ray rayD = new Ray (transform.position, -transform.right);
		RaycastHit hitInfo;
		if (actionTime == false)
		{
			Invoke ("tempsMort", tempsMortTime);				//tempsMort reste true 1sec si sa variable tempsMortTime est = 1sec;
			actionTime = true;									//tempsClair reste true 4sec si sa variable tempsClairTime est = à 4sec;
		}
		if (actionTime == true && decision_prise == false)
		{
			decision();
		}
		if (actionTime == true)
		{
			if (jeBouge == true)
			{
				if (Physics.Raycast (rayG, out hitInfo, vue_obstacle))
				{
					Debug.DrawLine (rayG.origin, hitInfo.point, Color.red);
					sens = 1f;
					tourne(1f);
					bouge(vitesse_swat/2);
				}
				if (Physics.Raycast (rayD, out hitInfo, vue_obstacle))
				{
					Debug.DrawLine (rayD.origin, hitInfo.point, Color.red);
					sens = -1f;
					tourne(-1f);
					bouge(vitesse_swat/2);
				}
				if (Physics.Raycast (ray, out hitInfo, vue_obstacle))
				{
					Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
					tourne(sens);
				}
				else
				{
					Debug.DrawLine (ray.origin, ray.origin + ray.direction * 100, Color.green);
					Debug.DrawLine (rayG.origin, rayG.origin + rayG.direction * 100, Color.blue);
					Debug.DrawLine (rayD.origin, rayD.origin + rayD.direction * 100, Color.yellow);
					bouge(vitesse_swat);
				}
			}
			if (jeRegarde == true)
			{
				observe(sens);
				Invoke("stopGuet", observe_time);
			}
		}
	}
}
