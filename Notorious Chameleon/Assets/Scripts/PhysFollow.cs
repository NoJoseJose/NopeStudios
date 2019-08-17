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

    //public float Kp = 0.05f;
    public GameObject target;

    //Quaternion P;

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

        var rotation = Quaternion.LookRotation(transform.TransformPoint(joint.anchor) - target.transform.position, gimbalAxis) * rotationalOffset;

        //this P is wrong, so it's currently zeroed out - fix this
        //fix this
        //fix this
        //fix this
        //fix this
        //maybe change the target line instead
        //P = Quaternion.LookRotation(transform.TransformPoint(joint.anchor) - transform.position, gimbalAxis) * Quaternion.Inverse(rotation);

        
        //rotation = Quaternion.Slerp(rotation, P, Kp);
        //rotation *= Quaternion.SlerpUnclamped(P, Quaternion.Inverse(lastP), Kd/Time.fixedDeltaTime);


        //lastP = P;
        //convert to whatever nonsense space a joint uses
        joint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(transform.parent.rotation) * rotation);

    }

    void OnDrawGizmos()
    {
        if (joint)
        {
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.TransformPoint(joint.anchor), Quaternion.LookRotation(target.transform.position - transform.TransformPoint(joint.anchor)) * Vector3.forward);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.TransformPoint(joint.anchor), Quaternion.LookRotation(transform.position - transform.TransformPoint(joint.anchor)) * Vector3.forward);

        }
    }
}
