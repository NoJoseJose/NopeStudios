using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysFollow : MonoBehaviour
{
    ConfigurableJoint joint; //the hinge
    Rigidbody lever; //the body
    //Transform desired; //desired orientation
    Quaternion rotationalOffset; //springs start at some arbitrary offset, because models
    public Vector3 gimbalAxis = Vector3.up;
    
    //assumption: The joint points at the lever.

    public float Kp, Kd, Ki = 1;
    public GameObject target;



    //Vector3 axis;

    void Awake()
    {
        //joint = GetComponent<ConfigurableJoint>();
        //rotationalOffset = joint.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        joint = GetComponent<ConfigurableJoint>();
        lever = GetComponent<Rigidbody>();

        rotationalOffset = Quaternion.LookRotation(transform.position - transform.TransformPoint(joint.anchor));
        
    }


    void FixedUpdate()
    {
        /*
        var right = joint.axis;
        var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
        var up = Vector3.Cross(forward, right).normalized;
        Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);
        */
        joint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(transform.parent.rotation) * Quaternion.LookRotation(transform.TransformPoint(joint.anchor) - target.transform.position, gimbalAxis) * rotationalOffset);

        Debug.Log("target: " + joint.targetRotation + " actual: " + transform.rotation);

        //joint.targetRotation *= Quaternion.Slerp(transform.rotation, joint.targetRotation)
       
    }

    void OnDrawGizmos()
    {
        if (joint)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.TransformPoint(joint.anchor), target.transform.position);
            Gizmos.color = Color.green;


        }
    }
}
