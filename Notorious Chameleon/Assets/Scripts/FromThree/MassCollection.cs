using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassCollection : MonoBehaviour
{
    public bool parented = false;
    Vector3 centerOfMass = Vector3.zero;
    public Rigidbody[] massPoints;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        if (parented)
        {
            massPoints = parent.GetComponentsInChildren<Rigidbody>();
            
        }
        else
        {
            //massPoints.SetValue(GetComponent<Rigidbody>(), 1);
            //FIX
        }
    }

    // Update is called once per frame
    void Update()
    {


        float massTotal = 0;
        //var lastCoM = centerOfMass;
        centerOfMass = Vector3.zero;

        
        foreach (Rigidbody massPoint in massPoints)
        {
            centerOfMass += massPoint.worldCenterOfMass * massPoint.mass;
            massTotal += massPoint.mass;
        }
        centerOfMass /= massTotal;

        transform.SetPositionAndRotation(centerOfMass, transform.rotation);
    }
}
