using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigIdentifier : MonoBehaviour
{
    public GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			thePlayer = col.gameObject;
		}
	}

    public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			Invoke("makeItNull", 1f);
		}
	}

    public void makeItNull ()
    {
        thePlayer = null;
    }
}
