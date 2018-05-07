using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingRotator : MonoBehaviour {

    public Rigidbody body;
    public float Kp, Kd = 1.0f;
    public float deadVelocity = 1.0f;

    Vector3 lastError;
    Vector3 P, D, seekDifference = Vector3.zero;

    public Transform optionalTarget;
    public bool on = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        if (!on || optionalTarget == null)
        {
            P = Vector3.Cross(body.velocity, body.transform.forward);
        }
        else
        {
            P = Vector3.Cross((optionalTarget.position - body.position).normalized, body.transform.forward);
        }
        D = (P - lastError) / Time.fixedDeltaTime;
        lastError = P;
        seekDifference = P * Kp + D * Kd;

        if (body.velocity.magnitude > deadVelocity || on)
        {
            body.AddTorque(seekDifference, ForceMode.Force);
        }
    }
}
