using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnGUI()
    {
        if (GUILayout.Button("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
        // Update is called once per frame
    void Update () {
		
	}
}
