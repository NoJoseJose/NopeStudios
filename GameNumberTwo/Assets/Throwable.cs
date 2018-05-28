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

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
	        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
	        lookPos = lookPos - transform.position;
            Vector3 lookPos2D = new Vector3(lookPos.x, 0, lookPos.z).normalized;

	        StartCoroutine(CreateNewThrowable());

            rb.velocity = 15 * lookPos2D;
            rb.angularVelocity = new Vector3(500,0,0);

            Debug.Log($"velocity: {rb.velocity}");
	        
	    }
	}

    private IEnumerator CreateNewThrowable()
    {
        GameObject clone = Instantiate(this.transform, this.transform.position, this.transform.rotation).gameObject;
        clone.transform.parent = GameObject.FindGameObjectWithTag("ThrowingHand").transform;
        yield return new WaitForSeconds(.1f);
    }
}
