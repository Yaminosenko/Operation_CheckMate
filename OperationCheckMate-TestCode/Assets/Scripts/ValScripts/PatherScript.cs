using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatherScript : MonoBehaviour
{
    private float _steps;
    public float _maxSteps = 2f;
    public bool firstIns = false;
    /*public void Givepath (GameObject _limitr, float _gSpendClone, float _step)
	{
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_limitr.transform.position);
        
        if (gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance > gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().stoppingDistance)
            _gSpendClone += Time.deltaTime;
        if (_gSpendClone >= (_step * 5))
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObject.transform.position);
            _gSpendClone = 0f;
        }
        Debug.Log("Givepath lancé");
    }*/

    void FixedUpdate ()
    {
        if (gameObject.activeSelf && firstIns == false)
        {
            _steps = 0f;
            firstIns = true;
        }

        if (_steps < _maxSteps)
        {
            _steps += Time.deltaTime;
        }
        else
        {
            _steps = 0f;
            firstIns = false;
            gameObject.SetActive(false);
        }
    }
}
