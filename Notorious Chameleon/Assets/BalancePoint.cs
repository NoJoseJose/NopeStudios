using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePoint : MonoBehaviour
{
    public GameObject characterParent;
    public float desiredHeight = 1.0f;


    public Vector3 desiredVelocity = Vector3.zero;
    public float maxDesiredSpeed = 10.0f;
    public float maxAcceleration = 1.0f;

    //public Rigidbody mainBody;
    public Vector3 resultTilt;
    // public Vector3 desiredAcceleration;

    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastPos;
    Vector3 lastError = Vector3.zero;
    Vector3 P, I, D, seekDifference = Vector3.zero;
    public bool isPlayer = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        /*
        int bodies = 0;
        Vector3 com = Vector3.zero;
        foreach (Rigidbody body in characterParent.GetComponentsInChildren<Rigidbody>(true))
        {
            com += body.position * body.mass;
            bodies++;
        }
        com /= bodies;
        */
        Vector3 com = characterParent.GetComponentInChildren<Rigidbody>().position + characterParent.GetComponentInChildren<Rigidbody>().centerOfMass;
        Vector3 velocity = (com - lastPos) / Time.fixedDeltaTime;
        lastPos = com;
        P = (/*desiredVelocity*/  - velocity);


        I += P * Time.fixedDeltaTime;
        D = (P - lastError) / Time.fixedDeltaTime;
        lastError = P;
        seekDifference = P * Kp + I * Ki + D * Kd;
        if (seekDifference.magnitude > maxAcceleration)
        {
            seekDifference = seekDifference.normalized * maxAcceleration;
        }
        resultTilt = new Vector3(0, -9.8f, 0) + seekDifference;

    }
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(lastPos, resultTilt);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(lastPos, seekDifference);
        
    }
}
