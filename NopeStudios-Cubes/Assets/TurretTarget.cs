using UnityEngine;
using System.Collections;

public class TurretTarget : MonoBehaviour {

    public Transform target;
    GameObject turretSphere;
    GameObject turretBarrel;
    GameObject turretBase;
    Transform turretTransform;
    HingeJoint sphereHinge;
    HingeJoint barrelHinge;
    Quaternion desired;
    // public float x;
    // public float y;
    bool reloading = false;
    float reloadTime;
    public Rigidbody boolit;

    // Use this for initialization
    void Start () {
        turretSphere = transform.Find("TurretSphere").gameObject;
        //turretSphere = GameObject.Find("TurretSphere");
        //turretBarrel = GameObject.Find("TurretBarrel");
        turretBarrel = transform.Find("TurretBarrel").gameObject;
        turretBase = transform.Find("TurretBase").gameObject;
        //turretBase = GameObject.Find("Turretbase");
        turretTransform = turretBase.GetComponent<Transform>();
        sphereHinge = turretSphere.GetComponent<HingeJoint>();
        barrelHinge = turretBarrel.GetComponent<HingeJoint>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        Vector3 localTarget = turretTransform.InverseTransformPoint(target.position);
        
        desired = Quaternion.LookRotation(localTarget);
        
        JointSpring sphereSpring = sphereHinge.spring;
        if (desired.eulerAngles.y > 180)
        {
            sphereSpring.targetPosition = (desired.eulerAngles.y - 360);
        }
        else
        {
            sphereSpring.targetPosition = desired.eulerAngles.y;
        }
        sphereHinge.spring = sphereSpring;        
            
            
      //  x = desired.eulerAngles.x;
        JointSpring barrelSpring = barrelHinge.spring;
        if (desired.eulerAngles.x > 180)
            barrelSpring.targetPosition = (desired.eulerAngles.x - 360);
        else
            barrelSpring.targetPosition = desired.eulerAngles.x;
       // z = desired.eulerAngles.z;
        barrelHinge.spring = barrelSpring;

        Shoot();
    }
    void Shoot()
    {
        if (!reloading)
        {
            //do gun stuff
            Rigidbody shot = (Rigidbody)Instantiate(boolit, turretBarrel.transform.position + (turretBarrel.transform.up.normalized * 1), turretBarrel.transform.rotation);
            shot.AddForce(turretBarrel.transform.up * 100, ForceMode.VelocityChange);
            //playerRigidbody.AddForceAtPosition(playerRigidbody.transform.forward * -100, playerRigidbody.transform.position, ForceMode.Impulse);

            //   Rigidbody rocketClone = (Rigidbody)Instantiate(rocket, transform.position, transform.rotation);
            // rocketClone.velocity = transform.forward * speed;
            //  rocketClone.GetComponent<MyRocketScript>().DoSomething();

            reloading = true;
            reloadTime = 2.0f;
        }
        if (reloadTime >= 0 && reloading)
        {
            reloadTime -= Time.deltaTime;
        }
        else if (reloading)
        {
            reloading = false;
        }
    }
 
}
