using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoverSystem : MonoBehaviour
{
    public bool covered = false;
    public float healPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (float damage)
    {
		healPoints -= damage;
	}

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
        }
    }
}
