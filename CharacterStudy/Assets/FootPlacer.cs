using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlacer : MonoBehaviour {


    public float velocityMult = 0.2f; //we aren't gonna be fast enough for instantaneous movement
    public Rigidbody bodyCenter;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public Vector3 leftNew, rightNew;
    public float placeDepth = 1.0f;
    public float placeMin = 0.2f;
    public float placeSpeed = 1.0f;

    public Vector3 rightOffset, leftOffset = Vector3.zero;

    public ReactionController controller;

    Vector3 placeTilt = Vector3.up;

    Vector3 leftCross, rightCross;
    Vector3 footSpot;

    bool rightUp, leftUp = false;
    

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
            placeTilt = controller.resultTilt;
        }
        if (rightFoot && leftFoot)
        {
            

            RaycastHit hit;
            if (Physics.Raycast(bodyCenter.position, Vector3.down, out hit, placeDepth, LayerMask.GetMask("Ground")))
            //if (Physics.Raycast(bodyCenter.position, -placeTilt, out hit, placeDepth, LayerMask.GetMask("Ground")))
            {
                footSpot = hit.point;
                float leftDist = (leftFoot.transform.position - footSpot).magnitude;
                float rightDist = (rightFoot.transform.position - footSpot).magnitude;
                
                
                if (Vector3.Dot(leftFoot.transform.position - footSpot, rightFoot.transform.position - footSpot) > 0)
                {

                    if (leftDist > placeMin && rightDist > placeMin && !leftUp && !rightUp)
                    {

                        if (leftDist > rightDist && leftUp == false)
                        {
                            leftNew = footSpot + (bodyCenter.rotation * leftOffset) + (bodyCenter.velocity * velocityMult);
                            leftUp = true;
                        }
                        else if (rightUp == false)
                        {
                            rightNew = footSpot + (bodyCenter.rotation * rightOffset) + (bodyCenter.velocity * velocityMult);
                            rightUp = true;
                        }
                    }
                    
                }

                if(leftUp)
                {
                    if(leftFoot.GetComponent<FootHint>().blend >= 0)
                    {
                        leftFoot.GetComponent<FootHint>().blend -= Time.deltaTime * placeSpeed;
                    }
                    else
                    {
                        leftUp = false;
                        leftFoot.transform.position = leftNew;
                        leftFoot.GetComponent<FootHint>().blend = 0;
                    }
                }
                else
                {
                    if (leftFoot.GetComponent<FootHint>().blend < 1)
                    {
                        leftFoot.GetComponent<FootHint>().blend += Time.deltaTime * placeSpeed;
                    }
                    else
                    {
                        leftFoot.GetComponent<FootHint>().blend = 1.0f;
                    }
                }


                if(rightUp)
                {
                    if (rightFoot.GetComponent<FootHint>().blend >= 0)
                    {
                        rightFoot.GetComponent<FootHint>().blend -= Time.deltaTime * placeSpeed;
                    }
                    else
                    {
                        rightUp = false;
                        rightFoot.transform.position = rightNew;
                        rightFoot.GetComponent<FootHint>().blend = 0;
                    }
                }
                else
                {
                    if (rightFoot.GetComponent<FootHint>().blend < 1)
                    {
                        rightFoot.GetComponent<FootHint>().blend += Time.deltaTime * placeSpeed;
                    }
                    else
                    {
                        rightFoot.GetComponent<FootHint>().blend = 1.0f;
                    }
                }

                //Debug.Log(Vector3.Dot(leftFoot.position - footSpot, rightFoot.position - footSpot));
                //Debug.Log(Vector3.Dot(leftCross, rightCross));
                //Debug.Log(leftDot + " " + rightDot);


                /*
                Vector3 footSpot = hit.point + (bodyCenter.velocity * velocityMult);
                float leftDist = (leftFoot.position - footSpot).magnitude;
                float rightDist = (rightFoot.position - footSpot).magnitude;

                if (leftDist > placeDist && rightDist > placeDist)
                {
                    if (leftDist > rightDist)
                    {
                        leftFoot.position = footSpot + (bodyCenter.rotation * leftOffset);
                    }
                    else
                    {
                        rightFoot.position = footSpot + (bodyCenter.rotation * rightOffset);
                    }
                }
                */
            }


        }
    }
    void OnDrawGizmos()
    {
        if (rightFoot && leftFoot)
        {


            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(rightFoot.transform.position, 0.1f);
            Gizmos.DrawSphere(leftFoot.transform.position, 0.1f);
            Gizmos.DrawRay(leftFoot.transform.position, leftCross * 10.0f);
            Gizmos.DrawRay(rightFoot.transform.position, rightCross * 10.0f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(footSpot, 0.1f);
        }
    }
}
