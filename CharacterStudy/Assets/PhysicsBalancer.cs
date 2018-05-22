using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBalancer : MonoBehaviour {


    public float Kp = 0.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    public float maxRightingForce = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D = Vector3.zero;
    Vector3 totalError;

    Vector3 desiredUp = Vector3.up;

    public Rigidbody lever;
    public Rigidbody motor;

    Vector3 lastMotorVelocity = Vector3.zero;
    Vector3 motorAcceleration = Vector3.zero;

    Vector3 centerOfMass = Vector3.zero;
    Rigidbody[] massPoints;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        float massTotal = 0;
        var lastCoM = centerOfMass;
        centerOfMass = Vector3.zero;

        massPoints = this.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody massPoint in massPoints)
        {
            centerOfMass += massPoint.worldCenterOfMass * massPoint.mass;
            massTotal += massPoint.mass;
        }
        centerOfMass /= massTotal;
        
    }

    void FixedUpdate()
    {
        //Vector3.Cross(lever.angularVelocity, Vector3.up)
        //P = (lever.angularVelocity - lastAngular) / Time.fixedDeltaTime;
        //lastAngular = lever.angularVelocity;
        //P = lever.angularVelocity;
        motorAcceleration = (motor.velocity - lastMotorVelocity) / Time.fixedDeltaTime;
        lastMotorVelocity = motor.velocity;

        desiredUp = (Vector3.up * 9.8f + motorAcceleration).normalized;


        P = Vector3.Cross(lever.transform.up.normalized, desiredUp);
        //P = lever.angularVelocity;
        I += P * Time.fixedDeltaTime;
        D = (P - lastError) / Time.fixedDeltaTime;
        lastError = P;
        totalError = P * Kp + I * Ki + D * Kd;

        totalError = totalError.normalized * Mathf.Clamp(totalError.magnitude, 0, maxRightingForce);

        //lever.AddForce(Vector3.Cross(totalError, desiredUp));
        //motor.AddForce(Vector3.Cross(-totalError, desiredUp));




    }

    void OnDrawGizmos()
    {
        if (lever)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawRay(lever.position, totalError);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(motor.position, motorAcceleration);
            //Gizmos.DrawRay(lever.position, lever.angularVelocity);
            Gizmos.color = Color.blue;
            //Gizmos.DrawRay(lever.position, Vector3.Cross(lever.transform.up, Vector3.up));
            Gizmos.DrawRay(motor.position, Vector3.up * 2.0f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(motor.position, desiredUp * 2.0f);
        }
    }
}
