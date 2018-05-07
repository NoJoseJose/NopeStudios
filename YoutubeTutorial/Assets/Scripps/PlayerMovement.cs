using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public Rigidbody rb;
    public int ForwardForce;
    public int SidewaysForce;
    public bool canUpdatePlayer = true;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!canUpdatePlayer)
            return;
        float dt = Time.deltaTime;
        rb.AddForce(0, 0, ForwardForce * dt);
        if (Input.GetKey("d"))
        {
            rb.AddForce(SidewaysForce * dt, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-SidewaysForce * dt, 0, 0, ForceMode.VelocityChange);
        }
        if (rb.position.y < 0.5)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void StopPlayer()        
    {
        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
        canUpdatePlayer = false;
    }
}
