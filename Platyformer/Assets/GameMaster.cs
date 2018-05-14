using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        if (gm == null)
            gm = this;
    }
    public void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
