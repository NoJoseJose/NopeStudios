using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public class EnemyStats
    {
        public int Health = 100;
    }

    public Player.EnemyStats stats = new Player.EnemyStats();

    public void DamageEnemy(int damage)
    {
        stats.Health -= damage;
        if (stats.Health <= 0)
        {
            GameMaster.gm.KillEnemy(this);
        }
    }

}
