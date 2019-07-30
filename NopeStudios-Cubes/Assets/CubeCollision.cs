using UnityEngine;
using System.Collections;

public class CubeCollision : MonoBehaviour {

    Vector3 lastHit;
    Vector3 lastVelocity;
    public GameObject cubelet;
    public GameObject echsplode;
    public void Explode(Vector3 collisionVel)
    {
        //AudioSource.PlayClipAtPoint(echsplode, transform.position, 4.0f);
        GameObject bit = (GameObject)Instantiate(cubelet, gameObject.GetComponent<Rigidbody>().position, gameObject.GetComponent<Rigidbody>().rotation);
        GameObject echsploding = (GameObject)Instantiate(echsplode, gameObject.GetComponent<Rigidbody>().position, gameObject.GetComponent<Rigidbody>().rotation);
        Destroy(echsploding, 3.0f);
        Transform[] bits = bit.GetComponentsInChildren<Transform>();
        foreach (Transform t in bits)
        {
            if (t.transform != bit.transform)
            {
                t.gameObject.GetComponent<Rigidbody>().AddForce(collisionVel / 270.0f, ForceMode.Impulse);
                Destroy(t.gameObject, (Random.value * 15f) + 15f);
            }

        }
        Destroy(bit, 30f);
        Destroy(transform.parent.gameObject, 0.0f);
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Blue Boolit(Clone)" || collision.gameObject.name == "Red Boolit(Clone)")
        {

            ContactPoint contact = collision.contacts[0];
            //     Debug.Log("bonk" + Quaternion.Angle(Quaternion.LookRotation(collision.relativeVelocity), Quaternion.LookRotation(contact.normal)));
            //lastHit = contact.normal * 2;

            //lastVelocity = collision.relativeVelocity.normalized * 2;

            if (Quaternion.Angle(Quaternion.LookRotation(collision.relativeVelocity), Quaternion.LookRotation(contact.normal)) < 20 && collision.relativeVelocity.magnitude > 40)
            {
                Explode(collision.relativeVelocity);
                
            }
            else
            {
                GetComponents<AudioSource>()[1].Play();
            }
            // armor / sin (angle) = modified armor
            // ie 90 deg is 1, 45 is armor / .7
            // ke = 1/2mv^2

        }
    }
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, lastHit);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, lastVelocity);

    }
    */
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
