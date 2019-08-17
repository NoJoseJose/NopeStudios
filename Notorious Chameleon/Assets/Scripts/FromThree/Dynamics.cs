using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamics : MonoBehaviour
{
    DynamicPart finalLimb;
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
        UpdateForward();
        UpdateBackwards();
    }

    void UpdateForward()
    {
        DynamicPart currentLimb = this.GetComponent<DynamicPart>();
        DynamicPart prevLimb = null;
        while (currentLimb != null)
        {
            //calc angular vel (prev)
            currentLimb.CalcAngVel(prevLimb);
            //calc angular accel (prev)
            currentLimb.CalcAngAccel(prevLimb);
            //calc linear accel at joint (prev)
            currentLimb.CalcLinAccelJoint(prevLimb);
            //calc linear accel at center (curr)
            currentLimb.CalcLinAccelCOM();
            //calc force (curr)
            currentLimb.CalcLimbForce();
            //calc accel (curr)
            currentLimb.CalcLimbTorque();
            prevLimb = currentLimb;
            currentLimb = currentLimb.nextLimb;
        }
        finalLimb = prevLimb;

    }
    void UpdateBackwards()
    {
        DynamicPart nextLimb = null;
        DynamicPart currentLimb = finalLimb;
        while (currentLimb != null)
        {
            //calc jointforce (next)
            currentLimb.CalcJointForce(nextLimb);
            //calc jointTorque (next)
            currentLimb.CalcJointTorque(nextLimb);
            nextLimb = currentLimb;
            currentLimb = currentLimb.prevLimb;
        }
    }
}
