using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnNocontact : MonoBehaviour
{
    public float lazerMax = 2f;

    void Start()
    {
        Invoke ("destroyLazer", 1f);
    }

    public void destroyLazer ()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(downRay, out hit))
        {
            if (hit.distance > lazerMax)
            {
                Destroy (gameObject, 0f);
            }
        }
    }
}
