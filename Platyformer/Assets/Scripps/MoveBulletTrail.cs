using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletTrail : MonoBehaviour
{
    public int MoveSpeed = 230;
    public float DecayTime = 1f;

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        Destroy(gameObject, DecayTime);
	}
}
