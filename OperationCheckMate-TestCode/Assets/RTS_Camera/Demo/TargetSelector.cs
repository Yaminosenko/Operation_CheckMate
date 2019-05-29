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
    //public List<KeyCode> _keyList = new List<KeyCode>();

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        camera = gameObject.GetComponent<Camera>();
        _transCam = gameObject.GetComponent<Transform>();
    }

    public void NewTarget() // envois la target a suivre pour la cam
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
