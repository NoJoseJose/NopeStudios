using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLeg : MonoBehaviour {

    public GameObject[] points;
    public GameObject resultObject;
    //public float maxTime = 2.0f;
    //public float tickTime = 0;
    //public bool active = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        

    }
    public void UpdateLeg(float lambda)
    {
        //expand this garbage later if you want to customize it. Make it recursive. Go nuts.
        if (points.Length == 4)
        {
           // var lambda = tickTime / maxTime;
            var firstInter = new Vector3[3];
            firstInter[0] = BezierPoint(points[0].transform.position, points[1].transform.position, lambda);
            firstInter[1] = BezierPoint(points[1].transform.position, points[2].transform.position, lambda);
            firstInter[2] = BezierPoint(points[2].transform.position, points[3].transform.position, lambda);
            var secondInter = new Vector3[2];
            secondInter[0] = BezierPoint(firstInter[0], firstInter[1], lambda);
            secondInter[1] = BezierPoint(firstInter[1], firstInter[2], lambda);
            //uuugh
            var result = BezierPoint(secondInter[0], secondInter[1], lambda);

            resultObject.transform.position = result;
        }

    }

    Vector3 BezierPoint(Vector3 start, Vector3 end, float lambda)
    {
        return start + (lambda * (end - start));
    }


}
