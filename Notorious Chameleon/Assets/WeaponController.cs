using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public CapsuleCollider Collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        GameObject hitTarget = other.gameObject;
        if (hitTarget.tag == "Enemy")
        {
            PlayerController enemyController = hitTarget.GetComponent<PlayerController>();
            enemyController.ReceiveDamage(1f, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), this.transform.forward);
        }
    }
}
