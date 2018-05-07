using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour {

    public Transform moveLocation;
    NavMeshAgent navAgent;
    LineRenderer navLine;
    public bool hasObject = false;
	// Use this for initialization
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);
        navLine = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if (navAgent && moveLocation)
        {

            navAgent.SetDestination(moveLocation.position);
            //navAgent.hasPath
            navLine.SetPosition(0, transform.position);
            navLine.SetPosition(1, navAgent.steeringTarget);
            //navAgent.steeringTarget
        

        }
        
        
	}

    void OnDrawGizmos()
    {
        
    }
}
