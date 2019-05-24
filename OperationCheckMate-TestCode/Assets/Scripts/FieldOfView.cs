using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieldOfView : MonoBehaviour {
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [SerializeField] private BaseComp _competanceScript;
    public bool _isActive = false;
    public List<Transform> visibleTargets = new List<Transform>();
    [SerializeField ]private Material _red;
    [SerializeField ]private Material _targetMaterial;
    [SerializeField] private Transform[] _currentTargets;
    private bool _aimTrue = false;

    [HideInInspector]

    public bool _swap = false;
    public bool _isAimaing = false;
    public Transform _actualTarget;
    private int _nbOfTarget = 0;
    private int _targetSelect = 1;
    public float _distanceTarget;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;


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
            if(_isActive == true)
            {
                FindVisibleTargets();
            }

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
    }

    void FindVisibleTargets() //mise en place des targets dans une list 
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void AimTarget() //lance à la fin du déplacement penser a clear a la fin du tour 
    {
        foreach (Transform Target in visibleTargets)
        {
            _nbOfTarget += 1;
        }
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
            for(int i = 0; i < _currentTargets.Length; i++)
            {
                Material _m;
                _m = _currentTargets[i].GetComponent<Renderer>().material;
                if (i+1 == _targetSelect)
                {
                    _m.color = Color.red;
                    _actualTarget = _currentTargets[i];
                    //Debug.Log(_currentTargets[i]);

                    _distanceTarget = Vector3.Distance(transform.position, _currentTargets[i].transform.position);
                }
                else
                {
                    _m.color = Color.blue;
                }
            }
            
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
                    Material _m;
                    _m = _currentTargets[i].GetComponent<Renderer>().material;
                    _m.color = Color.blue;
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


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
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
