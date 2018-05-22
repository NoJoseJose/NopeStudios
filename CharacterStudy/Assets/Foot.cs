using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour {

    public enum FootState { Placed, UpMoving, DownMoving, Invalid }
    //a foot is either where it wants to be, moving somewhere, or waiting till it can move

    public Vector3 desiredLeverOrientation = new Vector3(0, 1, 0);
    public Vector3 desiredCenterPosition;
    public GameObject center;


    public FootState state;

    public float maxTorque = 10.0f;

    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D, seekDifference = Vector3.zero;
    public float angularTolerance = 1.0f;
    public float maxStepTime = 1.0f;
    float stepTime = 0.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        P = Vector3.Cross(desiredCenterPosition - transform.position, desiredLeverOrientation);
        I += P * Time.fixedDeltaTime;
        D = (P - lastError) / Time.fixedDeltaTime;
        lastError = P;
        seekDifference = P * Kp + I * Ki + D * Kd;

        if (seekDifference.magnitude > maxTorque)
        {
            seekDifference = seekDifference.normalized * maxTorque;
        }
        

        float angleError = Vector3.Dot((desiredCenterPosition - transform.position).normalized, (desiredLeverOrientation).normalized);

        

        //if out of bounds
        if (angleError < angularTolerance && state == FootState.Placed)
        {
            state = FootState.Invalid;
            //fer newu
            StartStep();
        }
        if (state == FootState.UpMoving)
        {
            stepTime += Time.fixedDeltaTime;
        }
        if(state == FootState.UpMoving && stepTime >= maxStepTime)
        {
            state = FootState.DownMoving;
            stepTime = 0;
        }
        
        switch(state)
        {
            case FootState.Invalid:
                //lowered position
                this.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 1.5f, 0);
                break;
            case FootState.Placed:
                //lowered position
                this.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 1.5f, 0);
                center.GetComponent<Rigidbody>().AddTorque(-seekDifference, ForceMode.Force);
                this.GetComponent<Rigidbody>().AddTorque(seekDifference, ForceMode.Force);
                break;
            case FootState.UpMoving:
                //raised position
                this.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 0.5f, 0);
                center.GetComponent<Rigidbody>().AddTorque(-seekDifference, ForceMode.Force);
                this.GetComponent<Rigidbody>().AddTorque(seekDifference, ForceMode.Force);

                break;
            case FootState.DownMoving:
                //lowered position
                this.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 1.5f, 0);
                center.GetComponent<Rigidbody>().AddTorque(-seekDifference, ForceMode.Force);
                this.GetComponent<Rigidbody>().AddTorque(seekDifference, ForceMode.Force);
                break;
        }

    }

    void OnCollisionEnter()
    {
        if (state == FootState.DownMoving)
        {
            //thunk
            state = FootState.Placed;
        }
    }
    void StartStep()
    {
        state = FootState.UpMoving;
    }

}
