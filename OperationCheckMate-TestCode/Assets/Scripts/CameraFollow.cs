using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*private Transform _lookAt;
   private Vector3 _startOffsets;
   // Start is called before the first frame update
   void Start()
   {
       _lookAt = GameObject.FindGameObjectWithTag("Player").transform;
       _startOffsets = transform.position - _lookAt.position;
   }

   // Update is called once per frame
   void Update()
   {
       float _value = 5 * Time.deltaTime;
       transform.position = _lookAt.position + _startOffsets;

       transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);


   }*/

    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offstesVertical;
    private Vector3 offstesHorizontal;

    private bool _cameraTwist = false;

    void Start()
    {
        offstesVertical = new Vector3(player.position.x, player.position.y + 5.0f, player.position.z + 3.0f);
        offstesHorizontal = new Vector3(player.position.x, player.position.y + player.position.z);
    }

    void LateUpdate()
    {

        //Debug.Log(Input.GetAxis("rightJoyX"));


       // offstesVertical = Quaternion.AngleAxis(Input.GetAxis("Horizontal") * turnSpeed, Vector3.up) * offstesVertical;
        //  offstesHorizontal = Quaternion.AngleAxis(Input.GetAxis("Vertical") * turnSpeed, Vector3.forward) * offstesHorizontal;





        transform.position = player.position + offstesVertical;
        transform.LookAt(player.position);
    }
}
