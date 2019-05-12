﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimMovement : MonoBehaviour
{
    float x, z = 0.0f;
    float rx, rz = 0.0f;
    Vector3 moveVect = Vector3.zero;
    Vector3 turnVect = Vector3.zero;
    WeaponAttack weapon;
    CharacterController motor;
    public bool playerControlled;
    public float maxMoveForce;
    public float maxTurnTorque;
    // Use this for initialization
    void Start()
    {
        motor = this.GetComponent<CharacterController>();
        weapon = this.GetComponent<WeaponAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlled)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            moveVect += new Vector3(x, 0, z);
            x = 0;
            z = 0;

            rx = Input.GetAxis("RightH");
            rz = Input.GetAxis("RightV");
            turnVect += new Vector3(rx, 0, rz);
        }
        else
        {

        }


        moveVect *= maxMoveForce;
        if (moveVect.magnitude > maxMoveForce)
        {
            moveVect = moveVect.normalized * maxMoveForce;
        }
        if (motor.isGrounded)
        {
            motor.Move(moveVect);
        }
        else
        {
            motor.Move(Vector3.down * 9.8f);
        }
        moveVect = Vector3.zero;
        if (turnVect.sqrMagnitude > 0)
        {
            
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(turnVect), maxTurnTorque);
        }
        //motor.AddTorque(Vector3.Cross(motor.transform.forward, turnVect.normalized) * maxTurnTorque, ForceMode.Force);
        turnVect = Vector3.zero;


    }
}