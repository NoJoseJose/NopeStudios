using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth = 10f;
    public GameObject HealthSlider;
    public GameObject HealthText;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateUIElements();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damageVal)
    {
        CurrentHealth -= damageVal;
        UpdateUIElements();
        if (CurrentHealth <= 0)
        {
            Death();
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
