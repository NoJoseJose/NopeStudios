using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phystest : MonoBehaviour {

    public Transform targetbody;
    public Transform target;
    public Joint[] joints;
    public float weight;
    public float damper;
    public float maxTorque;

    Vector3[] lastError;

    Vector3 lasttorque;
    Vector3 lastPos;

	// Use this for initialization
	void Start () {

        lastError = new Vector3[joints.Length];
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        int loopCount = 0;
        
        Vector3 targetForce = target.position - targetbody.position;
        foreach (Joint joint in joints)
        {
            if(lastError[loopCount] == null)
            {
                lastError[loopCount] = Vector3.zero;
            }
            


            Rigidbody fore = joint.gameObject.GetComponent<Rigidbody>();
            Rigidbody rear = joint.connectedBody;

            fore.maxAngularVelocity = Mathf.Infinity;
            rear.maxAngularVelocity = Mathf.Infinity;

            Vector3 addTorque = Vector3.Cross(fore.transform.TransformPoint(fore.centerOfMass) - fore.transform.TransformPoint(joint.anchor), targetForce);

            

            addTorque *= weight;
            if(addTorque.magnitude > maxTorque)
            {
                addTorque = addTorque.normalized * maxTorque;
            }
            Vector3 dampning = (addTorque - lastError[loopCount]) / Time.fixedDeltaTime;
            lastError[loopCount] = addTorque;

            fore.AddTorque(addTorque + (dampning * damper), ForceMode.Force);
            rear.AddTorque(-(addTorque + (dampning * damper)), ForceMode.Force);

            lasttorque = addTorque;
            lastPos = fore.transform.TransformPoint(fore.centerOfMass);
            loopCount++;
        }
		
	}
    void OnDrawGizmos()
    {
        //Gizmos.DrawLine(target.position, arm1.position);
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(lastPos, lasttorque);
        Gizmos.DrawLine(target.position, targetbody.position);

    }
}
