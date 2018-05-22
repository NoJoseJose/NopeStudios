using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    float x, z = 0.0f;
    Vector3 moveVect = Vector3.zero;
    public Rigidbody motor;
    public float maxMoveForce;
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

    }
    void FixedUpdate()
    {
        //motor.AddRelativeForce((moveVect).normalized * maxMoveForce, ForceMode.Force);
        motor.AddForce((moveVect).normalized * maxMoveForce, ForceMode.Force);
        moveVect = Vector3.zero;
    }
}
