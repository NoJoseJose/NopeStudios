using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed = 1f;
    Rigidbody playerRigidbody;
    Vector3 movement;
    public float turnSpeed = 24f;
    public float tiltSpeed = 0.8f;
    bool reloading;
    float reloadTime;
    public Rigidbody boolit;
    

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //    float x = Input.GetAxisRaw("Mouse X");
        //   float y = Input.GetAxisRaw("Mouse Y");
        //   float x = Input.GetAxisRaw("RightH");
        //    float y = Input.GetAxisRaw("RightV");
        float x = Input.GetAxisRaw("Rotation");

        //Move(h, v);
        Turning(h, v, x);

        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
        }
        if (Input.GetButton("Fire1") && !reloading)
        {
            //do gun stuff
            Rigidbody shot = (Rigidbody)Instantiate(boolit, transform.position, transform.rotation);
            shot.AddForce(transform.forward * 100, ForceMode.VelocityChange);
            playerRigidbody.AddForceAtPosition(playerRigidbody.transform.forward * -100, playerRigidbody.transform.position, ForceMode.Impulse);

            //   Rigidbody rocketClone = (Rigidbody)Instantiate(rocket, transform.position, transform.rotation);
            // rocketClone.velocity = transform.forward * speed;
            //  rocketClone.GetComponent<MyRocketScript>().DoSomething();

            reloading = true;
            reloadTime = 2.0f;
        }
        if(reloadTime >= 0 && reloading)
        {
            reloadTime -= Time.deltaTime;
        }
        else if(reloading)
        {
            reloading = false;
        }

    }

    void Move(float h, float v) {

    //    movement.Set(h, 0f, v);
     //   movement = movement.normalized * speed * Time.deltaTime;
     //   playerRigidbody.AddRelativeForce(movement.normalized * speed, ForceMode.Acceleration);

    }

    void Turning(float x, float y, float z)
    {
        playerRigidbody.AddTorque(playerRigidbody.transform.up * z * turnSpeed, ForceMode.Acceleration); //y axis roll (turning)
        playerRigidbody.AddTorque(playerRigidbody.transform.right * y * turnSpeed, ForceMode.Acceleration); //forward/back roll
        playerRigidbody.AddTorque(playerRigidbody.transform.forward * x * -turnSpeed, ForceMode.Acceleration);


    }
}
