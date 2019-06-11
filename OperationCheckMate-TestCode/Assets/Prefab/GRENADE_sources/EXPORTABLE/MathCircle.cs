using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathCircle : MonoBehaviour
{
    public float radius;
    public float margin = 1.5f;
    public GrenadeScript greScr; 
    public LayerMask layer;
    Vector3 mousePos;
    public Camera cam;
    public GrenadeAimingSystem GAS;

    void Start()
    {
        radius = greScr.Radius;
    }

    void Update()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("grid");

        if (Physics.Raycast(camRay, out hit, 200f, layer))
        {
            mousePos = hit.point;
        }

        foreach (GameObject obj in objs)
        {
            if (GAS.cursorThere == false)
            {
                //obj.GetComponent<TriggerChangeColor>().rend.material.SetColor ("_TintColor", obj.GetComponent<TriggerChangeColor>()._color1);
                //obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, 0.2f, obj.transform.localScale.z);
            }
            else
            {
                float dist = Vector3.Distance(mousePos, obj.transform.position);
                if (dist <= radius + margin && dist >= radius - margin && GAS.inRange == true)
                {
                    //obj.GetComponent<TriggerChangeColor>().rend.material.SetColor ("_TintColor", obj.GetComponent<TriggerChangeColor>()._color3);
                    obj.transform.localScale = new Vector3(obj.transform.localScale.x, 2f, obj.transform.localScale.z);
                }
                else
                {
                    //obj.GetComponent<TriggerChangeColor>().rend.material.SetColor ("_TintColor", obj.GetComponent<TriggerChangeColor>()._color1);
                    obj.transform.localScale = new Vector3(obj.transform.localScale.x, 0.2f, obj.transform.localScale.z);
                }
            }
        }
    }
}
