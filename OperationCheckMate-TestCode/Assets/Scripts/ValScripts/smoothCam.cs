using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCam : MonoBehaviour
{
	Transform rootNode;
    Transform carCam;
    Transform car;
    Rigidbody carPhysics;
	public GameObject laCam;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;
    
    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 10.0f;
    
    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float cameraRotationSpeed = 5.0f;

    void Start ()
    {
        carCam = laCam.GetComponent<Transform>();
		rootNode = GetComponent<Transform>();
		// car = null;
		// carPhysics = null;
		GetThePlayer();
    }

    public void GetThePlayer ()
    {
		car = GameObject.FindWithTag("Player").GetComponent<Transform>();
		carPhysics = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

	public void Movement()
	{
		Quaternion look;

        // Moves the camera to match the car's position.
        rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);

        // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
        // if (carPhysics.velocity.magnitude < rotationThreshold)
            look = Quaternion.LookRotation(car.forward);
        // else
            // look = Quaternion.LookRotation(carPhysics.velocity.normalized);
        
        // Rotate the camera towards the velocity vector.
        look = Quaternion.Slerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);                
        rootNode.rotation = look;
	}
    void FixedUpdate()
    {
		if (car != null && carPhysics != null)
			Movement();
    }
}