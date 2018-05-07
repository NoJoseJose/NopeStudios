using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    float x, z = 0.0f;
    float rx, rz = 0.0f;
    Vector3 moveVect = Vector3.zero;
    Vector3 turnVect = Vector3.zero;
    public Rigidbody motor;
    public float maxMoveForce;
    public float maxTurnTorque;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        moveVect += new Vector3(x, 0, z);
        x = 0;
        z = 0;

        rx = Input.GetAxis("RightH");
        rz = Input.GetAxis("RightV");
        turnVect += new Vector3(rx, 0, rz);
        
    }
    void FixedUpdate()
    {
        //motor.AddRelativeForce((moveVect).normalized * maxMoveForce, ForceMode.Force);
        moveVect *= maxMoveForce;
        if(moveVect.magnitude > maxMoveForce)
        {
            moveVect = moveVect.normalized * maxMoveForce;
        }
        motor.AddForce(moveVect, ForceMode.Force);
        moveVect = Vector3.zero;
        motor.AddTorque(Vector3.Cross(motor.transform.forward, turnVect.normalized) * maxTurnTorque, ForceMode.Force);
        turnVect = Vector3.zero;
    }
    void OnDrawGizmos()
    {
        
        //Debug.Log(turnVect);
        Gizmos.color = Color.blue;
        
        Gizmos.DrawRay(motor.transform.position, Vector3.Cross(motor.transform.forward, turnVect.normalized));
        
        
    }
}
