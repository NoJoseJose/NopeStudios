using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
    float reloadTime = 0f;
    bool reloading = true;
    public float shotSpeed = 150f;
    //public Rigidbody boolit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (reloadTime >= 0 && reloading)
        {
            reloadTime -= Time.deltaTime;
        }
        else if (reloading)
        {
            reloading = false;
        }
    }



    public void Fire(GameObject Shot, float explodeTime)
    {
        Rigidbody boolit = Shot.GetComponent<Rigidbody>();
        if (!reloading)
        {
            Rigidbody shot = (Rigidbody)Instantiate(boolit, transform.position, transform.rotation);
            shot.GetComponent<Boolit>().explodeTimer = explodeTime;
            shot.AddForce(transform.up * shotSpeed, ForceMode.VelocityChange);
            shot.AddForce(GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
            shot.transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
            GetComponent<Rigidbody>().AddForceAtPosition(GetComponent<Rigidbody>().transform.up * -5, GetComponent<Rigidbody>().transform.position, ForceMode.Impulse);
            GetComponents<AudioSource>()[0].Play();
            reloading = true;
            reloadTime = 2.0f;
            
        }
    }
}

