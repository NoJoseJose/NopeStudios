using UnityEngine;
using System.Collections;

public class HipBalancer : MonoBehaviour {

    Rigidbody hipRigidbody;
    public float balancerForce = -10f;
    public float upwardForce = 4f;
    public float hoverDist = 6f;
    public float balancerThreshold = 2f;


    // Use this for initialization
    void Start () {
        hipRigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        int layermask = 1 << 8;
        if (Physics.Raycast(transform.position, -hipRigidbody.transform.up, out hit, hoverDist, layermask))
        {
            float expected = hit.point.y + hoverDist;
            
            hipRigidbody.AddForce(Vector3.up * (expected - transform.position.y) * upwardForce, ForceMode.Impulse);
            //AddForceAtPosition
            if ((Vector3.up - hipRigidbody.transform.up).magnitude > balancerThreshold)
            {
                hipRigidbody.AddForce((Vector3.up - hipRigidbody.transform.up) * balancerForce, ForceMode.Acceleration);
            }
        }
       
    }

    void OnDrawGizmos()
    {
        if (hipRigidbody)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawRay(hipRigidbody.transform.position - (Vector3.up * 0.5f), (Vector3.up - hipRigidbody.transform.up) * balancerForce);
        }
    }
}
