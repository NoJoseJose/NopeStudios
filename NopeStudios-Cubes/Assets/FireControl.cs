using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

    public float mass = 0.1f;
    public float gravity = 9.81f;
    public float startVel = 100f;
    public float k = 0.0001f;
    public float maxTime = 10f;
    public float stepTime = 0.01f;
    bool isCalc = false;
    public Vector3 target;
    public Vector3 leadTarget;
    public Vector3 lastCalc = Vector3.zero;
    public Vector3 startPos;
    public float lastTime = 0;

    Vector3 xline;
    Vector3 yline;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(!isCalc)
        {
        //    StartCoroutine("ContinualCalc");
        }
	}

    public bool SingleCalc()
    {
        float t;
        bool found = false;
        for (t = 0; t < maxTime && !found; t += stepTime)
        {
            //a superposition sphere of all possible firing angles with radius x(t)
            //will (may?) eventually collide (be within radius) with a target x(t) being acted on by various forces
            //note that gravity is up, as most things are being "held" up by the ground


            //float x = -1 * Mathf.Log(1 - startVel * k * t) / k;
            float x = 2.0f * mass * (Mathf.Log(startVel * k * t + 2f * mass) - Mathf.Log(2f * mass)) / k;
           // float x = 2.0f * 0.1f * (Mathf.Log(100f * dragK * t + 2 * 0.1f) - Mathf.Log(2f * 0.1f)) / dragK;
            Vector3 xv = target + Vector3.up * 1/2 * gravity * t * t;
            
            xv += leadTarget * t;
            // todo add other stuff to xv (like lead velocity) in respect to displacement
            Vector3 ev = startPos + (xv - startPos).normalized * x;


            //   Debug.Log("pointdist: " +(xv - startPos).magnitude);
            //  Debug.Log("rounddist: " + x);

            //so the sphere at startpos grows with t
            //we want the point where the predicted motion is just inside it
            //or the distance from start to predicted point is roughly x
            //(this distance will usually grow with t)
       //     Debug.Log("t: " + t + "fall: " + (xv - target).magnitude);

            if ((xv - startPos).magnitude < x)
            {

                found = true;
                lastTime = t;
                lastCalc = xv;
                yline = lastCalc;
                xline = ev;
            //    Debug.Log("flight t:" + t + "comp dist: " + x);
                
                return true;
                
                //   Debug.Log("pointdist: " +(xv - startPos).magnitude);
                //  Debug.Log("rounddist: " + x);

            }


        }
        return false;
    }


    //IEnumerator
    /*
    IEnumerator ContinualCalc()
    {
        isCalc = true;
        float t;
        bool found = false;
        for (t = 0; t < maxTime && !found; t += stepTime)
        {
            //a superposition sphere of all possible firing angles with radius x(t)
            //will (may?) eventually collide (be within radius) with a target x(t) being acted on by various forces
            //note that gravity is up, as most things are being "held" up by the ground
            float x = (2 * mass / k) * Mathf.Log( (k / (2 * mass)) * t + (1 / startVel));
            Vector3 xv = target + Vector3.up * gravity * t * t;
            // todo add other stuff to xv (like lead velocity) in respect to displacement
            Vector3 ev = startPos + (xv - startPos).normalized * x;
            xline = xv;
            yline = Vector3.up * gravity * t * t;
         //   Debug.Log("pointdist: " +(xv - startPos).magnitude);
          //  Debug.Log("rounddist: " + x);

            //so the sphere at startpos grows with t
            //we want the point where the predicted motion is just inside it
            //or the distance from start to predicted point is roughly x
            //(this distance will usually grow with t)
            if ((xv - ev).magnitude > 10)
            {
                
                found = true;
                lastTime = t;
                lastCalc = xv;
                yline = lastCalc;
                xline = ev;

            }
            
            yield return null; 

        }
        if(!found)
        {
          //  lastTime = 0;
          //  lastCalc = Vector3.zero;
        }

        isCalc = false;
    }
    */
    void OnDrawGizmos()
    {

 //       Gizmos.color = Color.magenta;
 //       Gizmos.DrawRay(startPos, xline - startPos);
  //      Gizmos.DrawRay(target, yline - target);

    }
}
