using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTriggers : MonoBehaviour
{
    public bool _bigCover = false;
    public float _triggerOrientation;
    public Transform _fovSavior;
    public bool _corner = false;

    public float _whichAxis;
    public GameObject _pngCoverLow;
    public GameObject _pngCoverHigh;
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

        if (_fovSavior != null)
        {
            _fovSavior.eulerAngles = new Vector3(_fovSavior.eulerAngles.x, _fovSavior.eulerAngles.y + -_triggerOrientation, _fovSavior.eulerAngles.z);
            
        }

    }

    private void OnMouseOver()
    {
        Debug.Log("unefois");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if(hit.collider.tag == "grid")
            {
                TriggerChangeColor t = hit.collider.GetComponent<TriggerChangeColor>();

                t.mousePasOver();
            }
        }
        if(_bigCover == true)
        {
        Debug.Log("mais");
            _pngCoverHigh.SetActive(true);
            _pngCoverLow.SetActive(false);
        }
        else
        {
        Debug.Log("pasCinq");
            _pngCoverHigh.SetActive(false);
            _pngCoverLow.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "grid")
            {
                TriggerChangeColor t = hit.collider.GetComponent<TriggerChangeColor>();

                t.MousePasExit();
            }
        }
        _pngCoverHigh.SetActive(false);
        _pngCoverLow.SetActive(false);
    }

    public void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            
            Player p = col.gameObject.GetComponent<Player>();
            FieldOfView f = col.gameObject.GetComponent<FieldOfView>();
            if(_corner == true)
            {
                if (_fovSavior != null)
                {
                 
                    f._lestestdufov = _fovSavior;
                }
            }

            
            p.covered = true;
            p._triggerCover = this.gameObject;
            p._axisRot = _whichAxis;
            if(_bigCover == false)
            {
             
                p._bigCover = false;
                p.CoverLow(true);
            }
            if(_bigCover == true)
            {

                p._bigCover = true;
                p.CoverHigh(true);
            }
		}
	}
    public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            Player p = col.gameObject.GetComponent<Player>();
            FieldOfView f = col.gameObject.GetComponent<FieldOfView>();
            p.covered = false;
            p._bigCover = false;
            p._triggerCover = null;
            p._axisRot = 0;
            f._lestestdufov = null;
            p.CoverHigh(false);
            p.CoverLow(false);
        }
	}


}
