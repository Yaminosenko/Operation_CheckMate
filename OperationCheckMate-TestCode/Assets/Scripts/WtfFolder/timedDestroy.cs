using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestroy : MonoBehaviour
{
    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("tempDestroy", timer);
    }

    // Update is called once per frame
    public void tempDestroy ()
    {
        //DestroyImmediate (gameObject, true);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "ghost")
		{
			//Destroy(gameObject, 0f);
            gameObject.SetActive(false);
		}
	}
}
