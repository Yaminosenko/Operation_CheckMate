using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Linq;
//using TMPro;

public class navAgent : MonoBehaviour
{
	public PlayerCard card;
	public Camera cam1;
	public Camera cam2;
	public Camera cam;
	public UnityEngine.AI.NavMeshAgent agent;
	private GameObject Player;
	public GameObject Ghost;
	public GameObject GhostSpawner;
	// POOLING
	public CurrentUnits _currentScript;

	public GhostPooler ghostPooler;

	//ENDPOOL
	public float ghostRefresh = 0.5f;
	private Transform _cursor;
	
	public bool journeyEnabled = false;
	public GameObject image;
	
	public ThirdPersonCharacter character;
	public Vector3 Pos;
	//public TextMeshProUGUI textName;

	public GameObject teamManager;
	public int myNumber = 0;
	public float maxSteps = 2;
	public float _step;
	private float _spend;
	private float _gSpend;
	private float _gSpendClone;

	public int numObjects = 36;
	public GameObject ghostLimiterPrefab;
	public bool ghosted = false;
	public Vector3 startPos;
	public float moveRange = 10f;
	public Animator anim;
	public bool _canClic = false;
	private Color cliquableColor;
	public bool _alreadyMouv = false;

	void Start ()
	{
		// POOLING

		ghostPooler = GhostPooler.Instance;

		//ENDPOOL
		teamManager = GameObject.Find("TeamManager");
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		Player = gameObject;
		//textName.text = card.name;
		agent.speed = card.playerSpeed;
		_step = maxSteps / card.playerSpeed;
		startPos = transform.position;
		anim = gameObject.GetComponent<Animator>();
		//cam = GameObject.FindWithTag("Camera").GetComponent<Camera>();
		cliquableColor = Ghost.GetComponent<ghostPathway>()._color2;

		Invoke("UntagMe", 1f);

		GhostsPrewarm();
	}

	public void GhostsPrewarm ()
	{
		prewarming(0);
		prewarming(0);
		prewarming(0);
		prewarming(0);
		prewarming(0);
	}
	

