using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueJoint : MonoBehaviour {

    public Rigidbody lever;
    public Rigidbody motor;
    
    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D, seekDifference = Vector3.zero;

    public Vector3 desiredLeverOrientation = new Vector3(0,1,0);

    public Transform desiredPoint;
   // public ConfigurableJoint joint;
    public float maxTorque = 0;
    public Transform desiredCenterObject;
    public ReactionController controller;

    public bool colliderDrive = false;
    public bool virtualMotor = false;
    
    // Use this for initialization
    void Start () {
        if (!virtualMotor)
        {
            motor.maxAngularVelocity = Mathf.Infinity;
        }
        lever.maxAngularVelocity = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void Update () {

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        
    }
    */
    void FixedUpdate()
    {
        if (lever)
        {
            if (controller)
            {
                desiredLeverOrientation = controller.resultTilt;
            }
            else //up is just up
            {
                desiredLeverOrientation = Vector3.up;
            }
            //P = Vector3.Cross(desiredPoint.position - (motor.transform.TransformPoint(joint.anchor)), desiredLeverOrientation);
            if (desiredCenterObject)
            {
                P = Vector3.Cross(desiredPoint.position - desiredCenterObject.position, desiredLeverOrientation);
            }
            else
            {
                P = Vector3.Cross(lever.transform.up, desiredLeverOrientation);
            }

            I += P * Time.fixedDeltaTime;
            D = (P - lastError) / Time.fixedDeltaTime;
            lastError = P;
            seekDifference = P * Kp + I * Ki + D * Kd;

            if (seekDifference.magnitude > maxTorque)
            {
                seekDifference = seekDifference.normalized * maxTorque;
            }
            if (colliderDrive)
            {

                motor.AddForceAtPosition(Quaternion.AngleAxis(-90.0f, motor.transform.up) * seekDifference, motor.transform.position, ForceMode.Force);
            }
            else if(!virtualMotor)
            {
                motor.AddTorque(-seekDifference, ForceMode.Force);
                lever.AddTorque(seekDifference, ForceMode.Force);
            }
            else
            {
                lever.AddTorque(seekDifference, ForceMode.Force);
            }
            

            // print(motor.angularVelocity.magnitude);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!colliderDrive)
        {
            Gizmos.DrawRay(lever.position, seekDifference);
            if (!virtualMotor)
            {
                Gizmos.DrawRay(motor.position, -seekDifference);
            }
            Gizmos.color = Color.green;
            if (desiredPoint && desiredCenterObject)
            {

                Gizmos.DrawRay(lever.position, desiredPoint.position - desiredCenterObject.position);
            }
        }
        else
        {
            
            Gizmos.DrawRay(motor.position, Quaternion.AngleAxis(-90.0f, motor.transform.up) * -seekDifference);
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(lever.position, P);

    }
}
