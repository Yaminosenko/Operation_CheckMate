using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSquare : MonoBehaviour
{
    public Material mat;

    void Awake ()
    {
        MyDrawSquare(transform.position);
    }


	public void MyDrawSquare(Vector3 spawnPoint)
	{
		//Create 4 Vertices
		Vector3[] vert = new Vector3[4];
		vert[0] = new Vector3(-1 + spawnPoint.x , 0, 1 + spawnPoint.z);//0                 0              1
		vert[1] = new Vector3(1 + spawnPoint.x , 0, 1 + spawnPoint.z);//1
		vert[2] = new Vector3(1 + spawnPoint.x , 0, -1 + spawnPoint.z);//2
		vert[3] = new Vector3(-1 + spawnPoint.x , 0, -1 + spawnPoint.z);//3                3               2
		
		//Create the triangles using the vertices
		int[] tris = new int[6];
		tris[0] = 0;
		tris[1] = 1;
		tris[2] = 2;
		tris[3] = 0;
		tris[4] = 2;
		tris[5] = 3;
		
		//Create a new mesh and pass down the vertices and triangles
		Mesh mesh = new Mesh();
		mesh.vertices = vert;
		mesh.triangles = tris;
		
		//Make sure mesh filter and mesh renderer componenets are attached
		if (!GetComponent<MeshFilter>())
			gameObject.AddComponent<MeshFilter>();
		
		if (!GetComponent<MeshRenderer>())
			gameObject.AddComponent<MeshRenderer>();
		
		//Pass down the mesh data to mesh filter
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		//Send material data to mesh renderer
		gameObject.GetComponent<MeshRenderer>().material = mat;
	}
}
