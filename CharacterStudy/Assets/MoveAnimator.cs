using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimator : MonoBehaviour {

    private Animator animController;
    public float walkMultiplier = 1.0f;
    public Rigidbody walkBone;
    public FootPlacer footLogic;

    public Transform chase;

	// Use this for initialization
	void Start () {
        animController = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        //animController.Update(0);
       // OnAnimatorIK();

    }
    void FixedUpdate()
    {
        //animController.SetFloat("forwardspeed", Vector3.Dot(walkBone.transform.forward, walkBone.velocity) * walkMultiplier);



    }
    void OnAnimatorIK()
    {
       // Debug.Log(footLogic.leftFoot);
        
        animController.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        //animController.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        //animController.SetIKPosition(AvatarIKGoal.LeftFoot, footLogic.leftFoot);
        animController.SetIKPosition(AvatarIKGoal.LeftFoot, chase.position);

       // animController.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
       // animController.SetIKPosition(AvatarIKGoal.LeftHand, footLogic.leftFoot);

      //  animController.SetIKHintPosition(AvatarIKHint.LeftKnee, chase.position);

        //animController.SetIKRotation(AvatarIKGoal.LeftFoot, chase.rotation);//ugghhh
        //Debug.Log(animController.GetIKPosition(AvatarIKGoal.LeftFoot));
    }
}
