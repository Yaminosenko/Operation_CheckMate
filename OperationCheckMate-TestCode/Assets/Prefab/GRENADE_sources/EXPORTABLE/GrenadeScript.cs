using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
	public float Radius = 20.0f;
	private float sphereRadius;
	private float damage;
	public float dmgRatio = 1f;
	public float sphereSpeed = 2f;
	public GameObject ExplosionEffect;
	public GameObject SoundOnCreate;

	Vector3 myPos;
	Vector3 aimPos;

	private bool growSphere = false;
	private bool exploded = false;

	public int explosionDmg = 10;
	public bool fixDamages = true;
	
	void Update ()
	{
		if (growSphere == true)
		{
			BOOM();
			if (sphereRadius <= Radius)
			{
				sphereRadius += Time.deltaTime * sphereSpeed;
			}
		}
		if (sphereRadius >= Radius)
			Destroy(gameObject, 0f);
	}

    void OnCollisionEnter (Collision other)
    {
		growSphere = true;
		Rigidbody rig = gameObject.GetComponent<Rigidbody>();
		rig.constraints =  RigidbodyConstraints.FreezeAll;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<SphereCollider>().enabled = false;
		if (exploded == false)
		{
			exploded = true;
			Instantiate(SoundOnCreate, transform.position, transform.rotation);
			Explode();
		}
    }

	void Explode ()
	{
		Instantiate(ExplosionEffect, transform.position, transform.rotation);
	}

	void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }

	void BOOM ()
	{
        Vector3 explosionPosition = transform.position;
		
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, sphereRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.transform.gameObject.tag == "Player")
            {
				myPos = transform.position;
				aimPos = hit.transform.gameObject.transform.position;
				
				var heading = aimPos - myPos;
				var distance = heading.magnitude;
				var direction = heading / distance;
				
				RaycastHit _hit;
				if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out _hit, Radius))
				{
					if (_hit.transform.gameObject == hit.transform.gameObject)
					{
                            Debug.Log("caca");
						Player Player = hit.transform.GetComponent<Player>();
						if (Player != null)
						{
							if (fixDamages == false)
							{
								float dist = Vector3.Distance(hit.transform.gameObject.transform.position, transform.position);

								if (dist < Radius)			
								{
									if (dist > 0f)
									{
										damage = (Radius / dist) * dmgRatio;
									}
									if (dist == 0f)
									{
										damage = Radius * dmgRatio;
									}
								}
							}
							else
							{
								damage = explosionDmg;
							}
							//if (Player.GetComponent<PlayerCoverSystem>().hitted == false)
							//{
								Player.GetComponent<Player>().TakeDmg((int)damage);
							//}
						}
					}
				}
            }
        }
	}
}
