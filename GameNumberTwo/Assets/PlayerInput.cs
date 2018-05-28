using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public ReactionController player;
    float x, z = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        /*
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        */

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        player.desiredVelocity.x = x;
        player.desiredVelocity.z = z;
        player.desiredVelocity = player.desiredVelocity * player.maxDesiredSpeed;
   
    }

}