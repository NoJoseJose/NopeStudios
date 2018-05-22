using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour {
    Rigidbody[] massPoints;
//    public Rigidbody legbase;
    public float compScale = 1.0f;
    public GameObject foot; //the thing we balance on - maybe not a foot
    Vector3 centerOfMass = Vector3.zero;
    ConfigurableJoint joint;
    List<Ray> visuals = new List<Ray>();

	// Use this for initialization
	void Start () {
        massPoints = this.GetComponentsInChildren<Rigidbody>();
    //    joint = legbase.GetComponent<ConfigurableJoint>();
    }
	
	// Update is called once per frame
	void Update () {

        //find CoM
        float massTotal = 0;
        var lastCoM = centerOfMass;
        centerOfMass = Vector3.zero;

        foreach (Rigidbody massPoint in massPoints)
        {
            centerOfMass += massPoint.worldCenterOfMass * massPoint.mass;
            massTotal += massPoint.mass;
        }
        centerOfMass /= massTotal;

        visuals.Clear();

 

        //displacement acceleration
        // joint.targetAngularVelocity = Vector3.Cross(Vector3.down, foot.transform.up * torqueScale);

        //AddTorque(Vector3.Cross(Vector3.down, foot.transform.up * torqueScale), ForceMode.Force);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(centerOfMass, 0.2f);
        if (visuals.Count > 0)
        {
            
            Gizmos.color = Color.red;
            //Gizmos.DrawRay(centerOfMass, Vector3.down * 9.8f);
            //Gizmos.DrawRay(foot.transform.position, foot.transform.up * -1.0f);
            //Gizmos.DrawRay(legbase.position, Vector3.Cross(Vector3.down, foot.transform.up * torqueScale) * 100);
            foreach (Ray visual in visuals)
            {
                Gizmos.DrawRay(visual);
            }
                
        }
    }
}
