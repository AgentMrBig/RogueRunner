using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objToSpawn;
    public bool stopSpawning = false;
    public int spawnCount = 0;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        Instantiate(objToSpawn, transform.position, transform.rotation);
        spawnCount++;
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }

}
