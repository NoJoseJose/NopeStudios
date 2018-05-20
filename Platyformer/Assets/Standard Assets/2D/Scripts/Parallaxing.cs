using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] Backgrounds;
    private float[] _parralexScales;
    public float Smoothing = 1f;

    private Transform _cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        _cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start ()
    {
        previousCamPos = _cam.position;
        _parralexScales = new float[Backgrounds.Length];
        for (int i = 0; i < Backgrounds.Length; i ++)
        {
            _parralexScales[i] = -Backgrounds[i].position.z;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - _cam.position.x) * _parralexScales[i];
            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;
            Vector3 newVec = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);
            Backgrounds[i].SetPositionAndRotation(Vector3.Lerp(newVec, Backgrounds[i].position, Smoothing * Time.deltaTime), new Quaternion(0, 0, 0, 0));
        }
        previousCamPos = _cam.position;
    }
}
