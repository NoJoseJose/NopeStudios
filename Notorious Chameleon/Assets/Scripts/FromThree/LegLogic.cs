using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegLogic : MonoBehaviour {

    public Rigidbody hip; //or whatever is the platform we're moving in relation to
    public float legPeriod = 1.0f;
    public float legTick = 0;
    public float legSearchDist = 2.0f;

    //public GameObject l1, l2, l3, l4;
    //public GameObject r1, r2, r3, r4;
    Vector3 leftTarget;
    Vector3 rightTarget;
    bool leftActive, rightActive = false;
    public BezierLeg leftLeg, rightLeg;
    //public GameObject activeSupport;

	// Use this for initialization
	void Start () {
        leftTarget = this.transform.InverseTransformDirection(hip.position - leftLeg.points[3].transform.position);
        rightTarget = this.transform.InverseTransformDirection(hip.position - rightLeg.points[3].transform.position);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        legTick += Time.fixedDeltaTime;
        if(legTick > legPeriod)
        {
            if (leftActive)
            {
                leftActive = false;
                rightActive = true;
                rightLeg.points[0].transform.position = rightLeg.points[3].transform.position;
                //activeSupport.transform.position = leftLeg.points[0].transform.position;
            }
            else
            {
                rightActive = false;
                leftActive = true;
                leftLeg.points[0].transform.position = leftLeg.points[3].transform.position; //leg reset
                //activeSupport.transform.position = rightLeg.points[0].transform.position;
            }
            legTick = 0;
        }

        if (leftActive)
        {
            RaycastHit hit;
            if (Physics.Raycast(hip.position, -hip.transform.TransformDirection(leftTarget) + hip.velocity * legPeriod, out hit, legSearchDist, LayerMask.GetMask("Ground")))
            {
                leftLeg.points[3].transform.position = hit.point;
            }

            //leftLeg.points[3].transform.position = hip.position - hip.transform.TransformDirection(leftTarget) + hip.velocity * legPeriod; //ongoing leg update

            leftLeg.UpdateLeg(legTick/legPeriod);
        }
        if(rightActive)
        {
            RaycastHit hit;
            if (Physics.Raycast(hip.position, -hip.transform.TransformDirection(rightTarget) + hip.velocity * legPeriod, out hit, legSearchDist, LayerMask.GetMask("Ground")))
            {
                rightLeg.points[3].transform.position = hit.point;
            }
            //rightLeg.points[3].transform.position = hip.position - hip.transform.TransformDirection(rightTarget) + hip.velocity * legPeriod;
            rightLeg.UpdateLeg(legTick / legPeriod);
        }



            

    }

    void OnDrawGizmos()
    {
        if (hip)
        {

            Gizmos.color = Color.black;
            Gizmos.DrawRay(hip.transform.position, -hip.transform.TransformDirection(leftTarget));
            Gizmos.DrawRay(hip.transform.position, -hip.transform.TransformDirection(rightTarget));

        }
    }
}
