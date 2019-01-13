using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    Rigidbody arrowBody;
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
        
        
        arrowBody.AddTorque(Vector3.Cross(arrowBody.transform.forward, arrowBody.velocity), ForceMode.Force);
    }
}
