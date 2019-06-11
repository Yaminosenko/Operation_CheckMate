using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTriggers : MonoBehaviour
{
    public bool _bigCover = false;
    public float _triggerOrientation;

    public float _whichAxis;
   // public float _newOrientation;

    private void OnEnable()
    {
        _triggerOrientation = transform.localEulerAngles.y;

        if (_triggerOrientation >= -1 && _triggerOrientation <= 1)
        {
            _whichAxis = 1;
            //Debug.Log(transform.position.x);
        }
        else if(_triggerOrientation >= 179 && _triggerOrientation <= 181)
        {
            _whichAxis = 2;
           // Debug.Log(transform.position.x);
        }
        else if (_triggerOrientation >= 89 && _triggerOrientation <= 91)
        {
            _whichAxis = 3;
            //Debug.Log(transform.position.z);
            
        }else if (_triggerOrientation >= 269 && _triggerOrientation <= 271)
        {
            _whichAxis = 4;
            //Debug.Log(transform.position.z);
        }


        
    }

    public void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            
            Player p = col.gameObject.GetComponent<Player>();
            p.covered = true;
            p._triggerCover = this.gameObject;
            p._axisRot = _whichAxis;
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
            Player p = col.gameObject.GetComponent<Player>();
            p.covered = false;
            p._bigCover = false;
            p._triggerCover = null;
            p._axisRot = 0;
        }
	}


}
