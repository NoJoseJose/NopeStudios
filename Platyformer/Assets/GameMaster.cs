using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform PlayerPrefab;
    public Transform SpawnPoint;
    public float SpawnDelay = 1f;
    public float killParticlesTime = 3f;
    public Transform SpawnPrefab;
    public AudioClip RespawnAudio;
    public float RespawnAudioVolume = 0.5f;

    void Start()
    {
        if (gm == null)
            gm = this;
    }
    public void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public IEnumerator RespawnPlayer()
    {
        AudioSource.PlayClipAtPoint(RespawnAudio, new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z), RespawnAudioVolume);
        yield return new WaitForSeconds(SpawnDelay);
        Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        GameObject clone = Instantiate(SpawnPrefab, SpawnPoint.position, SpawnPoint.rotation).gameObject;
        Destroy(clone, killParticlesTime);
    }
}