	public void prewarming(float moveRange)
	{
		Vector3 center = new Vector3(0f, -100f, 0f);
		for (int i = 0; i < numObjects; i++)
		{
			Vector3 pos = RandomCircle(center, moveRange, i * (360/numObjects));
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, center-pos);
			
			ghostPooler.SpawnFromPool("Ghost", center, Quaternion.identity).GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(ghostPooler.SpawnFromPool("GhostSpawner", pos, rot).transform.position);
		}
	}

	  public void UntagMe ()
	  {
        if (GameObject.FindWithTag("Camera") != null)
            GameObject.FindWithTag("Camera").tag = "alreadyNamed";
    }

	void Update ()
	{
		Movement();

        //if (cam1.enabled == true)
        //{
        //	cam = cam1;
        //}
        //else
        //{
        //	cam = cam2;
        //}
        cam = cam1;
	}

	void Movement()
	{
		if (Vector3.Distance(startPos, transform.position) >= moveRange)
		{
			if (agent.enabled == true && agent != null)
				agent.SetDestination(transform.position);
			if (character.enabled == true)
				character.Move(Vector3.zero, false, false);
			_spend = 0f;
		}

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (journeyEnabled == true)
		{
			if (ghosted == false)
			{
				splitGhost(moveRange/5);
				splitGhost(moveRange*2/5);
				splitGhost(moveRange*3/5);
				splitGhost(moveRange*4/5);
				splitGhost(moveRange);

				resetColorsBlue();
				Invoke ("canClic", _step + 0.5f);

				Freezaaa();
				Invoke("unFreezing", _step);
				ghosted = true;
				startPos = transform.position;
			}
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.isTrigger && hit.collider.gameObject.tag == "grid")
				{
					if (hit.collider.GetComponent<Renderer>().material != null &&
						hit.collider.GetComponent<Renderer>().material.GetColor("_TintColor") != null)
					{
						if (hit.collider.GetComponent<Renderer>().material.GetColor("_TintColor") == cliquableColor)
						{
							hit.collider.GetComponent<TriggerChangeColor>().insideZone = true;
                        
							if (Input.GetMouseButtonDown(1) && _canClic == true)
							{
								Invoke("endMyTurn", _step);
								Pos = new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z);
								agent.SetDestination(Pos);
							}
						}
						else
							if (hit.collider.gameObject.tag == "grid")
							{
								hit.collider.GetComponent<TriggerChangeColor>().insideZone = false;
							}
					}
				}
			}
		}
		else
		{
            image.SetActive(false);
            _canClic = false;
			agent.enabled = false;
			//if (character != null)
			//character.enabled = false;
		}
		if (agent.enabled == true && agent != null)
		{
			if (agent.remainingDistance > agent.stoppingDistance - 0.5F)
			{
				if (character.enabled == false)
				{
					character.enabled = true;
					character.Move(agent.desiredVelocity, false, false);
				}
				else
				character.Move(agent.desiredVelocity, false, false);
				//gameObject.GetComponent<Rigidbody>().isKinematic = false;
				
				_spend += Time.deltaTime;
			}
			else
				if (character.enabled == true)
				{
					character.enabled = true;
					anim.SetFloat("Forward", 0f);
				}
			if (character.enabled == true)
			{
				character.Move(Vector3.zero, false, false);
			}
		}
		else
		{
			if (character.enabled == true)
			{
				character.enabled = true;
				anim.SetFloat("Forward", 0f);
			}
			//character.Move(Vector3.zero, false, false);
			_spend = 0f;
			//gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	public void endMyTurn ()
	{
		Freezaaa();
		resetColorsBlack();
        _alreadyMouv = true;
        if (_alreadyMouv == false)
        {
            ghosted = false;
        }
        //ghosted = false;
        //_currentScript.ChangeUnitsEvrywhere();
        //_currentScript.EndOfThisUnitTurn();
        //teamManager.GetComponent<TeamManager>().soldierTurn++;
        journeyEnabled = false;
	}

    public void ChangeUnits()
    {
        Freezaaa();
        resetColorsBlack();
        if(_alreadyMouv == false)
        {
            Debug.Log("oui");
            ghosted = false;
        }

      
        journeyEnabled = false;
    }

	public void unFreezing ()
	{
		UnFreezaaa();
		image.SetActive(true);
		agent.enabled = true;
		character.enabled = true;
	}

	public void resetColorsBlack ()
	{
		GameObject[] toReset = GameObject.FindGameObjectsWithTag("grid");
		foreach(GameObject tR in toReset)
		{
			if (tR.GetComponent<Renderer>().material.GetColor("_TintColor") != tR.GetComponent<TriggerChangeColor>()._color1)
				{
					tR.GetComponent<TriggerChangeColor>().BlackReset();
				}
		}
	}

	public void resetColorsBlue ()
	{
		GameObject[] toReset = GameObject.FindGameObjectsWithTag("grid");
		foreach(GameObject tR in toReset)
		{
		if (tR.GetComponent<Renderer>().material.GetColor("_TintColor") != tR.GetComponent<TriggerChangeColor>()._color1 && 
			tR.GetComponent<Renderer>().material.GetColor("_TintColor") != tR.GetComponent<TriggerChangeColor>()._color2)
			{
				tR.GetComponent<TriggerChangeColor>().BlueReset();
			}
		}
	}

	public void Freezaaa ()
	{
		Rigidbody rig = gameObject.GetComponent<Rigidbody>();
		rig.constraints =  RigidbodyConstraints.FreezeAll;
	}

	public void UnFreezaaa ()
	{
		Rigidbody rig = gameObject.GetComponent<Rigidbody>();
		rig.constraints = RigidbodyConstraints.None;
		rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	public void splitGhost(float moveRange)
	{
		Vector3 center = transform.position;
		for (int i = 0; i < numObjects; i++)
		{
			Vector3 pos = RandomCircle(center, moveRange, i * (360/numObjects));
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, center-pos);
			
			ghostPooler.SpawnFromPool("Ghost", transform.position, Quaternion.identity).GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(ghostPooler.SpawnFromPool("GhostSpawner", pos, rot).transform.position);
		}
	}

	public void canClic ()
	{
		_canClic = true;
	}

	Vector3 RandomCircle (Vector3 center, float radius, float _angular)
	{
		float ang = _angular;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		return pos;
	}
}
