using UnityEngine;
using System.Collections;

public class TurretSolver : MonoBehaviour {

    public Vector3 target;
    public Transform followTarget;
    GameObject barrelBase;
    GameObject bodyBase; //the turret needs a local orientation to figure where to face the hinging

    public Vector3 trace;

	// Use this for initialization
	void Start () {

        bodyBase = transform.parent.Find("Base").gameObject;
        barrelBase = transform.parent.Find("BarrelBase").gameObject;
        target = Vector3.zero;


        GetComponent<HingeJoint>().useMotor = true;
        barrelBase.GetComponent<HingeJoint>().useMotor = true;

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        if (followTarget)
        {
            target = followTarget.position;
        }
        Vector3 localTarget = bodyBase.transform.InverseTransformDirection(target - transform.position);
        Quaternion localTargetAngles = Quaternion.LookRotation(localTarget);
        Quaternion barrelOrientation = Quaternion.LookRotation(bodyBase.transform.InverseTransformDirection(barrelBase.transform.up));
        JointMotor baseMotor = GetComponent<HingeJoint>().motor;
        //    float dif = Quaternion.FromToRotation(barrelOrientation * Vector3.forward, localTargetAngles * Vector3.forward).eulerAngles.y;
        float a = barrelOrientation.eulerAngles.y;
        float b = localTargetAngles.eulerAngles.y;
        float dif = Mathf.DeltaAngle(a, b);
        baseMotor.targetVelocity = dif * 3;

        
        GetComponent<HingeJoint>().motor = baseMotor;
        
        JointMotor barrelMotor = barrelBase.GetComponent<HingeJoint>().motor;
        //transform localeulerangles or something, ugh
        //  bodyBase.transform.localEulerAngles
        // dif = (Quaternion.FromToRotation(barrelOrientation * Vector3.forward, localTargetAngles * Vector3.forward)).eulerAngles.x;
        a = barrelOrientation.eulerAngles.x;
        b = localTargetAngles.eulerAngles.x;
        if (a > 180f)
            a -= 360f;
        if (b > 180f)
            b -= 360f;
        dif = b - a;


    //    Debug.Log(barrelOrientation.eulerAngles.x + " " + localTargetAngles.eulerAngles.x);
        if (dif > 180f)
        {
            dif -= 360f;
        }
        else
        {
            
        }
        if (dif > 0)
            dif = Mathf.Clamp(dif, 0, 4);
        else
            dif = Mathf.Clamp(dif, -4, 0);
      //  Debug.Log(dif);
        barrelMotor.targetVelocity = dif * 3;

        barrelBase.GetComponent<HingeJoint>().motor = barrelMotor;
    }


    /*
    void FixedUpdate()
    {
        if (followTarget)
        {
            target = followTarget.position;
        }
        Vector3 localTarget = bodyBase.transform.InverseTransformDirection(target - transform.position);
        Quaternion localTargetAngles = Quaternion.LookRotation(localTarget);

        JointSpring baseSpring = GetComponent<HingeJoint>().spring;
        if (localTargetAngles.eulerAngles.y > 180)
        {
            baseSpring.targetPosition = (localTargetAngles.eulerAngles.y - 360);
        }
        else
        {
            baseSpring.targetPosition = localTargetAngles.eulerAngles.y;
        }
        GetComponent<HingeJoint>().spring = baseSpring;

        JointSpring barrelSpring = barrelBase.GetComponent<HingeJoint>().spring;
        if (localTargetAngles.eulerAngles.x > 180)
            barrelSpring.targetPosition = (localTargetAngles.eulerAngles.x - 360);
        else
            barrelSpring.targetPosition = localTargetAngles.eulerAngles.x;

        barrelBase.GetComponent<HingeJoint>().spring = barrelSpring;


    }
    */

    void OnDrawGizmos()
    {
        if (bodyBase)
        {
            Gizmos.color = Color.magenta;
            //Gizmos.DrawRay(bodyBase.GetComponent<Transform>().position, trace - bodyBase.GetComponent<Transform>().position);
        }    

    }

}
