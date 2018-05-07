using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobList : MonoBehaviour {

    //Task[] taskList;
    List<Task> taskList = new List<Task>();
    //about the world for now
    public Transform objectLocation;
    public Transform targetLocation;

	// Use this for initialization
	void Start () {

        taskList.Add(this.gameObject.AddComponent<Task>());
        taskList[0].objectLocation = objectLocation;
        taskList[0].endLocation = targetLocation;
        

        //taskList[0] = new Task(objectLocation, targetLocation);

        //fire JobsUpdate off every 2
        InvokeRepeating("JobsUpdate", 2.0f, 2.0f);

    }

    void JobsUpdate()
    {
        if (taskList.Count > 0)
        {
            foreach (Task task in taskList)
            {
                //if a task doesn't have a worker
                if (!task.currentWorker)
                {
                    //just find one off the global gameobject list for now
                    var worker = GameObject.Find("Worker");
                    if (worker)
                    {
                        task.currentWorker = worker.GetComponent<Worker>();
                        //task.currentWorker.moveLocation = task.;
                    }
                }
                
                task.UpdateWorker();

            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
