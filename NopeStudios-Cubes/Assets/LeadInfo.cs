using UnityEngine;
using System.Collections;

public class LeadInfo : MonoBehaviour {

    public Vector3 averageVelocity = Vector3.zero;
    Queue velocityQueue = new Queue();

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CheckVelocity", 0.05f, 0.05f);
    }
    void CheckVelocity()
    {
        velocityQueue.Enqueue(GetComponent<Rigidbody>().velocity);
        if (velocityQueue.Count > 10)
        {
            Vector3 total = Vector3.zero;
            foreach (Vector3 vect in velocityQueue)
            {
                total += vect;
            }
            averageVelocity = total / velocityQueue.Count;
            velocityQueue.Dequeue();
        }

    }
    // Update is called once per frame
    void Update () {
	
	}
}
