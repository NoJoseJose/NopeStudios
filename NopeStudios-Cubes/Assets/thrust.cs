using UnityEngine;
using System.Collections;

public class thrust : MonoBehaviour {

    public float thrustForce = 0.0f;
    Rigidbody thrustRigidbody;

	// Use this for initialization
	void Start () {
        thrustRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        thrustRigidbody.AddForce(thrustRigidbody.transform.forward * thrustForce, ForceMode.Force);
    }
}
