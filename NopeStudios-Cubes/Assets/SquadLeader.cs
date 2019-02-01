using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadLeader : MonoBehaviour {
    public float squadDist = 40;
    public Vector3 attentionPoint;
    public bool hasAttention;
    GameObject[] redList;
    GameObject[] blueList;
    GameObject enemy;
    public GameObject closestRed;
    public GameObject closestBlue;
    public float closestRedDist;
    public float closestBlueDist;
    int loopCount = 0;
    public float enemyDist = 200f;
    //   public float allyDist = 100f;
    public List<GameObject> squadList;
    public bool active;

    // Use this for initialization
    void Start () {
        squadList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(squadList.Count > 10)
        {
            active = true;
        }
        else
        {
            active = false;
        }
	}

    void FixedUpdate()
    {

        loopCount++;
        if ((loopCount) % 20 == 0)
        {

            enemy = null;
            redList = GameObject.FindGameObjectsWithTag("Red Team");
            blueList = GameObject.FindGameObjectsWithTag("Blue Team");
            closestRed = null;
            foreach (GameObject red in redList)
            {
                int layermask = 1 << 8;
                if (!Physics.Linecast(transform.position, red.GetComponent<Transform>().position, layermask) /*|| red.name == "Red Cylinder"*/)
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
                // RaycastHit hit;
                int layermask = 1 << 8;
                if (!Physics.Linecast(transform.position, blue.GetComponent<Transform>().position, layermask) /*|| blue.name == "Blue Cylinder"*/)
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
                if (closestBlue && closestBlueDist < enemyDist)
                {
                    enemy = closestBlue;
                }
                if (closestRed)
                {
      //              ally = closestRed;
                }
            }
            if (gameObject.tag == "Blue Overseer")
            {
                if (closestRed && closestRedDist < enemyDist)
                {
                    enemy = closestRed;
                }
                if (closestBlue)
                {
    //                ally = closestBlue;
                }
            }

            if (enemy)
            {
                hasAttention = true;
                attentionPoint = enemy.GetComponent<Transform>().position;
            }
            else
            {
                hasAttention = false;
            }
/*            if (ally && (ally.GetComponent<Transform>().position - bodyPos.position).magnitude < allyDist)
            {
                avoidanceVect = ally.GetComponent<Transform>().position - bodyPos.position;
            }
            else
            {
                avoidanceVect = new Vector3(0, 0, 0);
            }
*/


        }
    }
    void OnDrawGizmos()
    {
        if (hasAttention)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, attentionPoint - transform.position);
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
