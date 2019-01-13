using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    Rigidbody arrowBody;
    bool isFlying = true;
    // Start is called before the first frame update
    void Start()
    {
        arrowBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isFlying)
        {
            arrowBody.AddTorque(Vector3.Cross(arrowBody.transform.forward, arrowBody.velocity), ForceMode.Force);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        if(Mathf.Abs(Vector3.Dot(contact.normal, arrowBody.transform.forward))> 0.5)
        {
            this.transform.parent = collision.gameObject.transform;

            isFlying = false;
            Destroy(arrowBody);
        }

        
    }
}
