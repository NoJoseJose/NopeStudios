using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrace : MonoBehaviour
{
    public bool holding = false;
    public Vector3 startPos = Vector3.zero;
    public Vector3 endPos = Vector3.zero;

    public GameObject startThing;
    public GameObject endThing;
    public Rigidbody Arrow;
    public Rigidbody currentArrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentHit = Vector3.zero;
        RaycastHit hit;
        LayerMask plane = LayerMask.GetMask("Input");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f, plane))
        {
            
            currentHit = hit.point;
        }

        if(Input.GetMouseButtonDown(0) && !holding)
        {
            //first press
            holding = true;
            startPos = currentHit;
            SpawnArrow();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //fire
            holding = false;
            endPos = currentHit;
            Fire(startPos, endPos);
        }
        else if(holding)
        {
            //holding down
            endPos = currentHit;
            
        }       
        //visuals
        startThing.transform.position = startPos;
        endThing.transform.position = endPos;
    }

    private void Fire(Vector3 startpos, Vector3 endPos)
    {
        Vector3 velocityVector = startpos - endPos;
        currentArrow.isKinematic = false;
        currentArrow.velocity = transform.TransformDirection(velocityVector * 1);
    }
    private void SpawnArrow()
    {
        currentArrow = Instantiate(Arrow, transform.position, transform.rotation);
    }
}
