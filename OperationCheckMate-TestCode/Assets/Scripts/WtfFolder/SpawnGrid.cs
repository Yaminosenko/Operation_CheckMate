using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {

	public GameObject block1; 

	public int worldWidth = 10;
	public int worldHeight = 10;
	public float multiply = 1f;

	//public float spawnSpeed = 0.2f;

	void  Start ()
	{
		for(int x = 0; x < worldWidth; x++)
		{
			for(int z = 0; z < worldHeight; z++)
			{
				GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
				block.transform.parent = transform;
				block.transform.localPosition = new Vector3(x*multiply, 0, z*multiply);
			}
		}
	}
}
