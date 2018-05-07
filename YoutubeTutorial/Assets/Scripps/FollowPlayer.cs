using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    public Vector3 CameraOffset;

	// Update is called once per frame
	void Update ()
    {
        transform.position = PlayerTransform.position + CameraOffset;
	}
}
