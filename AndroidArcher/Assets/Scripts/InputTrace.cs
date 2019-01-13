using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrace : MonoBehaviour
{
    private bool holding = false;
    private bool arrowSpawned = false;
    public Vector3 startPos = Vector3.zero;
    public Vector3 endPos = Vector3.zero;

    public GameObject startThing;
    public GameObject endThing;
    public Rigidbody Arrow;
    public Rigidbody currentArrow;

    public float arrowMult = 1.0f;
    public float lifetime = 5.0f;
    public float maxDraw = 15f;
    public GameObject heroProtagonist;

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
            Vector3 diffVector = startPos - currentHit;
            if (endPos != Vector3.zero && diffVector.magnitude > maxDraw)
                currentHit = startPos + diffVector.normalized * 15;
            else
                currentHit = hit.point;
        }

        if(Input.GetMouseButtonDown(0) && !holding)
        {
            //first press
            holding = true;
            startPos = currentHit;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //fire
            holding = false;
            arrowSpawned = false;
            endPos = currentHit;
            Fire(startPos, endPos);
            startPos = Vector3.zero;
            endPos = Vector3.zero;
        }
        else if(holding)
        {
            //holding down
            endPos = currentHit;
            Aim(startPos, endPos);
        }       
        //visuals
        startThing.transform.position = startPos;
        endThing.transform.position = endPos;
        //Debug.Log($"{GetCoordinates(startThing)} , {GetCoordinates(endThing)}");
    }

    private string GetCoordinates(GameObject obj)
    {
        return $"{obj.transform.position.x}, {obj.transform.position.y}, {obj.transform.position.z}";
    }

    private void Fire(Vector3 startpos, Vector3 endPos)
    {
        Vector3 velocityVector = startpos - endPos;
        currentArrow.isKinematic = false;
        //NOPE currentArrow.velocity = transform.TransformDirection(velocityVector * 1);
        currentArrow.velocity = velocityVector * arrowMult;
        Destroy(currentArrow.gameObject, lifetime);
    }
    private void SpawnArrow()
    {
        arrowSpawned = true;
        currentArrow = Instantiate(Arrow, heroProtagonist.transform.position, transform.rotation);
    }
    private void Aim(Vector3 startpos, Vector3 endpos)
    {
        if((startPos - endPos).sqrMagnitude > 0.02)
        {
            if (!arrowSpawned)
                SpawnArrow();
            currentArrow.transform.rotation = Quaternion.LookRotation(startPos - endPos, Vector3.up);
        }
        else
        {
            if (arrowSpawned)
                currentArrow.transform.rotation = Quaternion.LookRotation(heroProtagonist.transform.forward);
        }
    }
}
