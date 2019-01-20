using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    Rigidbody arrowBody;
    bool isFlying = true;
    float PenetrationAngle = 0.5f;
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
        var collidingObject = collision.gameObject;
        var colliderLayer = collision.gameObject.layer;

        if (colliderLayer == LayerMask.NameToLayer("Environment"))
            CheckIfPenetrated(contact, collidingObject);
        else if (colliderLayer == LayerMask.NameToLayer("EnemyActor"))
            CheckIfEnemyPenetrated(contact, collidingObject);
    }

    private void CheckIfEnemyPenetrated(ContactPoint contact, GameObject collidingObject)
    {
        CheckIfPenetrated(contact, collidingObject);
    }

    private void CheckIfPenetrated(ContactPoint contact, GameObject collidingObject)
    {
        if (Mathf.Abs(Vector3.Dot(contact.normal, arrowBody.transform.forward)) > PenetrationAngle)
        {
            this.transform.parent = collidingObject.transform;
            isFlying = false;
            Destroy(arrowBody);
        }

    }
}
