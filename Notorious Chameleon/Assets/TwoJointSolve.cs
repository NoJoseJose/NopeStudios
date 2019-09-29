using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoJointSolve : MonoBehaviour
{
    public ConfigurableJoint firstJoint;
    public ConfigurableJoint secondJoint;
    public Transform endPoint; //end effector
    //Vector3 helpPoint; //helper
    Quaternion firstRotationalOffset, secondRotationalOffset = Quaternion.identity; //joints could presumably start at some arbitrary offset, because models.
                                                                                    //this doesn't account correctly for the mid and shouldn't be relied on yet
    Quaternion firstRotationalAxis, secondRotationalAxis = Quaternion.identity;
    public Vector3 elbowAxis = new Vector3(1,0,0);
    //public Vector3 pointAxis = new Vector3(0, 1, 0);
    public Vector3 firstGimbalAxis = Vector3.up;
    public Vector3 secondGimbalAxis = Vector3.up;
    //public Vector3 
    public Transform target;
 //   public Transform inverseTarget;
    float dist1, dist2 = 0.1f;

 //   public bool inverseAnchor = false; //inverse mode should make the joint calc as if it's anchored on the other side - moving torso with the feet, for instance
                                        //you should probably move the target if you're doing this

    //visualizer
    Vector3 vv1, vv2, vv3, vv4;
    Vector3 vv1u, vv2u, vv3u, vv4u;
    //structure assumptions:
    //firstJoint and secondJoint transforms are at or roughly at the joint rotation
    //the system gets really confused if everything isn't in-line (or capable of) with each other (start joint -> mid joint -> end effector)


    // Start is called before the first frame update
    void Start()
    {
        //calculate the distance we have to work with - I'm assuming this doesn't change. (maybe I shouldn't)
        dist1 = Vector3.Distance(firstJoint.transform.position, secondJoint.transform.position);
        dist2 = Vector3.Distance(secondJoint.transform.position, endPoint.position);

        //firstRotationalOffset = Quaternion.LookRotation(endPoint.position - firstJoint.transform.position, firstJoint.transform.TransformVector(firstJoint.secondaryAxis));
        //secondRotationalOffset = Quaternion.LookRotation(endPoint.position - secondJoint.transform.position, secondJoint.transform.TransformVector(secondJoint.secondaryAxis));
        firstRotationalOffset = Quaternion.LookRotation(endPoint.position - firstJoint.transform.position, firstRotationalAxis * firstGimbalAxis);
        secondRotationalOffset = Quaternion.LookRotation(endPoint.position - secondJoint.transform.position, secondRotationalAxis * secondGimbalAxis);
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
        //otherwise you need to find two angles. Or maybe just one. The elbow joint orientation determines primarily length; the shoulder joint determines direction
        //I'm also trying to make the restriction that the elbow can only rotate on one axis.


        float angle, angle2;
        angle = 0.0f;
        angle2 = 0.0f;
        float targetDist;
    /*    if (inverseAnchor)
        {
            //inv anchor to target point
            targetDist = Vector3.Distance(inverseTarget.position, target.position);
        }
        else
        */
        {
            //start of joint to target point
            targetDist = Vector3.Distance(firstJoint.transform.position, target.position);
        }

        if (targetDist < dist1 + dist2)
        {

            //cos A = (b2 + c2 − a2) / 2bc

            angle = Mathf.Acos(((dist1 * dist1) + (targetDist * targetDist) - (dist2 * dist2)) / (2 * dist1 * targetDist)) * Mathf.Rad2Deg;
            //the triangle determines the foldback angle
            angle2 = 180 - Mathf.Acos(((dist2 * dist2) + (dist1 * dist1) - (targetDist * targetDist)) / (2 * dist2 * dist1)) * Mathf.Rad2Deg;

            
        }

        //basis from the parent
        firstRotationalAxis = firstJoint.transform.parent.rotation;
        secondRotationalAxis = secondJoint.transform.parent.rotation;

        Quaternion firstRotation;
        Quaternion secondRotation;
        /*
        if (inverseAnchor)
        {
            //this is strange - get the correct angles for where we want to be
            firstRotation = Quaternion.LookRotation(target.position - inverseTarget.position, firstRotationalAxis * firstGimbalAxis);
            
            secondRotation = firstRotation;
            //secondRotation = Quaternion.LookRotation(target.position - secondJoint.transform.position, secondRotationalAxis * secondGimbalAxis);
            //the second is more or less neutral?
        }
        else
        */
        {
            //point a rotation along the limb segment.
            firstRotation = Quaternion.LookRotation(target.position - firstJoint.transform.position, firstRotationalAxis * firstGimbalAxis);
            secondRotation = Quaternion.LookRotation(target.position - firstJoint.transform.position, secondRotationalAxis * secondGimbalAxis);
            //secondRotation = firstRotation;
        }


        /*
        if (inverseAnchor)
        {
            secondRotation *= Quaternion.AngleAxis(angle2 - angle,  firstRotation * -elbowAxis);
        }
 
        else
    */
        Debug.Log(angle + ", " + angle2);


        //do a bend. Currently leave the second half to find it's own way.
        firstRotation *= Quaternion.AngleAxis(angle, firstJoint.transform.rotation * elbowAxis); //why this is flipping?
        secondRotation *= Quaternion.AngleAxis(angle - angle2, firstJoint.transform.rotation * elbowAxis);


  
        //apply the starting limb conditions
        firstRotation *= Quaternion.Inverse(firstRotationalOffset);
        secondRotation *= Quaternion.Inverse(secondRotationalOffset);

        

        //apply the rotations needed to get where desired and the parent start
        firstJoint.targetRotation = Quaternion.Inverse(firstRotation) * firstRotationalAxis;
        secondJoint.targetRotation = Quaternion.Inverse(secondRotation) * secondRotationalAxis;

        vv1 = firstRotation * Vector3.forward * dist1;
        vv1u = firstRotationalAxis * firstGimbalAxis;
        vv2 = secondRotation * Vector3.forward * dist2;
        vv2u = secondRotationalAxis * secondGimbalAxis;
        vv3 = firstRotation * elbowAxis;

        //joint.targetRotation = Quaternion.Inverse(Quaternion.Euler(new Vector3(x, y, z)));
        //Debug.Log("1: " + angle + " 2: " + angle2);


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(secondJoint.transform.position, 0.3f);
        if(vv1 != null && vv1u != null)
        {

            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(firstJoint.transform.position, vv1);
     //       Gizmos.DrawRay(inverseTarget.position, vv1);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(firstJoint.transform.position, vv1u);
     //       Gizmos.DrawRay(inverseTarget.position, vv1u);
            
        }
        if (vv2 != null && vv2u != null && vv3 != null)
        {
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(secondJoint.transform.position, vv2);
     //       Gizmos.DrawRay(inverseTarget.position + vv1, vv2);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(secondJoint.transform.position, vv2u);
    //        Gizmos.DrawRay(inverseTarget.position + vv1, vv2u);

            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(secondJoint.transform.position, vv3);
    //        Gizmos.DrawRay(inverseTarget.position + vv1, vv3);
        }
    }
}
