﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieldOfView : MonoBehaviour { // crédit: Sebtian Lague pour la base du script d'origine 
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMaskTeam1;
    public LayerMask targetMaskTeam2;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
   // [SerializeField] private BaseComp _competanceScript;
    public bool _isActive = false;
    public List<Transform> visibleTargets = new List<Transform>();
    [SerializeField]private Material _red;
    [SerializeField]private Material _targetMaterial;
    [SerializeField] private Transform[] _currentTargets;
    [SerializeField] private Transform _gd;
     public Transform _lestestdufov;

    public BaseComp _baseComp;
    
    private bool _aimTrue = false;

    public bool _whichTeamAreYou = false;
    public Transform _CamPos;

    [HideInInspector]

 
    public Transform focused;
    public LineRenderer lr;
    public bool acted = false;
    Vector3 myPos;
    Vector3 aimPos;

    public CameraMouv _camMouvScript;
    public bool _swap = false;
    public bool _isAimaing = false;
    public Transform _actualTarget;
    private int _nbOfTarget = 0;
    private int _targetSelect = 1;
    public float _distanceTarget;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;
    public int _listNb;
    public Vector3 direction;
    public int _actuaTargetCover;
    public bool _isFlank = false;
    public bool _isOverwatched = false;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;


    private void Awake()
    {
        
        if(_whichTeamAreYou == false)
        {
            targetMask = targetMaskTeam1;
        }
        else
        {
            targetMask = targetMaskTeam2;
        }
    }

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", .2f);
        
    }


    IEnumerator FindTargetsWithDelay(float delay) //refresh des targets 
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            //if(_isActive == true)
            //{
                FindVisibleTargets();
            //}

        }
    }

    void LateUpdate()
    {

        if (_isActive == true)
        {
            DrawFieldOfView();

            
            if (_aimTrue == true)
            {
                SelectTarget();
            }

        }
            if(_isOverwatched == true)
            {
                OverwatchActivate();
            }
       
    }

    void FindVisibleTargets() //mise en place des targets dans une list 
    {
        _listNb = visibleTargets.Count;
        visibleTargets.Clear();
        Transform t;
        if (_lestestdufov!= null)
        {
             t = _lestestdufov.transform;
        }
        else
        {
             t = transform;
        }

       

        Collider[] targetsInViewRadius = Physics.OverlapSphere(t.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - t.position).normalized;
            if (Vector3.Angle(t.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(t.position, target.position);
                if (!Physics.Raycast(t.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (target.GetComponent<Player>()._dead == false)
                    {
                        visibleTargets.Add(target);
                    }
                    
                }
            }
        }
    }

    public void AimTarget() //lance à la fin du déplacement penser a clear a la fin du tour 
    {
        foreach (Transform Target in visibleTargets)
        {
            //if (Target.GetComponent<Player>()._dead == false)
            //{
                _nbOfTarget += 1;
            //}
        }
        //Debug.Log(_nbOfTarget);
        _aimTrue = true;
    }

    void SelectTarget() //Change l'unité visée a chaque "Tab". Penser a lock le changement d'unité lors de l'aim. 
    {
        //placer le for et le target select une fois avant de Tab 
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _swap = true;
            if(_targetSelect < _nbOfTarget)
            {
                _targetSelect += 1;
            }
            else
            {
                if (_nbOfTarget != 0)
                {
                    _targetSelect = 1;
                }
                else
                {
                    _targetSelect = 0;
                }
            }
            
            TargetLock();
            
        }
    }

    private void TargetLock()
    {
        for (int i = 0; i < _currentTargets.Length; i++)
        {

            //int speed = 5;
            if (i + 1 == _targetSelect)
            {

                if(_nbOfTarget != 0)
                {
                    
                    _actualTarget = _currentTargets[i];
                    _camMouvScript.targetObj = _actualTarget;
                    focused = _actualTarget;
                    CoverSystem();
                    GiveCover(direction);
                    //var targetRotation = Quaternion.LookRotation(_actualTarget.position - transform.position);

                    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
                }
                _distanceTarget = Vector3.Distance(transform.position, _currentTargets[i].transform.position);

            }
            else
            {
             
            }
        }
    }

    public void FirstSelect()
    {
        if (_currentTargets.Length != 0)
        {
            Material _m;
            //_m = _currentTargets[0].GetComponent<Renderer>().material;
            _actualTarget = _currentTargets[0];
            _distanceTarget = Vector3.Distance(transform.position, _currentTargets[0].transform.position);
            //_m.color = Color.red;
        }

    }

    public void Refresh() //stop aiming 
    {
        if (_isAimaing == true)
        {
            _nbOfTarget = 0;
            _targetSelect = 0;
            if (_currentTargets.Length != 0)
            {
                for (int i = 0; i < _currentTargets.Length; i++)
                {
                    //Material _m;
                    //_m = _currentTargets[i].GetComponent<Renderer>().material;
                    //_m.color = Color.blue;
                }
            }

            AimTarget();
            _currentTargets = visibleTargets.ToArray();

            if (_currentTargets.Length != 0)
            {
                _actualTarget = _currentTargets[0];
            }
        }
    }

    public void CoverSystem()
    {
        if (focused != null)
        {
            myPos = transform.position;
            aimPos = focused.transform.position;

            var heading = aimPos - myPos;
            var distance = heading.magnitude;
            direction = heading / distance;
       
            //lr.SetPosition(0, myPos);
            //lr.SetPosition(1, aimPos);
        }
    }

    public void GiveCover(Vector3 direction)
    {
        Player playerSys = focused.GetComponent<Player>();

        RaycastHit hit;
        if (Physics.Raycast(_gd.position, transform.TransformDirection(direction), out hit))
        {
            //Debug.DrawRay(_gd.position, transform.TransformDirection(direction) * hit.distance, Color.black, Mathf.Infinity);


            if(playerSys._triggerCover != null)
            {
                GameObject _triggerzone = playerSys._triggerCover;
                float _posZ = transform.position.z;
                float _posX = transform.position.x;
                float _axis = playerSys._axisRot;




                if(_axis == 1)
                {
                   if (_posX >= _triggerzone.transform.position.x)
                   {
                        _isFlank = true;
                   }
                }
                else if(_axis == 2)
                {
                    if (_posX <= _triggerzone.transform.position.x)
                    {
                        _isFlank = true;
                    }
                }
                else if(_axis == 3)
                {
                    if (_posZ <= _triggerzone.transform.position.z)
                    {
                        _isFlank = true;
                    }
                }
                else if (_axis == 4)
                {
                    if (_posZ >= _triggerzone.transform.position.z)
                    {
                        _isFlank = true;
                    }
                }

                //Quaternion _rot = Quaternion.LookRotation(transform.position, Vector3.up);
                //_triggerzone.transform.rotation = _rot;
                //Debug.Log(_triggerzone.transform.localEulerAngles.y);
                //Debug.DrawRay(_triggerzone.transform.position, transform.position * hit.distance, Color.black, Mathf.Infinity);
            }

            if (hit.transform == focused)
            {
                if (playerSys.covered == true)
                {
                    if(playerSys._bigCover == true)
                    {
                        _actuaTargetCover = 2;
                    }
                    else
                    {
                        _actuaTargetCover = 1;
                    }
                } 
                else
                {
                    _actuaTargetCover = 3;
                }
            }
            else
            {
                _actuaTargetCover = 0;
            }
        }

        //acted = false;
        //detected = false;
    }

    public void OverwatchActivate()
    {
        foreach (Transform Target in visibleTargets)
        {
            navAgent _agent = Target.GetComponent<navAgent>();
            Player _player = gameObject.GetComponent<Player>();
           
            if(_agent._isMoving == true)
            {
                _actualTarget = _agent.transform;
                //_camMouvScript.targetObj = _actualTarget;
                focused = _actualTarget;
                CoverSystem();
                GiveCover(direction);
                Debug.Log("overwatched");
                _baseComp.Overwatch(_player, _player._weapon, this);
                _isOverwatched = false;
            }
        }
    }


    void DrawFieldOfView() // FOV de l'unité calculé en fonction des obstacles en face de lui par triangulisation 
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }


            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) //optimisation en fonction de l'edge de l'obstacle pour obtenir la meillieurs FOV possible 
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}