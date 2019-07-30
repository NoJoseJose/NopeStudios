using UnityEngine;
using System.Collections;

public class WheelControl : MonoBehaviour {
    WheelCollider fL;
    WheelCollider fR;
    WheelCollider mL;
    WheelCollider mR;
    WheelCollider rL;
    WheelCollider rR;
    GameObject body;
    AudioSource rev;
    //    WheelCollider[] allWheels; 
    //    WheelCollider[] rightSideWheels;
    //    WheelCollider[] leftSideWheels;
    public float maxTorque = 5;
    public float brakeTorque = 10;

    // Use this for initialization
    void Start () {

        body = transform.Find("Base").gameObject;
        fL = transform.Find("WheelFL").gameObject.GetComponent<WheelCollider>();
        fR = transform.Find("WheelFR").gameObject.GetComponent<WheelCollider>();
        mL = transform.Find("WheelML").gameObject.GetComponent<WheelCollider>();
        mR = transform.Find("WheelMR").gameObject.GetComponent<WheelCollider>();
        rL = transform.Find("WheelRL").gameObject.GetComponent<WheelCollider>();
        rR = transform.Find("WheelRR").gameObject.GetComponent<WheelCollider>();

        rev = body.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Mathf.Clamp(y, -1, 1);
        Mathf.Clamp(x, -1, 1);

        MoveAllWheels(y * maxTorque);
        BrakeWheels(x);
        rev.pitch = 1 + Mathf.Abs(AverageRPM())/300f;

    }
    float AverageRPM()
    {
        return (
        fL.rpm +
        fR.rpm +
        mL.rpm +
        mR.rpm +
        rL.rpm +
        rR.rpm) /
        6;

    }
    void MoveAllWheels(float torque)
    {
        fL.motorTorque = torque;
        fR.motorTorque = torque;
        mL.motorTorque = torque;
        mR.motorTorque = torque;
        rL.motorTorque = torque;
        rR.motorTorque = torque;

    }
    void BrakeWheels(float horizontal)
    {
        fL.brakeTorque = Mathf.Clamp(horizontal, -1, 0) * -1f * brakeTorque;
        mL.brakeTorque = Mathf.Clamp(horizontal, -1, 0) * -1f * brakeTorque;
        rL.brakeTorque = Mathf.Clamp(horizontal, -1, 0) * -1f * brakeTorque;

        fR.brakeTorque = Mathf.Clamp(horizontal, 0, 1) * brakeTorque;
        mR.brakeTorque = Mathf.Clamp(horizontal, 0, 1) * brakeTorque;
        rR.brakeTorque = Mathf.Clamp(horizontal, 0, 1) * brakeTorque;

        //rR.rpm


    }



}
