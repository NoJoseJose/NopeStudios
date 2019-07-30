using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {
    void Start()
    {
        Physics.IgnoreLayerCollision(10, 12); //blue team and their bullets
        Physics.IgnoreLayerCollision(11, 9); //red team and their bullets

        Physics.IgnoreLayerCollision(11, 13); //red/blue team and the "unshootable" layer
        Physics.IgnoreLayerCollision(12, 13);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnGUI()
    {
        if (GUILayout.Button("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
  /*      if (GUI.Button(new Rect(10, 30, 75, 20), "Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */
    }

    void Update()
    {
/*        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Requires this button to be set in the Input Manager
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */
    }
    
}
