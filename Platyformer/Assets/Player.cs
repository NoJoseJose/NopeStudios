﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int fallBoundary = -20;
    public class PlayerStats
    {
        public float Health = 100f;
    }

    public PlayerStats ps = new PlayerStats();

    public void DamagePlayer(int damage)
    {
        ps.Health -= damage;
        if (ps.Health <= 0)
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
