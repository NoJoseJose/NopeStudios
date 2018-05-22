using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punchspace : MonoBehaviour {

    public JointedChainTarget punchSystem;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //Collider hitspace = this.GetComponent<SphereCollider>();
        

    }
    /*
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "punchable")
        {
            
            punchSystem.on = true;
      
        }
        
    }
    */
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "punchable")
        {
            punchSystem.on = true;
           // Debug.Log("entered");
        }

    }
    
    void OnTriggerExit(Collider collider)
    {
        punchSystem.on = false;
        
    }
}