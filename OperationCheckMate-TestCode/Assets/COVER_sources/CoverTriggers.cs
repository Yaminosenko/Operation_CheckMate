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
            Player p = col.gameObject.GetComponent<Player>();
            p.covered = true;
            if(_bigCover == false)
            {
                p._bigCover = false;
            }
            if(_bigCover == true)
            {
                p._bigCover = true;
            }
		}
	}
    public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            col.gameObject.GetComponent<Player>().covered = false;
		}
	}


}
