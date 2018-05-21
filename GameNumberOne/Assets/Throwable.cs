using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Throwable : MonoBehaviour
{
    private bool _hasBeenThrown;
	void Start () 
	{
		
	}
	
	void FixedUpdate () 
	{
	    if (!_hasBeenThrown && Input.GetKeyDown(KeyCode.Mouse0))
	    {
	        Rigidbody rb = GetComponent<Rigidbody>();
            Debug.Log($"Throwing {rb.name}");
	        _hasBeenThrown = true;
	        rb.isKinematic = true;
	        rb.useGravity = true;
            rb.velocity = 100*Vector3.forward;
	    }
	}
}
