using UnityEngine;
using System.Collections;

public class controlSurfaces : MonoBehaviour {

    public float rotation = 0.0f;
    public float pitchScale = -10.0f;
    public float yawScale = 10.0f;
    public float rollScale = 10.0f;
    //which control group does this fall under?
    public bool pitchGroup = false;
    public bool yawGroup = false;
    public bool rollGroup = false;


    //public GameObject root;
    HingeJoint hinge;

    // Use this for initialization
    void Start () {
        //root = transform.Find("TurretSphere").gameObject;
        hinge = GetComponent<HingeJoint>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rollGroup)
        {
            rotation = Input.GetAxisRaw("Horizontal") * rollScale;
        }
        if (pitchGroup)
        {
            rotation = Input.GetAxisRaw("Vertical") * pitchScale;
        }
        if(yawGroup)
        {
            rotation = Input.GetAxisRaw("Vertical") * yawScale;
        }
        
    }

    void FixedUpdate()
    {
        if (hinge)
        {
            JointSpring hingeSpring = hinge.spring;
            hingeSpring.targetPosition = rotation;
            hinge.spring = hingeSpring;
        }
    }
}
