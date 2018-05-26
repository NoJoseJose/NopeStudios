using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLeg : MonoBehaviour {

    public gameObject[] curvePoints;
    public gameObject result;
    // Use this for initialization
    float tickTime = 0;
    public float maxTime = 2;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        tickTime += Time.deltaTime;
        if(tickTime > maxTime)
        {
            tickTime = 0.0f;
        }
		
	}

    /*
    gameObject[] BezierPoint(gameObject[] points, float lambda)
    {
        //return start + lambda * (end - start);
        //maybe I should do this crap recursively

        if(points.Length == 2)
        {
            return points[0].transform.position + lambda * (points[1].transform.position - points[0].transform.position);
        }

    }
    */
}
