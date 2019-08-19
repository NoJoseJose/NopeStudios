using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoJointSolve : MonoBehaviour
{
    public ConfigurableJoint firstJoint;
    public ConfigurableJoint secondJoint;
    public Transform endPoint; //end effector
    Vector3 helpPoint; //helper
    Quaternion firstRotationalOffset, secondRotationalOffset = Quaternion.identity; //joints could presumably start at some arbitrary offset, because models.
                                                                                    //this doesn't account correctly for the mid and shouldn't be relied on yet
    Quaternion firstRotationalAxis, secondRotationalAxis = Quaternion.identity;
    public Vector3 elbowAxis = new Vector3(1,0,0);
    //public Vector3 pointAxis = new Vector3(0, 1, 0);
    public Vector3 firstGimbalAxis = Vector3.up; 
    public Transform target;
    float dist1, dist2 = 0.1f;

    //visualizer
    Vector3 vv1, vv2, vv3, vv4;
    
    //structure assumptions:
    //firstJoint and secondJoint transforms are at or roughly at the joint rotation
    //the system gets really confused if everything isn't in-line (or capable of) with each other (start joint -> mid joint -> end effector)


    // Start is called before the first frame update
    void Start()
    {
        //calculate the distance we have to work with - I'm assuming this doesn't change. 
        dist1 = Vector3.Distance(firstJoint.transform.position, secondJoint.transform.position);
        dist2 = Vector3.Distance(secondJoint.transform.position, endPoint.position);


        //   secondStartingGimbal = secondJoint.transform.parent.transform.rotation * Vector3.up;
        //rotationalOffset = Quaternion.LookRotation(firstJoint.transform.position - firstJoint.transform.TransformPoint(firstJoint.anchor));

        //  firstRotationalOffset = Quaternion.Inverse(Quaternion.LookRotation(firstJoint.transform.position - endPoint.position, firstJoint.transform.rotation * firstGimbalAxis)); //where to point?
        //secondRotationalOffset = Quaternion.LookRotation(endPoint.position - secondJoint.transform.position);//where is my end effector?


        //firstRotationalOffset = Quaternion.LookRotation(firstJoint.transform.position - endPoint.position);

        
        
        
        firstRotationalOffset = Quaternion.LookRotation(endPoint.position - firstJoint.transform.position, firstJoint.transform.TransformVector(firstJoint.secondaryAxis));
        secondRotationalOffset = Quaternion.LookRotation(endPoint.position - secondJoint.transform.position, secondJoint.transform.TransformVector(secondJoint.secondaryAxis));
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

        //nope
        //firstRotationalAxis = Quaternion.LookRotation(firstJoint.transform.TransformVector(firstJoint.axis), firstJoint.transform.TransformVector(firstJoint.secondaryAxis));
        //   firstRotationalAxis = Quaternion.LookRotation(firstJoint.transform.parent.forward, firstJoint.transform.parent.up);

        float angle, angle2;
        angle = 0.0f;
        angle2 = 0.0f;
        float targetDist = Vector3.Distance(firstJoint.transform.position, target.position);

        if (targetDist < dist1 + dist2)
        {

            //cos A = (b2 + c2 − a2) / 2bc

            angle = Mathf.Acos(((dist1 * dist1) + (targetDist * targetDist) - (dist2 * dist2)) / (2 * dist1 * targetDist)) * Mathf.Rad2Deg;
            //the triangle determines the foldback angle
            angle2 = 180 - Mathf.Acos(((dist2 * dist2) + (dist1 * dist1) - (targetDist * targetDist)) / (2 * dist2 * dist1)) * Mathf.Rad2Deg;


        }



        firstRotationalAxis = firstJoint.transform.parent.rotation;
        secondRotationalAxis = secondJoint.transform.parent.rotation;
        
        //vv2 = firstRotationalAxis * Vector3.forward;
        Quaternion firstRotation = Quaternion.LookRotation(target.position - firstJoint.transform.position);
        Quaternion secondRotation = Quaternion.LookRotation(target.position - secondJoint.transform.position);

        

        firstRotation *= Quaternion.AngleAxis(angle, firstRotation * elbowAxis);
        //secondRotation *= Quaternion.AngleAxis(angle2, secondRotation * -elbowAxis); 
        

        //Quaternion secondRotation = Quaternion.LookRotation(target.position - firstJoint.transform.TransformPoint(firstJoint.anchor));


        firstRotation *= Quaternion.Inverse(firstRotationalOffset);
        secondRotation *= Quaternion.Inverse(secondRotationalOffset);

        //bend to allow close thing
         
        //secondJoint.targetRotation = Quaternion.Inverse(Quaternion.AngleAxis(angle2, firstJoint.transform.parent.rotation * -elbowAxis)); //also mostly works




        firstJoint.targetRotation = Quaternion.Inverse(firstRotation) * firstRotationalAxis;
        secondJoint.targetRotation = Quaternion.Inverse(secondRotation) * secondRotationalAxis;

        

        


        //joint.targetRotation = Quaternion.Inverse(Quaternion.Euler(new Vector3(x, y, z)));
        //Debug.Log("1: " + angle + " 2: " + angle2);


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(secondJoint.transform.position, 0.3f);
        if(vv1 != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(secondJoint.transform.position, vv1 * 1.5f);
        }
        if (vv2 != null)
        {
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(secondJoint.transform.position, vv2 * 1.5f);
        }
    }
}
