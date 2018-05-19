using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField] private StatusIndicator _statusIndicator;

    void Start()
    {
        SetIndicatorHealth();
    }

    [Serializable]
    public class EnemyStats
    {
        public EnemyStats()
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

    private void SetIndicatorHealth()
    {

        if (_statusIndicator != null)
        {
            _statusIndicator.SetHealth(stats.CurrentHealth, stats.MaxHealth);
        }
    }

    public void DamageEnemy(int damage)
    {
        stats.CurrentHealth -= damage;
        if (stats.CurrentHealth <= 0)
        {
            GameMaster.gm.KillEnemy(this);
        }
        SetIndicatorHealth();
    }

}
