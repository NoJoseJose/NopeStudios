using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontmovescript : MonoBehaviour
{
    Quaternion baseQ = Quaternion.Euler(new Vector3(90, 0, 0));
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = baseQ;
    }
}
