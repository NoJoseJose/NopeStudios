using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 10f;

    public Vector3 offset;
   // Quaternion viewAngle;
    public float rotateSpeed = 2f;


    void Start()
    {
        //offset = transform.position - target.position;

    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        float y = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
        transform.Rotate(-y, x, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);


        Vector3 targetCamPos = target.position - (transform.rotation * offset);

        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.position = targetCamPos;

        //transform.LookAt(target.transform);

    }
}
