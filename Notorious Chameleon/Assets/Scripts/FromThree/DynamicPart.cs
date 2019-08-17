using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPart : MonoBehaviour
{
    public DynamicPart prevLimb = null;
    public DynamicPart nextLimb = null;
    Rigidbody rb;
    public Vector3 angVel, angAccel, linAccelJoint; //these are relative to parent body, I think
    Vector3 lastAngVel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //calc angular vel (prev)
    public void CalcAngVel(DynamicPart prevLimb)
    {
        if (prevLimb != null)
        {

            Rigidbody prb = prevLimb.GetComponentInParent<Rigidbody>();
            angVel = rb.angularVelocity - prb.angularVelocity;
        }
        
    }
    //calc angular accel (prev)
    public void CalcAngAccel(DynamicPart prevLimb)
    {
        if (prevLimb != null)
        {
            Rigidbody prb = prevLimb.GetComponentInParent<Rigidbody>();
            angVel = rb.angularVelocity - prb.angularVelocity;
            angAccel = (lastAngVel - angVel) / Time.fixedDeltaTime;

            lastAngVel = angVel;
        }

    }
    //calc linear accel at joint (prev)
    public void CalcLinAccelJoint(DynamicPart prevLimb)
    {
        if (prevLimb != null)
        {
            Rigidbody prb = prevLimb.GetComponentInParent<Rigidbody>();
            ConfigurableJoint pjoint = prevLimb.GetComponentInParent<ConfigurableJoint>();
           // pjoint.connectedAnchor
        }

    }
    //calc linear accel at center (curr)
    public void CalcLinAccelCOM()
    {

    }
    //calc force (curr)
    public void CalcLimbForce()
    {

    }
    //calc accel (curr)
    public void CalcLimbTorque()
    {

    }


    //calc jointforce (next)
    public void CalcJointForce(DynamicPart nextLimb) { }
    //calc jointTorque (next)
    public void CalcJointTorque(DynamicPart nextLimb) { }
}
