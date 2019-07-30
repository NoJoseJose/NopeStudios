using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public float spawnTime = 3f;
    public int spawnCount = 0;
    public GameObject skirmisher;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void Spawn()
    {
        Instantiate(skirmisher, transform.position, transform.rotation);
        spawnCount++;
        skirmisher.GetComponent<Soldier>().unitNumber = spawnCount;
    }
}
