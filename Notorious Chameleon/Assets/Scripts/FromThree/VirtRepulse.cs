using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtRepulse : MonoBehaviour {

    
    public Rigidbody parent;
    public float repulseDist;
    public float repulseMax;
    public float repulseMult;
    public float repulseDamp;
    public float contactDist = 0.0f;
  //  public float angularDamp;
    //public float intMult;
    public float lastForce = 0;
   // public float integ = 0;
   // public float integDecay = 1;
	// Use this for initialization
	void Start () {
        //parent = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(parent.position, -parent.transform.up, out hit, repulseDist , LayerMask.GetMask("Ground")))
        {
            contactDist = (parent.transform.position - hit.point).magnitude;
            var forceMag = repulseDist - contactDist;
            var forceDamp = (forceMag - lastForce) / Time.fixedDeltaTime;
           // integ += forceMag * Time.fixedDeltaTime;
            var forceTotal = Mathf.Clamp(forceMag * repulseMult + forceDamp * repulseDamp/* + integ * intMult*/, 0, repulseMax);
            lastForce = forceMag;


            //parent.AddForce(parent.angularVelocity * contactDist, ForceMode.VelocityChange);
            parent.AddForce(parent.transform.up * forceTotal, ForceMode.Force);
            //parent.AddTorque(parent.angularVelocity * angularDamp);
        }
        else
        {
          // integ -= integDecay * Time.fixedDeltaTime;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (parent)
        {
            Gizmos.DrawRay(parent.position, -parent.transform.up * repulseDist);
        }
        
        
    }
}
