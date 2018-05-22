using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlacement : MonoBehaviour {

    public GameObject[] feet;
    

    public GameObject center; 
    public Vector3 desiredLeverOrientation = new Vector3(0, 1, 0);
    public ReactionController controller;

    public float angularTolerance = 1.0f;
    public float maxTorque = 10.0f;

    public float Kp = 1.0f;
    public float Ki = 0.0f;
    public float Kd = 0.0f;
    Vector3 lastError;
    Vector3 P, I, D, seekDifference = Vector3.zero;


    // Use this for initialization
    void Start () {
        

    }

    // Update is called once per frame
    void Update () {
		
	}


    void FixedUpdate()
    {
        
        if (controller)
        {
            desiredLeverOrientation = controller.resultTilt;
        }

        int footCounter = 0;
        
        foreach (GameObject foot in feet)
        {
            foot.GetComponent<Foot>().desiredLeverOrientation = desiredLeverOrientation;
            foot.GetComponent<Foot>().desiredCenterPosition = center.transform.position;
            foot.GetComponent<Foot>().angularTolerance = angularTolerance;
            footCounter++;

        }

        /*
        foreach (GameObject foot in feet)
        {
            P = Vector3.Cross(center.transform.position - foot.transform.position, desiredLeverOrientation);
            I += P * Time.fixedDeltaTime;
            D = (P - lastError) / Time.fixedDeltaTime;
            lastError = P;
            seekDifference = P * Kp + I * Ki + D * Kd;

            if (seekDifference.magnitude > maxTorque)
            {
                seekDifference = seekDifference.normalized * maxTorque;
            }
            // THIS SHOULDN'T BE USED FOR MULTIPLE - CLEAR THIS UP 

            float angleError = Vector3.Dot((center.transform.position - foot.transform.position).normalized, (desiredLeverOrientation).normalized);


            

            if (angleError < angularTolerance)
            {
                
                //raised position
                foot.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 0.5f, 0);
                center.GetComponent<Rigidbody>().AddTorque(-seekDifference, ForceMode.Force);
                foot.GetComponent<Rigidbody>().AddTorque(seekDifference, ForceMode.Force);
            }
            else
            {
                //lowered position
                foot.GetComponent<ConfigurableJoint>().targetPosition = new Vector3(0, 1.5f, 0);

            }
            footCounter++;
        }
        */

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }
}
