using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverEnnemiTest : MonoBehaviour
{
    public GameObject focused;
    public float rangeMax;
    public float damages;
    public float coverRatio;
    public LineRenderer lr;

    public bool detected = false;
    public bool acted = false;
    //public float dynamicRange;

    Vector3 myPos;
    Vector3 aimPos;

    void Start()
    {
        //dynamicRange = 0f;
    }

    void Update()
    {
        myPos = transform.position;
        aimPos = focused.transform.position;
        
        var heading = aimPos - myPos;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (Input.GetButtonDown("Jump"))
        {
            if (acted == true)
            {
                acted = false;
            }
            else
            {
                acted = true;
            }
        }

        if (acted)
        {
           //dynamicRange = 0f;
            GiveDamage(direction);
        }
        else
        {
            lr.SetPosition(0, myPos);
            lr.SetPosition(1, aimPos);

           /* if (!detected)
                dynamicRange += Time.deltaTime;*/
            //Detector(dynamicRange);
        }
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, dynamicRange);
    }*/

    /* void Detector(float dynamicRange)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, dynamicRange);
		foreach (Collider hit in colliders)
		{
			if(hit.transform.gameObject.tag == "Player")
			{
				focused = hit.transform.gameObject;
                Debug.Log("Detected !");
                dynamicRange = 0f;
                detected = true;
			}
		}
    }*/

    void GiveDamage (Vector3 direction)
    {
        PlayerCoverSystem playerSys = focused.GetComponent<PlayerCoverSystem>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, rangeMax))
        {
            if (hit.transform.gameObject == focused)
            {
                if (playerSys.covered == true)
                {
                    playerSys.TakeDamage(damages * coverRatio);
                    Debug.Log("Damaged !" + " x" + damages * coverRatio);
                }
                else
                {
                    playerSys.TakeDamage(damages);
                    Debug.Log("Damaged !" + " x" + damages);
                }
            }
            else
            {
                playerSys.TakeDamage(0);
                Debug.Log("Damaged !" + " x" + 0f);
            }
        }

        acted = false;
        detected = false;
    }
}
