using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceToCam : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        // transform.LookAt(target);
		transform.rotation = Quaternion.LookRotation(target.transform.forward, target.transform.up);
    }
}
