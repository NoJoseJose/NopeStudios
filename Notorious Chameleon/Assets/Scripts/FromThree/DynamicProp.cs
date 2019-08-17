using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicProp : MonoBehaviour
{
    public DynamicProp prev, next = null;

    /*
    public Vector3 desiredPos = Vector3.zero;
    public Vector3 desiredVel = Vector3.zero;
    public Vector3 desiredAcc = Vector3.zero;
    public Vector3 desiredForce = Vector3.zero;
    public Vector3 desiredTorque = Vector3.zero;
    */
    public float pushForce = 0.0f;
    public Vector3 runningTorque, preTorque, postTorque = Vector3.zero;
    Vector3 runningVel = Vector3.zero;
    Vector3 runningAccel = Vector3.zero;
    public Vector3 runningForce, preForce, postForce = Vector3.zero;
    Rigidbody rb, prb;
    ConfigurableJoint joint, prbj;

    Vector3 centerOfMass = Vector3.zero;
    public Rigidbody[] massPoints;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;
        if(prev != null)
        {
            prb = prev.GetComponent<Rigidbody>();
            prbj = prev.GetComponent<ConfigurableJoint>();
        }
        joint = this.GetComponent<ConfigurableJoint>();
        if (parent != null)
        {
            massPoints = parent.GetComponentsInChildren<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {

        runningTorque = Vector3.zero;

        float massTotal = 0;
        centerOfMass = Vector3.zero;
        foreach (Rigidbody massPoint in massPoints)
        {
            runningTorque -= Vector3.Cross(massPoint.mass * Physics.gravity, transform.TransformPoint(joint.anchor) - massPoint.worldCenterOfMass);
            //centerOfMass += massPoint.worldCenterOfMass * massPoint.mass;
            //massTotal += massPoint.mass;
        }
        
        runningTorque -= Vector3.Cross(Vector3.left * pushForce, transform.TransformPoint(joint.anchor) - rb.position);
        //centerOfMass /= massTotal;

        //runningForce = Physics.gravity * massTotal;
        
        if (next != null)
        {

        }
        //runningTorque = -Vector3.Cross(runningForce, transform.TransformPoint(joint.anchor) - centerOfMass); //torque force on my joint from CoM on all connecteds

        if (prev != null)
        {
            //prev.postTorque = -runningTorque; //opposing torque on my connection to parent
        }

        //runningTorque /= 2.0f;
        rb.AddTorque(runningTorque + postTorque, ForceMode.Force);
        
        if (prb != null)
        {
            prb.AddTorque(-runningTorque, ForceMode.Force);
        }
        
    }
    private void OnDrawGizmos()
    {
        if (joint != null)
        {
            //Gizmos.DrawSphere(centerOfMass, 0.2f);
            Gizmos.DrawRay(transform.TransformPoint(joint.anchor), runningTorque);
        }
    }
}
