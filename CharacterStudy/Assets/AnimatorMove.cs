using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : MonoBehaviour {

    public Rigidbody body;
    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if(animator && body)
        {
            animator.SetFloat("Forward", body.velocity.magnitude);
            //positive is right - fix this
            animator.SetFloat("Turn", body.angularVelocity.magnitude);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
