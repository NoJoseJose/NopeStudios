using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Infinity;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
