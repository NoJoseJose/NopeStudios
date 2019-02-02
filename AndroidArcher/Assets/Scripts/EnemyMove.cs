using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody body;
    public Rigidbody target;
    public float moveForce = 1;
    public float maxVelocity = 1;
    private bool movementEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementEnabled && target)
        {
            //Debug.Log((1 - (Vector3.Dot(body.velocity, target.position - body.position) / maxVelocity)));
            Vector3 actingForce = (target.position - body.position).normalized * moveForce;
            
            body.AddForce(actingForce, ForceMode.Force);
        }
    }

    public void StopMovement()
    {
        movementEnabled = false;
    }
}
