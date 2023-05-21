using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudA;
    public GameObject cloudB;

    float spawnRateMin = 20f;
    float spawnRateMax = 30f;

    float spawnRate;
    float timeAfterSpawn;

    void Start()
    {
        SpawnCloud();
    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn >= spawnRate)
        {
            SpawnCloud();
        }
    }

    void SpawnCloud()
    {
        timeAfterSpawn = 0f;
        int spawn = Random.Range(0, 2);
        float x = -40f;
        float y = Random.Range(24f, 26f);
        float z = Random.Range(38f, 45f);
        Vector3 pos = new Vector3(x, y, z);
        if(spawn == 0) Instantiate(cloudA, pos, Quaternion.identity);
        else Instantiate(cloudB, pos, Quaternion.identity);
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }
}
