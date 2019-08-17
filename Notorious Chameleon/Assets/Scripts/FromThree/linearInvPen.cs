using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearInvPen : MonoBehaviour {

    public Rigidbody lever;
    public Rigidbody motor;

    
    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D, seekDifference = Vector3.zero;
    
    public Vector3 desiredLeverOrientation; //should be forward-flat axis for the spring pivot type

    public Transform massPoint;
    public ConfigurableJoint joint;
    Quaternion startRotation;


   // Vector3 point;



    // Use this for initialization
    void Start()
    {
        if (motor)
        {
            motor.maxAngularVelocity = Mathf.Infinity;
        }
        if (lever)
        {
            lever.maxAngularVelocity = Mathf.Infinity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {


        

        if(joint)
        {

            

            //z = 1 (so, motor's forward, but don't tilt with the motor, duh) the joint orients itself to a straightupward pivot.
            joint.targetRotation = Quaternion.Inverse(motor.transform.rotation) * Quaternion.LookRotation(desiredLeverOrientation, Vector3.up);

            if (massPoint)
            {
                /*
                 * I don't have any other word for this - a cross product will usually get you a correct sign and axis for a difference of rotation.
                 * Problem is, that's in torque directions and these stupid joints don't really let me do that
                 * So X is really the Z direction and vice versa
                 * */
                //Vector3 torqueVector = Vector3.Cross(Vector3.up, massPoint.position - motor.position);


                //Vector3 massPoint.position - motor.position

                //joint.targetRotation *= Quaternion.FromToRotation(massPoint.position - motor.position, motor.transform.up);
                joint.targetRotation *= Quaternion.FromToRotation(massPoint.position - motor.position, Vector3.up);
                //joint.targetRotation *= Quaternion.Inverse(Quaternion.FromToRotation(massPoint.position - motor.position, motor.transform.up));

                // z is in x, x in z
                //joint.targetRotation *= massPoint.position;

            }

            //forward feet don't do left/right leans - fix to upwards

            //joint.targetRotation *= Quaternion.AngleAxis(-90.0f, motor.transform.right);


        }
        
        
        else if (lever)
        {
            P = Vector3.Cross(lever.transform.up, desiredLeverOrientation);
            I += P * Time.fixedDeltaTime;
            D = (P - lastError) / Time.fixedDeltaTime;
            lastError = P;
            seekDifference = P * Kp + I * Ki + D * Kd;
            



            /*
            ConfigurableJoint localJoint = GetComponent<ConfigurableJoint>();
            float motorDistance = (motor.transform.position - motor.transform.TransformPoint(localJoint.anchor)).magnitude;
            float leverDistance = (lever.position - motor.transform.TransformPoint(localJoint.anchor)).magnitude;
            */
            /*
            Vector3 centerOfMass = Vector3.zero;

            centerOfMass += motor.position * motor.mass;
            centerOfMass += lever.position * lever.mass;
            centerOfMass /= (lever.mass + motor.mass);

            
            
            float motorDistance = (motor.position - centerOfMass).magnitude;
            float leverDistance = (lever.position - centerOfMass).magnitude;
            */
            //print((-seekDifference * motorDistance * motor.mass).magnitude + " . " + (seekDifference * leverDistance * lever.mass).magnitude);
            
            
            

            motor.AddTorque(-seekDifference, ForceMode.Force);
            lever.AddTorque(seekDifference, ForceMode.Force);

            /*
            motor.AddTorque(-seekDifference, ForceMode.Acceleration);
            lever.AddTorque(seekDifference, ForceMode.Acceleration);
            */
        }
        

    }

    void OnDrawGizmos()
    {
        if (joint)
        {


            Gizmos.color = Color.red;
            Gizmos.DrawRay(motor.transform.position, desiredLeverOrientation * 1.0f);
            
          //  Gizmos.DrawRay(joint.transform.position, joint.secondaryAxis * 1.0f);
            Gizmos.color = Color.magenta;
            if (massPoint)
            {
                Gizmos.DrawLine(massPoint.position, joint.transform.position);
            }
            Gizmos.color = Color.green;
            Gizmos.DrawRay(motor.position, motor.transform.forward);


        }
        if(lever)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(motor.transform.position, -seekDifference * 0.001f);
            Gizmos.DrawRay(lever.transform.position, seekDifference  * 0.001f);
            
        }
    }


}
