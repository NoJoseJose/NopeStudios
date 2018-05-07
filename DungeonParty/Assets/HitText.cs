using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitText : MonoBehaviour {
    public TextMesh textCanvas;

    // Use this for initialization
    void Start () {

        //TextMesh textCanvas = GetComponentInChildren<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        //if(collision)
        textCanvas.text = (collision.impulse.magnitude / Time.fixedDeltaTime).ToString();
        //Debug.Log(collision.impulse.magnitude / Time.fixedDeltaTime);
    }
    /*
    {
        textCanvas.text = (collision.impulse.magnitude / Time.fixedDeltaTime).ToString();
        Debug.Log(collision.impulse.magnitude / Time.fixedDeltaTime);
    }
    */
}
