using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Throwable : MonoBehaviour
{
    private bool _hasBeenThrown;
    public Transform HandPosition;
    public float ThrowableTimeout = 5f;

    void Awake () 
	{
	    _hasBeenThrown = false;
	    Rigidbody rb = GetComponent<Rigidbody>();
	    rb.isKinematic = true;
	    rb.useGravity = false;
	    rb.velocity = new Vector3(0, 0, 0);
    }
	
	void Update () 
	{
	    if (!_hasBeenThrown && Input.GetKeyDown(KeyCode.Mouse0))
	    {
            Rigidbody rb = GetComponent<Rigidbody>();
            Debug.Log($"Throwing {rb.name}");
            Destroy(gameObject, ThrowableTimeout);
	        _hasBeenThrown = true;
	        transform.parent = null;
	        rb.isKinematic = false;
	        rb.useGravity = true;
            rb.velocity = 10 * new Vector3(1, 0, 0);
	        GameObject clone = Instantiate(this.transform, this.transform.position, this.transform.rotation).gameObject;
            clone.transform.parent = GameObject.FindGameObjectWithTag("ThrowingHand").transform;
	        
	    }
	}
}
