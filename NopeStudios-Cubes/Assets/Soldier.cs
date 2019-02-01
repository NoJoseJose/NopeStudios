using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {
    GameObject body;
    Rigidbody bodyRigid;
    public Vector3 moveTarget;
    Transform bodyPos;
    public float moveStrength = 6f;
    public float enemyDist = 30f;
    public float allyDist = 10f;
    GameObject[] redList;
    GameObject[] blueList;
    public GameObject closestRed = null;
    public GameObject closestRedVisible = null;
    public GameObject closestBlue = null;
    public GameObject closestBlueVisible = null;
    public float closestRedDist = Mathf.Infinity;
    public float closestBlueDist = Mathf.Infinity;
    public float closestRedDistVisible = Mathf.Infinity;
    public float closestBlueDistVisible = Mathf.Infinity;
    public Rigidbody boolit;
    public GameObject gunEnemy;
    public GameObject moveEnemy;
    public GameObject ally;
    public float reloadTime = 6;
    float shotTimer = 0;
    public int unitNumber = 0;
    public float tolerance = 0.5f;
    public Vector3 avoidanceVect;
    public GameObject squadLeader;
    public float shotSpeed = 100.0f;
    public bool inSquad = false;
    bool searchingRed = false;
    bool searchingBlue = false;
    bool searchingEnemy = false;
//    public Vector3 averageVelocity = Vector3.zero;
//    Queue velocityQueue = new Queue();

    // Use this for initialization
    void Start()
    {

        body = transform.Find("SoldierCube").gameObject;
        bodyPos = body.GetComponent<Transform>();
        bodyRigid = body.GetComponent<Rigidbody>();
        moveTarget = transform.Find("SoldierCube").position;
        //   InvokeRepeating("Shoot", Random.value, shotTime);
//        InvokeRepeating("CheckVelocity", 0.05f, 0.05f);

    }
 /*   void CheckVelocity()
    {
        velocityQueue.Enqueue(bodyRigid.velocity);
        if(velocityQueue.Count > 10)
        {
            Vector3 total = Vector3.zero;
            foreach(Vector3 vect in velocityQueue)
            {
                total += vect;
            }
            averageVelocity = total / velocityQueue.Count;
            velocityQueue.Dequeue();
        }

    }
    */
    IEnumerator WorldSearchRedNearest()
    {
        float sqrDist;
        searchingRed = true;
        redList = GameObject.FindGameObjectsWithTag("Red Team");
        if (closestRed) // if we're starting over, there's an existing we probably want to use
        {
            sqrDist = (bodyPos.position - closestRed.GetComponent<Transform>().position).sqrMagnitude;
            closestRedDist = (bodyPos.position - closestRed.GetComponent<Transform>().position).magnitude;
        }
        else
        {
            sqrDist = Mathf.Infinity;
        }
        
  //      int counter = 0;
        foreach (GameObject red in redList)
        {

            if (!closestRed) //it could die between scans
            {
                sqrDist = Mathf.Infinity;
            }
            else
            {
                sqrDist = (bodyPos.position - closestRed.GetComponent<Transform>().position).sqrMagnitude;
            }
            
            if (red && red != body && (bodyPos.position - red.GetComponent<Transform>().position).sqrMagnitude < sqrDist)
            {

                closestRed = red;
                closestRedDist = (bodyPos.position - red.GetComponent<Transform>().position).magnitude;
            }
//            counter++;

            /*if (counter % 2 == 0)
            {
                Debug.Log("red yield");
                yield return null;

            }*/
            yield return null;
        }
        searchingRed = false;
    }

    IEnumerator WorldSearchBlueNearest()
    {
        float sqrDist;
        blueList = GameObject.FindGameObjectsWithTag("Blue Team");
        searchingBlue = true;
        if (closestBlue) // if we're starting over, there's an existing we probably want to use
        {
            sqrDist = (bodyPos.position - closestBlue.GetComponent<Transform>().position).sqrMagnitude;
            closestBlueDist = (bodyPos.position - closestBlue.GetComponent<Transform>().position).magnitude;
        }
        else
        {
            sqrDist = Mathf.Infinity;
        }
        

        foreach (GameObject blue in blueList)
        {

            if (!closestBlue) //it could die between scans
            {
                sqrDist = Mathf.Infinity;
            }
            else
            {
                sqrDist = (bodyPos.position - closestBlue.GetComponent<Transform>().position).sqrMagnitude;
            }
            
            if (blue && blue != body && (bodyPos.position - blue.GetComponent<Transform>().position).sqrMagnitude < sqrDist)
            {

                closestBlue = blue;
                closestBlueDist = (bodyPos.position - blue.GetComponent<Transform>().position).magnitude;
            }

            yield return null;
        }
        searchingBlue = false;
    }

    IEnumerator WorldSearchLOSEnemy(string tag)
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag(tag);
        float sqrDistVisible = Mathf.Infinity;
        int layermask = 1 << 8;
        searchingEnemy = true;
        foreach (GameObject enemy in enemyList)
        {
            if (gunEnemy && (bodyPos.position - gunEnemy.GetComponent<Transform>().position).sqrMagnitude <= enemyDist * enemyDist * 1.1f 
                && !Physics.Linecast(bodyPos.position, gunEnemy.GetComponent<Transform>().position, layermask))
            {
                //if old is out of range or sight, throw it out
                //this means we might have one LOS check already
                sqrDistVisible = (bodyPos.position - gunEnemy.GetComponent<Transform>().position).sqrMagnitude;
            }
            else
            {
                //zero target information
                GetComponent<FireControl>().target = Vector3.zero;
                GetComponent<FireControl>().startPos = Vector3.zero;
                GetComponent<FireControl>().leadTarget = Vector3.zero;

                gunEnemy = null;
                sqrDistVisible = Mathf.Infinity;
            }
            if (enemy && (bodyPos.position - enemy.GetComponent<Transform>().position).sqrMagnitude <= enemyDist * enemyDist * 1.1f
                && (bodyPos.position - enemy.GetComponent<Transform>().position).sqrMagnitude < sqrDistVisible
                && !Physics.Linecast(bodyPos.position, enemy.GetComponent<Transform>().position, layermask))
            {
                //if this one is within range and LOS
                gunEnemy = enemy;
                //zero target information
                GetComponent<FireControl>().target = Vector3.zero;
                GetComponent<FireControl>().startPos = Vector3.zero;
                GetComponent<FireControl>().leadTarget = Vector3.zero;

                sqrDistVisible = (bodyPos.position - gunEnemy.GetComponent<Transform>().position).sqrMagnitude;
            }
            yield return null;


        }
        searchingEnemy = false;
    }
    /*
    IEnumerator WorldSearch()
    {
        int count = 0;
        while(count < 20000)
        {
            count++;
            yield return null;
        }
        StartCoroutine("WorldSearchRedNearest");
        StartCoroutine("WorldSearchBlueNearest");
            
        if (body.tag == "Blue Team")
        {
        //    StartCoroutine("WorldSearchLOSEnemy", "Red Team");
        }
        if (body.tag == "Red Team")
        {
        //    StartCoroutine("WorldSearchLOSEnemy", "Blue Team");
        }
        
    }
    */

    void FixedUpdate()
    {


        //StartCoroutine("WorldSearch");
        if (!searchingBlue)    
        {
            StartCoroutine("WorldSearchBlueNearest");
        }
        if (!searchingRed)
        {
            StartCoroutine("WorldSearchRedNearest");
        }
        if (!searchingEnemy)
        {
            if (body.tag == "Blue Team")
            {
                StartCoroutine("WorldSearchLOSEnemy", "Red Team");
            }
            if (body.tag == "Red Team")
            {
                StartCoroutine("WorldSearchLOSEnemy", "Blue Team");
            }
        }

        moveEnemy = null;
    //    gunEnemy = null;
        ally = null;
        if (body.tag == "Red Team")
        {
            if (closestBlue)
            {
                moveEnemy = closestBlue;
            }
            /*
            if(closestBlueVisible)
            {
                gunEnemy = closestBlueVisible;
            }
            */
            if (closestRed)
            {
                ally = closestRed;
            }
        }
        if (body.tag == "Blue Team")
        {
            if (closestRed)
            {
                moveEnemy = closestRed;
            }
            /*
            if (closestRedVisible)
            {
                gunEnemy = closestRedVisible;
            }
            */
            if (closestBlue)
            {
                ally = closestBlue;
            }
        }

        if (gunEnemy && gunEnemy == moveEnemy && ((gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * tolerance || (gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude > enemyDist))
        {
            //maintain distance on a target closest physical and have los
            moveTarget = gunEnemy.GetComponent<Transform>().position - ((gunEnemy.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist);
        }
        else if(gunEnemy && (gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * tolerance)
        {
            //if there's something close that can shoot us
            moveTarget = gunEnemy.GetComponent<Transform>().position - ((gunEnemy.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist * tolerance);
        }
        else if(moveEnemy)
        {
            moveTarget = bodyPos.position + (moveEnemy.GetComponent<Transform>().position - bodyPos.position).normalized * 2; //the dist here is to normalize things a bit
        }
/*
            if ((enemy.GetComponent<Transform>().position - bodyPos.position).magnitude > enemyDist || (enemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * tolerance)
            {
                moveTarget = enemy.GetComponent<Transform>().position - ((enemy.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist);
            }
        }

            if ((enemy.GetComponent<Transform>().position - bodyPos.position).magnitude > enemyDist || (enemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * tolerance)
            {
                moveTarget = enemy.GetComponent<Transform>().position - ((enemy.GetComponent<Transform>().position - bodyPos.position).normalized * enemyDist);
            }
*/
        else
        {   
            moveTarget = bodyPos.position;
        }
        

        if (ally && (ally.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
        {
            avoidanceVect = ally.GetComponent<Transform>().position - bodyPos.position;
        }
        else
        {
            avoidanceVect = new Vector3(0, 0, 0);
        }

            
        
        shotTimer += Time.deltaTime;
        if(gunEnemy && shotTimer > reloadTime)
        {
            Shoot();
            
        }
        /*
        bodyRigid.AddForceAtPosition(((moveTarget - bodyPos.position) +
                avoidanceVect.normalized * (avoidanceVect.magnitude - allyDist)
                ).normalized * moveStrength, bodyPos.position + (Vector3.up), ForceMode.Acceleration);
                */
        bodyRigid.AddForceAtPosition(((moveTarget - bodyPos.position).normalized +
            -2 * avoidanceVect.normalized
            ).normalized * moveStrength, bodyPos.position + (Vector3.up), ForceMode.Acceleration);

    }
    /*
    void Shootold()
    {
        if (gunEnemy != null && (gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * 1.10f)
        {
       
            float theta = CalcBallistic(shotSpeed, bodyPos.position, gunEnemy.GetComponent<Transform>().position);
            float dist = (gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude;
            float t = CalcBallisticTime(shotSpeed, dist, theta);
            // so the bullet is gonna have a deacceleration on the order of
            // a = 1/2 k v^2 /m
            // yeah, it's gonna change with velocity, but it's always going to be only as strong as starting velocity
            //float k = 0.5f * 0.002f / 0.1f;
            //Debug.Log("t:" + t);
            //Debug.Log("tmod:" + (Mathf.Exp(k * dist) - 1) / (k * shotSpeed));

            //float timeDif = (Mathf.Exp(k * dist) - 1) / (k * shotSpeed) / t;
            //Debug.Log("timediff" + timeDif);


            //theta = CalcBallistic(shotSpeed, bodyPos.position, newTarget + gunEnemy.GetComponentInParent<Soldier>().averageVelocity * t);

            if (gunEnemy && gunEnemy.GetComponentInParent<Soldier>())
            {
                t *= 1 + (0.5f - Random.value) * 0.4f; 
                theta = CalcBallistic(shotSpeed, bodyPos.position, gunEnemy.GetComponent<Transform>().position + gunEnemy.GetComponentInParent<Soldier>().averageVelocity * t);
            }
            //Debug.Log(t);
            //Rigidbody shot = (Rigidbody)Instantiate(boolit, bodyPos.position,
            //    Quaternion.LookRotation(enemy.GetComponent<Transform>().position + (Random.insideUnitSphere * 1) + (Vector3.up * (Random.value + 0.5f)) - bodyPos.position));
            Vector3 flatEnemy = gunEnemy.GetComponent<Transform>().position;
            flatEnemy.y = 0;
            Vector3 flatPos = bodyPos.position;
            flatPos.y = 0;
            Rigidbody shot = (Rigidbody)Instantiate(boolit, bodyPos.position,
                Quaternion.LookRotation(flatEnemy - flatPos));
            //  Debug.Log(theta);
            
            shot.transform.Rotate(-theta * Mathf.Rad2Deg, 0, 0);

            
            
            shot.AddForce((shot.GetComponent<Rigidbody>().transform.forward + Random.insideUnitSphere * 0.005f) * shotSpeed, ForceMode.VelocityChange);
            bodyRigid.AddForce((shot.GetComponent<Rigidbody>().transform.forward).normalized * -10, ForceMode.Impulse);
            //shot.AddForce((shot.GetComponent<Rigidbody>().transform.forward) * 100f, ForceMode.VelocityChange);
            shot.transform.Rotate(90, 0, 0); //turn the bullet the right way
            shotTimer = 0;
            body.GetComponents<AudioSource>()[0].Play();

        }
    }
    */
    void Shoot()
    {
        if (gunEnemy != null && (gunEnemy.GetComponent<Transform>().position - bodyPos.position).magnitude < enemyDist * 1.10f)
        {

            /*
            public float startVel = 100f;
            public float k = 0.01f;
            public float maxTime = 10f;
            public float stepTime = 0.01f;
            public Vector3 target;
            public Vector3 leadTarget;
            public Vector3 lastCalc = Vector3.zero;
            public Vector3 startPos;
            public float lastTime = 0;
            */

            GetComponent<FireControl>().target = gunEnemy.GetComponent<Transform>().position;
            GetComponent<FireControl>().startPos = bodyPos.position;
            
            if (gunEnemy && gunEnemy.GetComponentInParent<LeadInfo>())
            {
                GetComponent<FireControl>().leadTarget = gunEnemy.GetComponentInParent<LeadInfo>().averageVelocity;
            }
            GetComponent<FireControl>().SingleCalc();
            Vector3 target = GetComponent<FireControl>().lastCalc;
            if (GetComponent<FireControl>().lastTime > 0.01f)
            {
                

                Rigidbody shot = (Rigidbody)Instantiate(boolit, bodyPos.position,
                Quaternion.LookRotation(target - bodyPos.position));

                

                shot.AddForce((shot.GetComponent<Rigidbody>().transform.forward + Random.insideUnitSphere * 0.005f) * shotSpeed, ForceMode.VelocityChange);
                bodyRigid.AddForce((shot.GetComponent<Rigidbody>().transform.forward).normalized * -10, ForceMode.Impulse);

                

              //  shot.transform.Rotate(90, 0, 0); //turn the bullet the right way
                shotTimer = 0;
                body.GetComponents<AudioSource>()[0].Play();
                //zero target information
                
                GetComponent<FireControl>().target = Vector3.zero;
                GetComponent<FireControl>().startPos = Vector3.zero;
                GetComponent<FireControl>().leadTarget = Vector3.zero;
                
            }
            else
            {
                //zero target information
                GetComponent<FireControl>().target = Vector3.zero;
                GetComponent<FireControl>().startPos = Vector3.zero;
                GetComponent<FireControl>().leadTarget = Vector3.zero;
            }
            //  Debug.Log(theta);


        }
    }
    float CalcBallistic(float v, Vector3 shootPos, Vector3 targetPos)
    {
        float height = targetPos.y - shootPos.y;
        float dist = (targetPos - shootPos).magnitude;
        float g = 9.8f;
     //   Debug.Log("height: " + height + " dist: " + dist);
        //Debug.Log("radianh: " + (Mathf.Atan(((v * v) + Mathf.Sqrt((v * v * v * v) - (g * ((g * dist * dist) + (2 * height * v * v)))) / (g * dist)))));
        //Debug.Log("radianl: " + (Mathf.Atan(((v * v) - Mathf.Sqrt((v * v * v * v) - (g * ((g * dist * dist) + (2 * height * v * v)))) / (g * dist)))));
     //   Debug.Log("atan2: " + Mathf.Atan2(v * v - Mathf.Sqrt(v * v * v * v - g * (g * dist * dist + 2 * height * v * v)), (g * dist)));
        // t = d / (v cos theta)
        return Mathf.Atan2(v * v - Mathf.Sqrt(v * v * v * v - g * (g * dist * dist + 2 * height * v * v)), (g * dist));
    }
    float CalcBallisticTime(float v, float dist, float theta)
    {
        return dist / (v * Mathf.Cos(theta));
        // (v sin theta + sqrt( (v sin theta)^2 + 2 g y) ) / g
    }
    // Update is called once per frame
    void Update()
    {
        //bodyRigid.WakeUp();
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
            void OnDestroy()
    {
        if(squadLeader)
        {
            squadLeader.GetComponent<SquadLeader>().squadList.Remove(gameObject);
        }
        
    }
    void OnDrawGizmos()
    {
        if (body && body.tag == "Blue Team")
        {
            Gizmos.color = Color.cyan;
            if (moveEnemy)
            {
                Gizmos.DrawRay(bodyPos.position, ((moveTarget - bodyPos.position).normalized +
            -2 * avoidanceVect.normalized
            ).normalized * moveStrength);
            }
            Gizmos.color = Color.red;
            /*
            if (gunEnemy && gunEnemy.GetComponentInParent<Soldier>())
            {
                Gizmos.DrawRay(bodyPos.position, (gunEnemy.GetComponent<Transform>().position - bodyPos.position) + gunEnemy.GetComponentInParent<Soldier>().averageVelocity * 2);
            }
            */
            if (gunEnemy)
            {
                Gizmos.DrawRay(bodyPos.position, (gunEnemy.GetComponent<Transform>().position - bodyPos.position));
            }
        }
        
        
        /*if (closestRed)
        {

            if((closestRed.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(bodyPos.position, closestRed.GetComponent<Transform>().position - bodyPos.position);

            }
            Gizmos.color = Color.red;



        }
        */

    }

}


