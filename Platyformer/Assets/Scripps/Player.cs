using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int fallBoundary = -20;
    public class PlayerStats
    {
        public PlayerStats()
        {
            CurrentHealth = MaxHealth;
        }
        public int MaxHealth = 100;
        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            }
        }
        private int _currentHealth;
    }

    public PlayerStats ps = new PlayerStats();

    public void DamagePlayer(int damage)
    {
        ps.CurrentHealth -= damage;
        if (ps.CurrentHealth <= 0)
        {
            GameMaster.gm.KillPlayer(this);
        }
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(Int32.MaxValue);
    }
}
