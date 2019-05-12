using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float CurrentHealth { get; set; } = MaxHealth;
    public static float MaxHealth = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damageVal)
    {
        CurrentHealth -= damageVal;
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("you died");
    }
}
