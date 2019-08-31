using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject cubelet;
    public GameObject echsplode;
    public float CurrentHealth { get; set; }
    public float MaxHealth = 2f;
    public GameObject HealthSlider;
    public GameObject HealthText;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateUIElements();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("FireLB"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ReceiveDamage(float damageVal, Vector3 collisionPosition, Vector3 collisionvel)
    {
        if (isDead)
            return;
        CurrentHealth -= damageVal;
        UpdateUIElements();
        if (CurrentHealth <= 0)
        {
            isDead = true;
            Explode(collisionPosition, collisionvel);
            Death();
        }
    }

    private void Explode(Vector3 collisionPosition, Vector3 collisionVel)
    {
        //AudioSource.PlayClipAtPoint(echsplode, transform.position, 4.0f);
        GameObject bit = (GameObject)Instantiate(cubelet, collisionPosition, new Quaternion());
        GameObject echsploding = (GameObject)Instantiate(echsplode, collisionPosition, new Quaternion());
        Destroy(echsploding, 3.0f);
        Transform[] bits = bit.GetComponentsInChildren<Transform>();
        foreach (Transform t in bits)
        {
            if (t.transform != bit.transform)
            {
                t.gameObject.GetComponent<Rigidbody>().AddForce(collisionVel / 270.0f, ForceMode.Impulse);
                Destroy(t.gameObject, (UnityEngine.Random.value * 15f) + 15f);
            }

        }
        Destroy(bit, 30f);
        //Destroy(collidingObject, 0.0f);

        //make the gameobject a ragdoll layer
        RagdollRecursively(gameObject, LayerMask.NameToLayer("Ragdoll"));
        //if There's a move script, let's stop the force
        //EnemyMove move = gameObject.GetComponentInChildren<EnemyMove>();
        //if (move != null)
        //{
        //    move.StopMovement();
        //}
    }

    void RagdollRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;
        ConfigurableJoint joint = obj.GetComponent<ConfigurableJoint>();
        if (joint != null)
        {
            JointDrive springX = joint.angularXDrive;
            JointDrive springYZ = joint.angularYZDrive;

            springX.positionSpring = 0;
            springYZ.positionSpring = 0;
            joint.angularXDrive = springX;
            joint.angularYZDrive = springYZ;
        }
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            RagdollRecursively(child.gameObject, newLayer);
        }
    }

    private void Death()
    {
        Debug.Log("you died");
    }

    private void UpdateUIElements()
    {
        HealthText.GetComponent<Text>().text = $"{Math.Ceiling(CurrentHealth)}";
    }
}
