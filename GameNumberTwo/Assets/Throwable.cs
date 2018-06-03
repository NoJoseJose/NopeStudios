using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Throwable : MonoBehaviour
{
    private bool _hasBeenThrown;
    private bool _canStartThrow;
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

    void Update()
    {
        if (!_hasBeenThrown && Input.GetKeyDown(KeyCode.Mouse0))
            _canStartThrow = true;
    }
	
	void FixedUpdate () 
	{
	    //if (_canStartThrow)
	    //{
	    //    _canStartThrow = false;
     //       Rigidbody rb = GetComponent<Rigidbody>();
     //       Debug.Log($"Throwing {rb.name}");
     //       Destroy(gameObject, ThrowableTimeout);
	    //    _hasBeenThrown = true;
	    //    transform.parent = null;
	    //    rb.isKinematic = false;
	    //    rb.useGravity = true;

     //       Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
	    //    Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
	    //    lookPos = lookPos - transform.position;
     //       Vector3 lookPos2D = new Vector3(lookPos.x, 0, lookPos.z).normalized;
            
     //       rb.AddForce(15*lookPos2D, ForceMode.VelocityChange);
     //       rb.angularVelocity = new Vector3(500,0,0);


     //   }
	}


    private Vector3 CopyVector3(Vector3 input)
    {
        return new Vector3(input.x, input.y, input.z);
    }
    private Quaternion CopyQuaternion(Quaternion input)
    {
        return new Quaternion(input.x, input.y, input.z, input.w);
    }
}
