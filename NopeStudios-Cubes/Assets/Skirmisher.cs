using UnityEngine;
using System.Collections;

public class Skirmisher : MonoBehaviour {

    GameObject body;
    Rigidbody bodyRigid;
    Transform moveTarget;
    Transform bodyPos;
    public float moveStrength = 6f;
    public float enemyDist = 30f;
    public float allyDist = 10f;
    public int team;
    GameObject[] redList;
    GameObject[] blueList;
    GameObject closestRed;
    GameObject closestBlue;
    public float closestRedDist;
    public float closestBlueDist;
    public Rigidbody boolit;
    GameObject enemy;
    public float shotTime;
    public int unitNumber;
    int loopCount = 0;
    public float tolerance = 0.5f;
    Vector3 avoidanceVect;

    // Use this for initialization
    void Start () {
        Physics.IgnoreLayerCollision(10, 12); //blue team and their bullets
        Physics.IgnoreLayerCollision(11, 9); //red team and their bullets
        body = transform.Find("SoldierCube").gameObject;
        bodyPos = body.GetComponent<Transform>();
        bodyRigid = body.GetComponent<Rigidbody>();
        moveTarget = transform.Find("DesiredPos").transform;
        InvokeRepeating("Shoot", Random.value, shotTime);
    }
    //get an example of InvokeRepeating() somewhere to not do some of this every frame

    void FixedUpdate()
    {
        loopCount++;
        if ((loopCount + unitNumber) % 30 == 0)
        {
            //Vector3 avoidanceVect = new Vector3(0,0,0);
            //enemy = null;
            redList = GameObject.FindGameObjectsWithTag("Red Team");
            blueList = GameObject.FindGameObjectsWithTag("Blue Team");
            closestRed = null;
            foreach (GameObject red in redList)
            {
                //RaycastHit hit;
                int layermask = 1 << 8;
                //if (!Physics.CheckCapsule(bodyPos.position, red.GetComponent<Transform>().position, 0.25f, layermask) || red.name == "Red Cylinder")
                if (!Physics.Linecast(bodyPos.position + (Vector3.down * 0.5f), red.GetComponent<Transform>().position, layermask) || red.name == "Red Cylinder")
                {
                    if (closestRed == null && red != body)
                    {
                        closestRed = red;
                        closestRedDist = (bodyPos.position - red.GetComponent<Transform>().position).magnitude;
                    }
                    else if (red != body)
                    {
                        float dist = (bodyPos.position - red.GetComponent<Transform>().position).magnitude;
                        if (dist < closestRedDist)
                        {
                            closestRed = red;
                            closestRedDist = dist;
                        }
                    }
                }
            }
            closestBlue = null;
            foreach (GameObject blue in blueList)
            {
               // RaycastHit hit;
                int layermask = 1 << 8;
                //if (!Physics.CheckCapsule(bodyPos.position, blue.GetComponent<Transform>().position, 0.25f, layermask) || blue.name == "Blue Cylinder")
                if (!Physics.Linecast(bodyPos.position + (Vector3.down * 0.5f), blue.GetComponent<Transform>().position, layermask) || blue.name == "Blue Cylinder")
                {
                    if (closestBlue == null && blue != body)
                    {
                        closestBlue = blue;
                        closestBlueDist = (bodyPos.position - blue.GetComponent<Transform>().position).magnitude;
                    }
                    else if (blue != body)
                    {
                        float dist = (bodyPos.position - blue.GetComponent<Transform>().position).magnitude;
                        if (dist < closestBlueDist)
                        {
                            closestBlue = blue;
                            closestBlueDist = dist;
                        }
                    }
                }
            }

            if (team == 0)
            {
                if (closestBlue)
                {
                    moveTarget.position = closestBlue.GetComponent<Transform>().position - ((closestBlue.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist);
                }
                if (closestRed && (closestRed.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
                {
                    avoidanceVect = closestRed.GetComponent<Transform>().position - bodyPos.position;
                }
                else
                {
                    avoidanceVect = new Vector3(0, 0, 0);
                }
                if (closestBlue && (closestBlue.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * 1.10f)
                {
                    enemy = closestBlue;

                }
            }
            else if (team == 1)
            {
                if (closestRed)
                {
                    moveTarget.position = closestRed.GetComponent<Transform>().position - ((closestRed.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist);
                }
                if (closestBlue && (closestBlue.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
                {
                    avoidanceVect = closestBlue.GetComponent<Transform>().position - bodyPos.position;
                }
                else
                {
                    avoidanceVect = new Vector3(0, 0, 0);
                }
                if (closestRed && (closestRed.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * 1.10f)
                {
                    enemy = closestRed;

                }
            }
        }
        bodyRigid.AddForceAtPosition(((moveTarget.position - bodyPos.position) +
                avoidanceVect.normalized * (avoidanceVect.magnitude - allyDist)
                ).normalized * moveStrength, bodyPos.position + (Vector3.up * 0.5f), ForceMode.Acceleration);


            //   bodyRigid.AddForceAtPosition((moveTarget.position - bodyPos.position).normalized * moveStrength, bodyPos.position + (Vector3.up * 0.5f), ForceMode.Force);

    }
    void Shoot()
    {
        if (enemy != null)
        {
            Rigidbody shot = (Rigidbody)Instantiate(boolit, bodyPos.position, 
                Quaternion.LookRotation(enemy.GetComponent<Transform>().position + (Random.insideUnitSphere) + (Vector3.up * 0.5f) - bodyPos.position));
            shot.transform.Rotate(90, 0, 0);
            shot.AddForce((enemy.GetComponent<Transform>().position - bodyPos.position).normalized * 100, ForceMode.VelocityChange);
            //Physics.IgnoreLayerCollision(10, 12); //blue team and their bullets
            //Physics.IgnoreLayerCollision(11, 9); //red team and their bullets
            //Physics.IgnoreCollision(shot.GetComponent<Collider>(), body.GetComponent<Collider>());
        }
    }
    // Update is called once per frame
    void Update () {
     //   bodyRigid.WakeUp();
    }
    /*
        public void Bonk(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "Red Boolit" || collision.gameObject.name == "Blue Boolit")
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    Debug.Log("bonk" + Quaternion.Angle(Quaternion.LookRotation(collision.gameObject.GetComponent<Rigidbody>().velocity), Quaternion.LookRotation(contact.normal)));


                        Rigidbody cubeshred = (Rigidbody)Instantiate(cubelet, bodyPos.position,
                    Quaternion.LookRotation(contact.normal));


                }

            }
        }
        */
        /*
    void OnDrawGizmos()
    {
        if (closestRed)
        {
            
            if((closestRed.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(bodyPos.position, closestRed.GetComponent<Transform>().position - bodyPos.position);
                
            }
            Gizmos.color = Color.red;

            

        }
    }
    */
}


