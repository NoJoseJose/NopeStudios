using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class WeaponScript : MonoBehaviour
{
    public float fireRate = 0;
    public float Damage = 10;
    public float fireDistance = 100;
    public LayerMask ToHit;
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public float effectSpawnRate = 10;

    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    private Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("There is no firepoint.");
        }
    }

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (fireRate == 0)
	    {
            if (Input.GetButtonDown("Fire1"))
	        {
	            Shoot();
	        }
	    }
	    else
	    {
	        if (Input.GetButton("Fire1") && Time.time > timeToFire)
	        {
	            timeToFire = Time.time + (1/fireRate);
	            Shoot();
	        }
	    }
	}

    private void Shoot()
    {
        Vector3 cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(cam.x, cam.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast( firePointPosition, mousePosition - firePointPosition, fireDistance, ToHit);

        //draw the particle effect
        if (Time.time >= timeToSpawnEffect)
        {
            DrawBulletTracer();
            timeToSpawnEffect = Time.time + (1 / effectSpawnRate);
        }
        
        if (hit.collider != null)
        {
            Debug.Log($"We hit {hit.collider.name} and did {Damage} damage");
        }

    }

    private void DrawBulletTracer()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = 0.01f * new Random().Next(60, 90);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
