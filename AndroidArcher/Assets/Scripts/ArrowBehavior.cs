using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    Rigidbody arrowBody;
    bool isFlying = true;
    float PenetrationAngle = 0.5f;
    bool hasPenetrated = false;
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
        if (hasPenetrated)
            return;
        ContactPoint contact = collision.contacts[0];
        var collidingObject = collision.gameObject;
        var colliderLayer = collision.gameObject.layer;

        if (colliderLayer == LayerMask.NameToLayer("Environment"))
            CheckIfPenetrated(contact, collidingObject);
        else if (colliderLayer == LayerMask.NameToLayer("EnemyActor"))
            CheckIfEnemyPenetrated(contact, collidingObject, collision);
    }

    private void CheckIfEnemyPenetrated(ContactPoint contact, GameObject collidingObject, Collision collision)
    {
        if (CheckIfPenetrated(contact, collidingObject))
        {
            //find parent root gameobject of collider

            //send info to enemy manager
            EnemyManager mgr = collidingObject.GetComponentInParent<EnemyManager>();
            mgr.ReportHit(collidingObject, collision.relativeVelocity);
        }
    }

    private bool CheckIfPenetrated(ContactPoint contact, GameObject collidingObject)
    {
        
        bool penetrated = false;
        if (Mathf.Abs(Vector3.Dot(contact.normal, arrowBody.transform.forward)) > PenetrationAngle)
        {
            hasPenetrated = true;
            penetrated = true;
            GameObject arrowHolder = new GameObject("ArrowHolder");
            
            arrowHolder.transform.parent = collidingObject.transform;
            this.transform.parent = arrowHolder.transform;
            isFlying = false;
            Destroy(arrowBody);
        }
        return penetrated;
    }
}
