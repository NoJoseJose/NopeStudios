using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invPBalancer : MonoBehaviour {

    public Rigidbody lever;
    public Rigidbody motor;
    //public float length = 2.0f;
    
    public float motorKp = 1.0f;
    public float motorKd = 1.0f;
    public float motorKi = 1.0f;
    Vector3 posI = Vector3.zero;
    Vector3 lastBalanceError = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        //motor.AddTorque(  9.8f / length * Vector3.Cross(Vector3.up, (lever.transform.up).normalized));
        //motor.AddTorque(9.8f * length * Vector3.Cross(Vector3.up, (lever.transform.up).normalized));
        /*    motor.AddForce(motorStrength * Vector3.Cross(
                    Vector3.Cross(Vector3.up, (lever.transform.up).normalized)
                    , Vector3.up)
                    , ForceMode.Acceleration);
        */
        

        //this is the handed torque vector - it's a measure of our error
        Vector3 balanceDelta = (Vector3.Cross(Vector3.up, (lever.transform.up).normalized) - lastBalanceError) / Time.fixedDeltaTime;
        lastBalanceError = Vector3.Cross(Vector3.up, (lever.transform.up).normalized);
        posI += Vector3.Cross(Vector3.up, (lever.transform.up).normalized) * Time.fixedDeltaTime;

        /*
        motor.AddForce(motorKd * Vector3.Cross(balanceDelta, Vector3.up),ForceMode.Acceleration);
        motor.AddForce(motorKp * Vector3.Cross(Vector3.Cross(Vector3.up, (lever.transform.up).normalized), Vector3.up), ForceMode.Acceleration);
        motor.AddForce(motorKi * Vector3.Cross(posI, Vector3.up), ForceMode.Acceleration);
        */
        
    }

    void OnDrawGizmos()
    {
        if (motor)
        {

            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(motor.position, Vector3.Cross(Vector3.up, (lever.transform.up).normalized));
            Gizmos.color = Color.red;
            Gizmos.DrawRay(motor.position, Vector3.Cross(
                Vector3.Cross(Vector3.up, (lever.transform.up).normalized)
                ,Vector3.up));
            


        }
    }


}
