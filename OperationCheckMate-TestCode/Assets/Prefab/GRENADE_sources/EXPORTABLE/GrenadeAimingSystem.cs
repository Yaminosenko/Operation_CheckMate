using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAimingSystem : MonoBehaviour
{
    public Rigidbody grenadePrefab;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;

    public Camera cam;

    public float myZ;
    public bool cursorThere = false;
    public float maxRange = 20f;

    public bool inRange = false;
    public bool _isActive = false;

    public Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float m_CurrentClipLength;
    public GameObject Player;
    public BaseComp _compScript;
    public GameObject _mainUnit;

    public bool throwed = false;
    public Vector3 Vo;
    public LineRenderer lr;

    private void Start()
    {
        FieldOfView f = _mainUnit.GetComponent<FieldOfView>();
        Player p = _mainUnit.GetComponent<Player>();
        if (f._whichTeamAreYou == false)
        {
            anim = p._playerMesh[0].GetComponent<Animator>();
        }
        else
        {
            anim = p._playerMesh[1].GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (throwed == false)
            LaunchProjectile();
        CursorGestion();
        
        if (cursorThere == true)
        {
            lr.SetWidth(0.1f, 0.1f);
        }
        else
        {
            lr.SetWidth(0f, 0f);
        }
    }

    void CursorGestion ()
    {
        if (_isActive == true)
        {
            cursorThere = !cursorThere;
            _isActive = false;
            throwed = false;
            //cursor.SetActive(cursorThere);
        }
        if (cursorThere == true && throwed == false)
        {
            Vector3 lookGrenade = new Vector3(cursor.transform.position.x, Player.transform.position.y, cursor.transform.position.z);
            Player.transform.LookAt(lookGrenade);
        }
    }

    public void TimedLaunch ()
    {
        Rigidbody obj = Instantiate(grenadePrefab, shootPoint.position, Quaternion.identity);
        obj.velocity = Vo;
        cursorThere = false;
    }

    void LaunchProjectile ()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 200f, layer))
        {
            cursor.transform.position = hit.point + Vector3.up;

            Vo = CalculateVelocity(hit.point, shootPoint.position, myZ);
            float dist = Vector3.Distance(cursor.transform.position, transform.position);

            transform.rotation = Quaternion.LookRotation(Vo);

            if (dist <= maxRange)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
            if (Input.GetMouseButtonDown(0) && cursorThere == true && inRange == true)
            {
                if (throwed == false)
                {
                    Vector3 _here = cursor.transform.position;
                    throwed = true;
                    StartCoroutine(_compScript.WaitAfterGrenadeLaunch());
                }
                Player p = _mainUnit.GetComponent<Player>();
                p.CoverHigh(false);
                p.CoverLow(false);
                anim.SetTrigger("isThrowingGrenade");

                float AnimationLength(string name)
                {
                    float time = 0;
                    RuntimeAnimatorController ac = anim.runtimeAnimatorController;   

                    for (int i = 0; i < ac.animationClips.Length; i++)
                        if (ac.animationClips[i].name == name)
                            time = ac.animationClips[i].length;

                    return time;
                }
                Invoke("TimedLaunch", AnimationLength("grenade")*1/2);
            }
        }

        Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0f;

            float Sy = distance.y;
            float Sxz = distanceXZ.magnitude;

            float Vxz = Sxz / time;
            float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            Vector3 result = distanceXZ.normalized;
            result *= Vxz;
            result.y = Vy;

            return result;
        }
    }
}
