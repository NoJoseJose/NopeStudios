using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateAndDestroyObjects : NetworkBehaviour
{
    float spawnEveryXSeconds = 10;
    float lastSpawnTime;
    float destroyEveryXSeconds = 5;
    float lastDestroyTime;
    public GameObject objectToSpawn;
    int numberOfObjects = 35;
    GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = new GameObject[numberOfObjects];
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkServer.active)
        {
            if (Time.time - lastSpawnTime > spawnEveryXSeconds)
            {
                lastSpawnTime = Time.time;
                for (int i = 0; i < numberOfObjects; i++)
                {
                    GameObject newObj = GameObject.Instantiate(objectToSpawn);
                    NetworkServer.Spawn(newObj);
                    objects[i] = newObj;
                }                
            }
            if (Time.time - lastSpawnTime > destroyEveryXSeconds)
            {
                if (objects[0] != null)
                {
                    for (int i = 0; i < numberOfObjects; i++)
                    {
                        NetworkServer.Destroy(objects[i]);
                    }
                }
            }
        }
    }
}
