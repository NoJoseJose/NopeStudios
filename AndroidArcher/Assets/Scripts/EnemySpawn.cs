using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    float timeTotal = 0.0f;
    public int spawnInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTotal > spawnInterval)
        {
            Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
            timeTotal = 0.0f;
        }
        timeTotal += Time.deltaTime;
    }
}
