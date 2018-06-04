using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private bool _hasBeenThrown;
    private bool _canStartThrow;
    public Transform ThrowableParent;
    public List<ThrowingWeapon> ThrowableWeapons;
    private ThrowingWeapon CurrentWeapon;
    public float ThrowableTimeout = 5f;

    [Serializable]
    public class ThrowingWeapon
    {
        public int Index;
        public int ThrowForce;
        public int AngularForce;
        public float FireRate;
        public Transform Prefab;

        public Transform Instance;

        public Rigidbody RigidBody => _rb ?? (_rb = Instance.GetComponent<Rigidbody>());
        private Rigidbody _rb;

        public Transform ThrowPoint => _throwPoint ?? (_throwPoint = Instance.GetComponentInChildren<Transform>());
        private Transform _throwPoint;

        public IEnumerator CreateNewThrowable(Transform parent)
        {
            yield return new WaitForSeconds(1/FireRate);
            GameObject clone = Instantiate(Prefab, parent, false).gameObject;
            Instance = clone.transform;
            _rb = null;
            _throwPoint = null;
        }
    }

    void Start ()
    {
        CurrentWeapon = ThrowableWeapons[0];
        StartCoroutine(CurrentWeapon.CreateNewThrowable(ThrowableParent));
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
            Destroy(CurrentWeapon.Instance.gameObject, ThrowableTimeout);
            _hasBeenThrown = true;
            CurrentWeapon.Instance.parent = null;
            CurrentWeapon.RigidBody.isKinematic = false;
            CurrentWeapon.RigidBody.useGravity = true;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - CurrentWeapon.Instance.position;
            Vector3 lookPos2D = new Vector3(lookPos.x, 0, lookPos.z).normalized;

            StartCoroutine(CurrentWeapon.CreateNewThrowable(ThrowableParent));

            Vector3 throwForceVector = CurrentWeapon.ThrowForce * lookPos2D;
            if (CurrentWeapon.ThrowPoint != null)
            {
                CurrentWeapon.RigidBody.AddForceAtPosition(throwForceVector, CurrentWeapon.ThrowPoint.position, ForceMode.VelocityChange);
            }
            CurrentWeapon.RigidBody.AddForce(throwForceVector, ForceMode.VelocityChange);
            CurrentWeapon.RigidBody.angularVelocity = new Vector3(CurrentWeapon.AngularForce, 0, 0);
        }
        _hasBeenThrown = false;
    }

    private void ManageWeapons()
    {
        if (Input.GetKeyDown("1"))
        {
            if (CurrentWeapon.Index == 0)
                return;
            CurrentWeapon = ThrowableWeapons[0];
            StartCoroutine(CurrentWeapon.CreateNewThrowable(ThrowableParent));
        }
        if (Input.GetKeyDown("2"))
        {
            if (CurrentWeapon.Index == 1)
                return;
            CurrentWeapon = ThrowableWeapons[1];
            StartCoroutine(CurrentWeapon.CreateNewThrowable(ThrowableParent));
        }
    }

    
}
