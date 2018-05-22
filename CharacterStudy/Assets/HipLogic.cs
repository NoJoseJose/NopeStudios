using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipLogic : MonoBehaviour {

    //float legLength = 3f;
    //float stanceLength;

    public GameObject[] legs;

    Rigidbody hip;
    public GameObject seekPoint;

    float legCount;

    Vector3 integError = Vector3.zero;
    Vector3 pLastError = Vector3.zero;

    public float Kp = 1.0f;
    public float Ki = 1.0f;
    public float Kd = 1.0f;

    public float maxForce = 100f;

    Vector3 pforce = Vector3.zero;
    Vector3 iforce = Vector3.zero;
    Vector3 dforce = Vector3.zero;

    // Use this for initialization
    void Start () {
        hip = this.gameObject.GetComponent<Rigidbody>();
        legCount = legs.Length;

    }
    /*
	float FindHeight(Rigidbody foot, float legLength)
    {
        //legvector dot widthvector = height
        //or just do this
        var stanceLength = (new Vector3(foot.position.x, 0, foot.position.z) - new Vector3(this.gameObject.GetComponent<Rigidbody>().position.x, 0, this.gameObject.GetComponent<Rigidbody>().position.z)).sqrMagnitude;
        //find how high we can sit
        return maxHeight = Mathf.Sqrt((legLength * legLength) - (stanceLength));
    }*/
	// Update is called once per frame
	void FixedUpdate ()
    {
        //seek the point!

        
        Vector3 pError = hip.position - seekPoint.transform.position;
        integError += pError * Time.fixedDeltaTime;
        Vector3 dError = (pError - pLastError) / Time.fixedDeltaTime;
        pLastError = pError;


        foreach (GameObject leg in legs)
        {
            var thigh = leg.GetComponent<ThighScript>().GetComponent<Rigidbody>();
            var foot = leg.GetComponent<ThighScript>().foot;
            var legAnchor = leg.transform.TransformPoint(leg.GetComponent<ConfigurableJoint>().anchor);

            pforce = pError * Kp;
            iforce = integError * Ki;
            dforce = dError * Kd;

            Vector3 force = (pforce + iforce + dforce).normalized * Mathf.Clamp((pforce + iforce + dforce).magnitude, 0, maxForce);
                 

            hip.AddForceAtPosition(force, legAnchor, ForceMode.Force);
            foot.GetComponent<Rigidbody>().AddForce(force * -1, ForceMode.Force);


            /* 
             //simple (cheat) seeking, pushing force divided on all legs
             seekMag = (seekPoint.transform.position - hip.position).magnitude * seekStrength / legCount;
             seekMag = Mathf.Clamp(seekMag, 0.0f, seekCap);
             hip.AddForceAtPosition((seekPoint.transform.position - hip.position).normalized * seekMag, legAnchor, ForceMode.Force);
             foot.GetComponent<Rigidbody>().AddForce((seekPoint.transform.position - hip.position).normalized * -seekMag, ForceMode.Force);


             //A dot B, what we can push with this leg projected on to where we want to go
             vertScalar = Vector3.Dot(foot.transform.position - legAnchor, Vector3.up) / (foot.transform.position - legAnchor).magnitude;


             vertMag = vertCap * vertScalar;
             vertForce = (foot.transform.position - legAnchor).normalized * -vertMag / legCount;
             hip.AddForceAtPosition(vertForce, legAnchor, ForceMode.Acceleration);
             foot.GetComponent<Rigidbody>().AddForce(-vertForce * foot.GetComponent<Rigidbody>().mass, ForceMode.Force);

             //Cheat seeking

             seekMag = ((seekPoint.transform.position - hip.position).magnitude) * seekStrength / legCount;
             var seekAccel = 0.0f;
             if ((seekPoint.transform.position - hip.position).magnitude > 0.001f)
             {
                 seekAccel = 0.5f * hip.velocity.sqrMagnitude / ((seekPoint.transform.position - hip.position).magnitude);
             }
             seekAccel *= hip.mass;

             seekMag = Mathf.Clamp(seekMag, 0.0f, seekCap);


             seekForce = (seekPoint.transform.position - hip.position).normalized * seekMag;
             hip.AddForceAtPosition(seekForce, legAnchor, ForceMode.Force);
             foot.GetComponent<Rigidbody>().AddForce(-seekForce, ForceMode.Force);
             var seekDragMag = 0.0f;

             if(hip.velocity.magnitude < seekDrag)
             {
                 seekDragMag = -hip.velocity.magnitude;
             }
             else
             {
                 seekDragMag = -seekDrag;
             }

             //seekDragForce = (seekPoint.transform.position - hip.position).normalized * seekDragMag;
             seekDragForce = (hip.velocity).normalized * seekDragMag;
             hip.AddForceAtPosition(seekDragForce, legAnchor, ForceMode.Force);
             foot.GetComponent<Rigidbody>().AddForce(-seekDragForce, ForceMode.Force);

             //hip righting motion
             torqueForce = Vector3.Cross(transform.up, Vector3.up) * torqueStrength / legCount;
             torqueForce = torqueForce.normalized * Mathf.Clamp(torqueForce.magnitude, 0.0f, torqueCap);
             hip.AddTorque(torqueForce, ForceMode.Force);
             thigh.AddTorque(-torqueForce, ForceMode.Force);
             */



        }
        

        //hip.AddTorque(Vector3.Cross(transform.up, Vector3.up) * torqueStrength, ForceMode.Force);

    }
    void OnDrawGizmos()
    {
        if (hip)
        {

            


            foreach (GameObject leg in legs)
            {
                var foot = leg.GetComponent<ThighScript>().foot;
                var legAnchor = leg.transform.TransformPoint(leg.GetComponent<ConfigurableJoint>().anchor);
                Gizmos.color = Color.magenta;
                Gizmos.DrawRay(legAnchor, pforce * 0.01f);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(legAnchor, iforce * 0.01f);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(legAnchor, dforce * 1.0f);
                Gizmos.color = Color.green;
                Gizmos.DrawRay(legAnchor, pforce + dforce + iforce * 0.01f);

                /*
                var foot = leg.GetComponent<ThighScript>().foot;
                var legAnchor = leg.transform.TransformPoint(leg.GetComponent<ConfigurableJoint>().anchor);

                Gizmos.color = Color.magenta;

                Gizmos.DrawRay(legAnchor, vertForce * 0.1f);
                Gizmos.DrawRay(foot.transform.position, -vertForce * 0.1f);


                Gizmos.color = Color.green;
                Gizmos.DrawRay(foot.transform.position, -seekForce * 0.1f);
                Gizmos.DrawRay(legAnchor, seekForce * 0.1f);

                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(foot.transform.position, -seekDragForce * 0.1f);
                Gizmos.DrawRay(legAnchor, seekDragForce * 0.1f);
                */


            }

        }

    }
}
