using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFace : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    //[SerializeField] private GameObject _cam;
    [SerializeField] private Canvas _myCanvas;

    private void OnEnable()
    {
        _myCanvas = GetComponent<Canvas>();
        m_Camera = Camera.main;
        
        _myCanvas.worldCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}
