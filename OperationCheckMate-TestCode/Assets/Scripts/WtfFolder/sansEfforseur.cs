using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sansEfforseur : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject Aexit;
    public GameObject Bexit;
	Vector3 offset1;
	Vector3 offset2;
	Vector3 pPos;
	public GameObject player;
	public float tpPhysique = 2.0f;
	public float reloadTimer = 5.0f;
	public float detectMargin = 0.2f;
	public bool engaged = false;
    public float speed = 5f;
    float dist1 = 0;
    float dist2 = 0;

    void Start()
    {
        offset1 = A.transform.position;
        offset2 = B.transform.position;
    }

	public void ReloadTp ()
	{
		engaged = false;
	}

	public void leTp1 ()
	{
		//player = A.GetComponent<trigIdentifier>().thePlayer;
        //player.transform.position = offset2;
        //player.transform.position = Vector3.MoveTowards(transform.position, B.transform.position, speed * Time.deltaTime);
        //player.GetComponent<navAgent>().Pos = B.transform.position;
        player.GetComponent<navAgent>().agent.Warp(B.transform.position);
        player.GetComponent<navAgent>().agent.SetDestination(Bexit.transform.position);
        player.transform.position = Bexit.transform.position;

        //Debug.Log("TOPKEK");
		Invoke("ReloadTp", reloadTimer);
	}

	public void leTp2 ()
	{
		//player = B.GetComponent<trigIdentifier>().thePlayer;
        //player.transform.position = offset1;
        //player.transform.position = Vector3.MoveTowards(transform.position, B.transform.position, speed * Time.deltaTime);
        //player.GetComponent<navAgent>().Pos = A.transform.position;
        player.GetComponent<navAgent>().agent.Warp(A.transform.position);
        player.GetComponent<navAgent>().agent.SetDestination(Aexit.transform.position);
        player.transform.position = Aexit.transform.position;

        //Debug.Log("TOPKEK");
		Invoke("ReloadTp", reloadTimer);
	}

    void Update()
    {
        if (A.GetComponent<trigIdentifier>().thePlayer != null && engaged == false)
        {
            player = A.GetComponent<trigIdentifier>().thePlayer;
            pPos = player.transform.position;
            dist1 = Vector3.Distance(pPos, offset1);
            Debug.Log(dist1.ToString());
            if (engaged == false)
            {
                if (dist1 <= detectMargin)
                {
                    Invoke("leTp1", tpPhysique);
                    engaged = true;
                    return;
                }
            }
        }
        if (B.GetComponent<trigIdentifier>().thePlayer != null && engaged == false)
        {
            player = B.GetComponent<trigIdentifier>().thePlayer;
            pPos = player.transform.position;
            dist2 = Vector3.Distance(pPos, offset2);
            Debug.Log(dist2.ToString());
            if (engaged == false)
            {
                if (dist2 <= detectMargin)
                {
                    Invoke("leTp2", tpPhysique);
                    engaged = true;
                    return;
                }
            }
        }
            
    }
}
