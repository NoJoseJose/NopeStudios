using UnityEngine;
using System.Collections;

public class TankPlayerController : MonoBehaviour {

    GameObject body;
    GameObject turretBase;
    GameObject barrelBase;
    GameObject barrelEnd;
    WheelControl wheelController;
    TurretSolver turretController;
    bool isAiming;
    public Vector3 gunTarget;
    public float gunTargetDist = 100f;
    public float playerGunArc = 0f; //player's chosen elevation in space, independant of tank orientation
    public float aimMult = 1f;
    LineRenderer[] crosshairs;
    Vector3 lastTransform;
    GameObject headerIcon;
    public GameObject shotType;
    Camera gunCamera;
    int zoomLevel = 0;
    FireControl fireCalc;
    public bool playerControlled = true;


	// Use this for initialization
	void Start () {
        body = transform.parent.Find("Base").gameObject;
        turretBase = transform.parent.Find("TurretBase").gameObject;
        barrelBase = transform.parent.Find("BarrelBase").gameObject;
        wheelController = transform.parent.GetComponent<WheelControl>();
        turretController = turretBase.transform.GetComponent<TurretSolver>();
        gunTarget = turretBase.transform.position + turretBase.transform.forward * gunTargetDist;
        turretController.target = Vector3.zero;
        crosshairs = GetComponentsInChildren<LineRenderer>();
        lastTransform = turretBase.transform.position;
        barrelEnd = transform.parent.Find("Barrel").gameObject;
        headerIcon = transform.Find("TurretHeading").gameObject;
        gunCamera = turretBase.transform.Find("Camera").gameObject.GetComponent<Camera>();
        fireCalc = GetComponent<FireControl>();
        if (playerControlled)
        {
            //barrelBase.GetComponent<HingeJoint>().useMotor = true;
            turretBase.GetComponent<HingeJoint>().useMotor = true;
        }
    }

    void FixedUpdate()
    {
        
        if (playerControlled)
        {
            //this is done entirely to keep gunTarget from positional drift
            //may extend this to de-stablize yaw
            Vector3 deltaDist = turretBase.transform.position - lastTransform;
            gunTarget += deltaDist;
            lastTransform = turretBase.transform.position;

            //float x = Input.GetAxisRaw("Mouse X") * 1000f * aimMult;
            //float y = Input.GetAxisRaw("Mouse Y") * -aimMult;

            //JointMotor barrelJointMotor = barrelBase.GetComponent<HingeJoint>().motor;
            //JointMotor turretJointMotor = turretBase.GetComponent<HingeJoint>().motor;
            //barrelJointMotor.targetVelocity = y * Time.deltaTime;
            //turretJointMotor.targetVelocity = x * Time.deltaTime;
            //barrelJointMotor.freeSpin = true;
            //barrelBase.GetComponent<HingeJoint>().motor = barrelJointMotor;
            //turretBase.GetComponent<HingeJoint>().motor = turretJointMotor;

        }
        
    }

        // Update is called once per frame
    void Update () {
        float x;
        float y;
        float z;
        //we're going to keep a distance tracked from the turret
        //turning motions with the mouse will just move the location laterally
        //mousewheel changes distance
        //this way I can theoretically make bots by just controlling the target thing
        
        Quaternion orientation = Quaternion.LookRotation(gunTarget - turretBase.transform.position, turretBase.transform.up);
        if (playerControlled)
        {
            x = Input.GetAxisRaw("Mouse X");
            y = Input.GetAxisRaw("Mouse Y");
            z = Input.GetAxisRaw("Mouse ScrollWheel");
            //    playerGunArc += y * aimMult; //should clamp this

            gunTargetDist += z * 100;

      //      orientation = Quaternion.LookRotation(barrelEnd.transform.up, turretBase.transform.up); 
            //current gun orientation
            //     orientation *= Quaternion.AngleAxis(y * -aimMult, Vector3.right);
            //   orientation *= Quaternion.AngleAxis(playerGunArc, Vector3.right);
            
            orientation *= Quaternion.AngleAxis(y * -aimMult, Vector3.right);
           // gunTarget = orientation * Vector3.forward * gunTargetDist + turretBase.transform.position;

            //orientation *= Quaternion.AngleAxis(-1 * difference, Vector3.right);

            orientation *= Quaternion.AngleAxis(x * aimMult, Vector3.up);
        }
        else
        {
            
        }
        fireCalc.target = orientation * Vector3.forward * gunTargetDist + turretBase.transform.position;
        fireCalc.leadTarget = body.GetComponent<LeadInfo>().averageVelocity * -1;
        fireCalc.startPos = turretBase.transform.position;
        gunTarget = orientation * Vector3.forward * gunTargetDist + turretBase.transform.position;
        fireCalc.SingleCalc(); //should be auto
        turretController.target = fireCalc.lastCalc;
        if (playerControlled)
        {
            headerIcon.transform.position = (gunCamera.transform.position - gunTarget).normalized * -2 + gunCamera.transform.position;
            headerIcon.transform.rotation = Quaternion.LookRotation(headerIcon.transform.position - gunCamera.transform.position);

            //    Vector3 cross = cross = barrelBase.transform.up * 10f + barrelBase.transform.position;


            Vector3 cross = fireCalc.lastCalc;
            Vector3[] crossPos = new Vector3[] { cross, gunTarget };

            crosshairs[0].SetPositions(crossPos);
            //cross = barrelBase.transform.up * 30f + barrelBase.transform.position;
            //crossPos = new Vector3[] { cross, cross + Vector3.down * 200 };
            //crosshairs[1].SetPositions(crossPos);

            if (Input.GetKeyDown("e"))
            {
                zoomLevel++;
                if (zoomLevel > 4)
                    zoomLevel = 0;

                gunCamera.fieldOfView = 66f - zoomLevel * 15;
                 
            }
            if (Input.GetButton("Fire1"))
            {
                barrelEnd.GetComponent<FireScript>().Fire(shotType, fireCalc.lastTime);
            }
        }
    }

    void OnDrawGizmos()
    {

        if (turretBase)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(turretBase.transform.position, gunTarget - turretBase.transform.position);
        }

    }
}
