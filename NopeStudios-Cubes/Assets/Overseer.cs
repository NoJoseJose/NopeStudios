using UnityEngine;
using System.Collections;

public class Overseer : MonoBehaviour {


    public Vector3 moveTarget;
    Vector3 avoidanceVect;
    GameObject[] redList;
    GameObject[] blueList;
    GameObject enemy;
    GameObject ally;
    public GameObject closestRed;
    public GameObject closestBlue;
    public float closestRedDist;
    public float closestBlueDist;
    int loopCount = 0;
    public float enemyDist = 200f;
    public float allyDist = 100f;
   
    public float moveStrength = 4f;
    public float tolerance = 0.95f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        loopCount++;
        if ((loopCount) % 20 == 0)
        {

            enemy = null;
            redList = GameObject.FindGameObjectsWithTag("Red Overseer");
            blueList = GameObject.FindGameObjectsWithTag("Blue Overseer");
            closestRed = null;
            foreach (GameObject red in redList)
            {
            //    int layermask = 1 << 8;
            //    if (!Physics.Linecast(transform.position, red.GetComponent<Transform>().position, layermask) /*|| red.name == "Red Cylinder"*/)
                {
                    if (closestRed == null && red != gameObject)
                    {
                        closestRed = red;
                        closestRedDist = (transform.position - red.GetComponent<Transform>().position).magnitude;
                    }
                    else if (red != gameObject)
                    {
                        float dist = (transform.position - red.GetComponent<Transform>().position).magnitude;
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
             //   int layermask = 1 << 8;
             //   if (!Physics.Linecast(transform.position, blue.GetComponent<Transform>().position, layermask) /*|| blue.name == "Blue Cylinder"*/)
                {
                    if (closestBlue == null && blue != gameObject)
                    {
                        closestBlue = blue;
                        closestBlueDist = (transform.position - blue.GetComponent<Transform>().position).magnitude;
                    }
                    else if (blue != gameObject)
                    {
                        float dist = (transform.position - blue.GetComponent<Transform>().position).magnitude;
                        if (dist < closestBlueDist)
                        {
                            closestBlue = blue;
                            closestBlueDist = dist;
                        }
                    }
                }
            }

            if (gameObject.tag == "Red Overseer")
            {
                if (closestBlue)
                {
                    enemy = closestBlue;
                }
                if (closestRed)
                {
                    ally = closestRed;
                }
            }
            if (gameObject.tag == "Blue Overseer")
            {
                if (closestRed)
                {
                    enemy = closestRed;
                }
                if (closestBlue)
                {
                    ally = closestBlue;
                }
            }

            if (enemy)
            {
                //int layermask = 1 << 8;
                //if (!Physics.Linecast(transform.position, enemy.GetComponent<Transform>().position, layermask))

                if ((enemy.GetComponent<Transform>().position - transform.position).magnitude > enemyDist || (enemy.GetComponent<Transform>().position - transform.position).magnitude < enemyDist * tolerance)
                {
                    moveTarget = enemy.GetComponent<Transform>().position - ((enemy.GetComponent<Transform>().position - transform.position).normalized * enemyDist);
                }

            }
            else
            {
                moveTarget = transform.position;
            }
            if (ally && (ally.GetComponent<Transform>().position - transform.position).magnitude < allyDist)
            {
                avoidanceVect = ally.GetComponent<Transform>().position - transform.position;
            }
            else
            {
                avoidanceVect = new Vector3(0, 0, 0);
            }

        }

        GetComponent<Rigidbody>().AddForce(((moveTarget - transform.position) +
            2 * avoidanceVect.normalized * (avoidanceVect.magnitude - allyDist)
            ).normalized * moveStrength, ForceMode.Acceleration);

        RaycastHit hit;
        int layermask = 1 << 8;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 6, layermask))
        {
            float expected = hit.point.y + 6;
            GetComponent<Rigidbody>().AddForce(Vector3.up * (expected - transform.position.y) * 6, ForceMode.Acceleration);
        }

    }

    void OnDrawGizmos()
    {
        if (transform.tag == "Blue Overseer")
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, moveTarget - transform.position);
        }
    }


}
