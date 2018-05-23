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
	
	void Update () 
	{
	    if (!_hasBeenThrown && Input.GetKeyDown(KeyCode.Mouse0))
	    {
	        GameObject clone = Instantiate(this.transform, this.transform.position, this.transform.rotation).gameObject;
            Rigidbody rb = GetComponent<Rigidbody>();
	        //Collider cd = GetComponent<Collider>();
            Debug.Log($"Throwing {rb.name}");
	        _hasBeenThrown = true;
	        rb.isKinematic = false;
	        rb.useGravity = true;
            rb.velocity = 10*Vector3.forward;
	        //cd.enabled = true;
            Instantiate(clone);
	        clone.transform.parent = GameObject.FindGameObjectWithTag("ThrowingHand").transform;
	    }
	}
}
