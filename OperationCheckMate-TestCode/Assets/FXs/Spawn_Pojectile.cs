using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Pojectile : MonoBehaviour
{

    public GameObject firePoint;
    public GameObject projectile;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnVFX();
        }
    }

    void SpawnVFX()
    {
        Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
    }
}