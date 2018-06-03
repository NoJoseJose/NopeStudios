using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private bool _hasBeenThrown;
    private bool _canStartThrow;
    public Transform ThrowableParent;
    public Transform ThrowablePrefab;
    private Transform CurrentThrowable;
    public float ThrowableTimeout = 5f;

    void Start ()
    {
        StartCoroutine(CreateNewThrowable(ThrowablePrefab));
    }
    void Update()
    {
        if (!_hasBeenThrown && Input.GetKeyDown(KeyCode.Mouse0))
            _canStartThrow = true;
        ManageWeapons();
    }

    void FixedUpdate()
    {
        if (_canStartThrow)
        {
            _canStartThrow = false;
            Rigidbody rb = CurrentThrowable.GetComponent<Rigidbody>();
            Debug.Log($"Throwing {rb.name}");
            Destroy(CurrentThrowable.gameObject, ThrowableTimeout);
            _hasBeenThrown = true;
            CurrentThrowable.transform.parent = null;
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - CurrentThrowable.transform.position;
            Vector3 lookPos2D = new Vector3(lookPos.x, 0, lookPos.z).normalized;

            StartCoroutine(CreateNewThrowable(ThrowablePrefab));

            rb.AddForce(15 * lookPos2D, ForceMode.VelocityChange);
            rb.angularVelocity = new Vector3(500, 0, 0);
        }
    }

    private void ManageWeapons()
    {
        if (Input.GetKeyDown("1"))
        {

        }
    }

    private IEnumerator CreateNewThrowable(Transform t)
    {
        yield return new WaitForSeconds(.5f);
        GameObject clone = Instantiate(t, ThrowableParent, false).gameObject;
        CurrentThrowable = clone.transform;
        _hasBeenThrown = false;
    }
}
