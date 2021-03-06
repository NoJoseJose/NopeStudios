﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReactionController : NetworkBehaviour {

    public Vector3 desiredVelocity = Vector3.zero;
    public float maxDesiredSpeed = 10.0f;
    public float maxAcceleration = 1.0f;

    public Rigidbody mainBody;
    public Vector3 resultTilt;
   // public Vector3 desiredAcceleration;

    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D, seekDifference = Vector3.zero;
    public bool isPlayer = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        */
        if (!isLocalPlayer)
        {
            return;
        }
        if (isPlayer)
        {
            desiredVelocity.x = Input.GetAxis("Horizontal");
            desiredVelocity.z = Input.GetAxis("Vertical");
            desiredVelocity = desiredVelocity * maxDesiredSpeed;
        }
    }
    void FixedUpdate()
    {
        
        P = (desiredVelocity - mainBody.velocity);
        P.y = 0f;

        I += P * Time.fixedDeltaTime;
        D = (P - lastError) / Time.fixedDeltaTime;
        lastError = P;
        seekDifference = P * Kp + I * Ki + D * Kd;
        if (seekDifference.magnitude > maxAcceleration)
        {
            seekDifference = seekDifference.normalized * maxAcceleration;
        }
        /*
        if (mainBody.velocity.sqrMagnitude < velocitySqrDeadzone - desiredVelocity.magnitude)
        {
            desiredAcceleration = Vector3.zero;
        }
        */
        resultTilt = new Vector3(0, 9.8f, 0) + seekDifference;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(mainBody.position, resultTilt);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(mainBody.position, seekDifference);
    }
}
