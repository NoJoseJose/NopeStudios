using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {

    //Vector3 [] waypoints;
    public Transform endLocation = null;
    public Transform objectLocation = null;
    public Worker currentWorker = null;
    public float closeEnough = 2.0f;
    //public TaskType job;

    //public enum TaskType { Delivery, Pickup, Destination, Dropoff };
    /*
    public Task(Transform objLoc, Transform destLoc)
    {
        objectLocation = objLoc;
        endLocation = destLoc;
        //job = 
 
        //SetWorker(newWorker);
    }
    public Task(Transform destLoc)
    {
        endLocation = destLoc;
        //SetWorker(newWorker);
    }
    */

    public void SetWorker(Worker worker)
    {
        currentWorker = worker;

        //updateWorker();
    }
    public void UpdateWorker()
    {
        if(!currentWorker.moveLocation)
        {
            currentWorker.moveLocation = endLocation;
        }

        if (!currentWorker.hasObject && (currentWorker.transform.position - currentWorker.moveLocation.position).magnitude < closeEnough )
        {
            currentWorker.hasObject = true;
            
        }

        if (currentWorker.hasObject)
        {
            currentWorker.moveLocation = endLocation;
        }
        else if (!currentWorker.hasObject && objectLocation)
        {
            currentWorker.moveLocation = objectLocation;
        }
        else
        {
            //there's no object to have?
            currentWorker.moveLocation = endLocation;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
