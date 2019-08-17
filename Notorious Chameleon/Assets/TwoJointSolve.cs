using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoJointSolve : MonoBehaviour
{
    public ConfigurableJoint firstJoint;
    public ConfigurableJoint secondJoint;
    public Transform endPoint; //end effector
    Vector3 helpPoint; //helper
    Quaternion firstRotationalOffset, secondRotationalOffset = Quaternion.identity; //joints start at some arbitrary offset, because models
    public Vector3 elbowAxis = new Vector3(1,0,0);
    public Vector3 firstGimbalAxis, secondGimbalAxis = Vector3.up; 
    public Transform target;
    float dist1, dist2 = 0.1f;

    //structure assumptions:
    //firstJoint and secondJoint transforms are at or roughly at the joint rotation



    // Start is called before the first frame update
    void Start()
    {
        //calculate the distance we have to work with - I'm assuming this doesn't change. 
        dist1 = Vector3.Distance(firstJoint.transform.position, secondJoint.transform.position);
        dist2 = Vector3.Distance(secondJoint.transform.position, endPoint.position);

        

        //rotationalOffset = Quaternion.LookRotation(firstJoint.transform.position - firstJoint.transform.TransformPoint(firstJoint.anchor));
        firstRotationalOffset = Quaternion.LookRotation(secondJoint.transform.position - firstJoint.transform.position); //where is my next joint?
        secondRotationalOffset = Quaternion.LookRotation(endPoint.position - secondJoint.transform.position);//where is my end effector?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //elbowaxis and gimbalaxis need a redefinitions

    void FixedUpdate()
    {
        //part 1: effective IK.
        //if the target is far enough away, both joints fully extend pointing at target. That's the easy part.
        //otherwise you need to find two angles. Or at least one. The elbow joint orientation determines primarily length; the shoulder joint determines distance and direction
        //I'm also trying to make the restriction the elbow can only rotate on one axis.

        Quaternion firstRotation = Quaternion.LookRotation(firstJoint.transform.TransformPoint(firstJoint.anchor) - target.position, firstJoint.transform.parent.transform.rotation * firstGimbalAxis) * firstRotationalOffset;

        //if we wanted it to rotate in any way, which we don't
        //Quaternion secondRotation = Quaternion.LookRotation(secondJoint.transform.TransformPoint(secondJoint.anchor) - target.position, secondGimbalAxis) * secondRotationalOffset;

        //works, technically
        Quaternion secondRotation = Quaternion.LookRotation(secondJoint.transform.TransformPoint(secondJoint.anchor) - target.position, secondJoint.transform.parent.transform.rotation * secondGimbalAxis) * secondRotationalOffset;

        float angle, angle2 = 0.0f;
        float targetDist = Vector3.Distance(firstJoint.transform.position, target.position);
        if(targetDist < dist1 + dist2)
        {

            //cos A = (b2 + c2 − a2) / 2bc


            angle = Mathf.Acos(((dist1 * dist1) + (targetDist * targetDist) - (dist2 * dist2)) / (2 * dist1 * targetDist)) * Mathf.Rad2Deg;
            firstRotation *= Quaternion.AngleAxis(angle, firstJoint.transform.parent.transform.rotation * elbowAxis); 
            //angle2 = Mathf.Acos(((dist2 * dist2) + (dist1 * dist1) - (targetDist * targetDist)) / (2 * dist2 * dist1)) * Mathf.Rad2Deg;
            //secondRotation *= Quaternion.AngleAxis(angle2, elbowAxis);
            //Debug.Log("1: " + angle + " 2: " + angle2);

            //currently leaving second joint to find target on it's own. The fold already occured.

        }
        firstJoint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(firstJoint.transform.parent.rotation) * firstRotation);
        //secondJoint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(secondJoint.transform.parent.rotation) * secondRotation);

        
        secondJoint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(secondJoint.transform.parent.rotation) * secondRotation);
    }
}
