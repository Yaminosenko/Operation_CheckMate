using UnityEngine;
using System.Collections;
using System.Linq;
using RTS_Cam;
using System.Collections.Generic;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour 
{
    private RTS_Camera cam;
    private new Camera camera;
    public Transform _target;
    private Transform _transCam;
    public string targetsTag;
    public List<KeyCode> _keyList = new List<KeyCode>();

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        camera = gameObject.GetComponent<Camera>();
        _transCam = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(targetsTag))
                    cam.SetTarget(hit.transform);
                else
                    cam.ResetTarget();
            }
        }

        //if (_target != null)
        //{
        //    if(_transCam == _target)
        //    {
        //        cam.ResetTarget();
        //    }

        //    if (Input.anyKey)
        //    {
        //        cam.ResetTarget();
        //    }
        //}
    }

    public void NewTarget()
    {
        cam.SetTarget(_target);
        StartCoroutine(TravelTime());
    }

    IEnumerator TravelTime()
    {
        yield return new WaitForSeconds(1);
        cam.ResetTarget();
    }
}
