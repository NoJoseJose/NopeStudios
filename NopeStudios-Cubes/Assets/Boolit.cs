using UnityEngine;
using System.Collections;

public class Boolit : MonoBehaviour {
    public float lifetime = 20.0f;
    public float dragK = 0.001f;
    public float explodeTimer = 0f;

    float timer = 0f;
    public AudioClip pip;
    bool pipped = false;
    float t = 0;
  //  Vector3 start;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
        transform.Rotate(90, 0, 0);
        //    start = gameObject.GetComponent<Rigidbody>().position;

    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(explodeTimer != 0f && timer > explodeTimer)
        {
            Explode();
        }
        if(!pipped)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
                transform.Rotate(90, 0, 0);
            }
            GetComponent<Rigidbody>().AddForce(dragK * -0.5f * vel.sqrMagnitude * vel.normalized, ForceMode.Force);
            //GetComponent<Rigidbody>).AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
            t += Time.deltaTime;
            //Debug.Log("velocity:" + vel.magnitude + "t:" + t + "expectedv: " + 2.0f * 100f * 0.1f / (100f * dragK * t + 2.0f * 0.1f));

            float x = 2.0f * 0.1f * (Mathf.Log(100f * dragK * t + 2 * 0.1f) - Mathf.Log(2f * 0.1f)) / dragK;
           // Debug.Log("dist: " + (gameObject.GetComponent<Rigidbody>().position - start).magnitude + " estdist: " + x);



            
            
        }
    }
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.5f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].name == "SoldierCube")
            {
                hitColliders[i].transform.GetComponent<CubeCollision>().Explode(GetComponent<Rigidbody>().velocity);
            }
            
            i++;
        }
        Destroy(gameObject, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Stop();
        if (!pipped)
        {
            pipped = true;
            AudioSource.PlayClipAtPoint(pip, transform.position, 1.0f);
        }
    }

   /* 
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "SoldierCube")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                //Debug.Log("bonk" + Quaternion.Angle(Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity), Quaternion.LookRotation(contact.normal)));
                if(Quaternion.Angle(Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity), Quaternion.LookRotation(contact.normal)) > 120)
                {
                    
      
                }

            }

        }
        
        
    }
   */
    // Update is called once per frame
    void Update () {
	
	}
}
