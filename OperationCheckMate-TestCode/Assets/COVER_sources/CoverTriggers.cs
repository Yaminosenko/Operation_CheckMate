using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTriggers : MonoBehaviour
{
    public bool _bigCover = false;

    public void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            col.gameObject.GetComponent<PlayerCoverSystem>().covered = true;
		}
	}
    public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            col.gameObject.GetComponent<PlayerCoverSystem>().covered = false;
		}
	}


}
