using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {

    float x, z = 0.0f;
    Vector3 moveVect = Vector3.zero;
    public Rigidbody motor;
    public float maxMoveForce;
    

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        moveVect += new Vector3(x, 0, z);
        x = 0;
        z = 0;

    }
    void FixedUpdate()
    {
        
        motor.AddForce((moveVect).normalized * maxMoveForce, ForceMode.Force);
        moveVect = Vector3.zero;
    }
}
