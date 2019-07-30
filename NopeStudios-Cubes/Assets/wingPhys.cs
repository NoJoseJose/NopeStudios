using UnityEngine;
using System.Collections;

public class wingPhys : MonoBehaviour {

    Rigidbody wingRigidbody;
    public float airDensity = 1.3f;
    public float wingArea = 1.0f;
    public float camberLift = 0.0f;
    public float liftOffset = 0.25f;
    float liftVisual;
    public float stallThreshold = 1.5f;
    //float lastLiftMag = 0.0f;
    // public float maxLiftForce = 1000f;
  //  Queue magQueue = new Queue();
  //  public int forceInterp = 5;
  //  float maxDelta = 1.0f;
    float lastMag;
    //public float liftDamp = 0.65f;
    public float minDrag = 0.05f;

	// Use this for initialization
	void Start () {
        wingRigidbody = GetComponent<Rigidbody>();

    }
	
	void Update()
    {
        //Debug.Log(wingRigidbody.velocity.magnitude );
    }
	void FixedUpdate () {
        //thin airfoil theory:
        //center of pressure and aerodynamic center lie one quarter of the chord behind the leading edge
        //section lift coefficient = (lift at zero) + 2 * pi * angle of attack

        //lift force = 1/2 (air density) * vel^2 * wing area * lift coefficent at desired angle
        //lift is perpendicular to flow direction
        float AoA = Mathf.Deg2Rad * (Vector3.Angle(transform.up, wingRigidbody.velocity) - 90.0f);
        //Debug.Log(Vector3.Angle(transform.up, wingRigidbody.velocity));

        float liftCoefficient = camberLift + (2 * Mathf.PI * AoA);
        
        //WHAT ARE YOU DOING
        
        if (liftCoefficient > stallThreshold )
        {
            liftCoefficient = stallThreshold;
        }
        else if(liftCoefficient < stallThreshold * -1)
        {
            liftCoefficient = stallThreshold * -1;
        }
        
    
        //Debug.Log(liftCoefficient);
        //float liftMag = Mathf.Clamp(0.5f * airDensity * wingRigidbody.velocity.sqrMagnitude * wingArea * liftCoefficient, maxLiftForce * -1, maxLiftForce);
        float liftMag = 0.5f * airDensity * wingRigidbody.velocity.sqrMagnitude * wingArea * liftCoefficient;

        //we have problems with 'boundry wiggle', caused by rapid shifts in force direction. 
        //This happens when the airfoils are flying 'straight' and fast, and is possibly a problem due to simulation tickrate
        //attempt to solve it without hurting performance with a more exact simulation
        /*
        liftMag = Mathf.Lerp(lastMag, liftMag, maxDelta);

        lastMag = liftMag;
        */
        
        // Debug.Log("airflow angle: " + (Vector3.Angle(transform.up, wingRigidbody.velocity) - 90.0f) + " velocity:" + wingRigidbody.velocity.magnitude + " velocitysq:" + wingRigidbody.velocity.sqrMagnitude + " liftcoef: " + liftCoefficient + " liftmag: " + liftMag);
        
        //wingRigidbody.AddForce(wingRigidbody.transform.up * liftMag, ForceMode.Acceleration);
        liftVisual = liftMag * 0.01f;

        //drag
        wingRigidbody.AddForce(((Mathf.Abs(liftCoefficient) / 6.0f) + minDrag) * -0.5f * wingRigidbody.velocity.sqrMagnitude * wingRigidbody.velocity.normalized, ForceMode.Force);

        //lift
        wingRigidbody.AddForceAtPosition(wingRigidbody.transform.up * liftMag, wingRigidbody.position + wingRigidbody.transform.forward * liftOffset, ForceMode.Force);
        
        

    }
    void OnDrawGizmos()
    {
        if (wingRigidbody)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(wingRigidbody.position + wingRigidbody.transform.forward * liftOffset, wingRigidbody.transform.up * liftVisual);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(wingRigidbody.position, wingRigidbody.velocity);
        }

    }
}
