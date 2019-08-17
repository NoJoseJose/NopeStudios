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

        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        Vector3 object_pos = Camera.main.WorldToScreenPoint(this.transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));


    }
    void FixedUpdate()
    {
        
        motor.AddForce((moveVect).normalized * maxMoveForce, ForceMode.Force);
        moveVect = Vector3.zero;
    }
}
