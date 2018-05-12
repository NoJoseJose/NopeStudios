using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offsetX = 2;
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private Camera _cam;
    private Transform myTransform;

    private void Awake()
    {
        _cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (!hasALeftBuddy || !hasARightBuddy)
        {
            float camHorizontalExtend = _cam.orthographicSize * Screen.width / Screen.height;
            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            if (!hasARightBuddy && _cam.transform.position.x >= edgeVisiblePosRight - offsetX)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (!hasALeftBuddy && _cam.transform.position.x <= edgeVisiblePosRight - offsetX)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }

        }
	}

    private void MakeNewBuddy (int rightOrLeft)
    {
        Vector3 newPos = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPos, myTransform.rotation);

        if (reverseScale)
        {
            newBuddy.localScale = new Vector3(-newBuddy.localScale.x, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        else
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
    }
}
