using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attiributes")]
    [SerializeField] private int enemyPerSec;

    public int currentWave = 1;
    public float spawnInterval = 2.0f;
    private float nextSpawnTime = 0.0f;
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
        
    }
    private void SpawnEnemy()
    {
        int ex = 0;
        int ix;
        ix = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[ix];
        GameObject prefabToSpawn = enemyPrefabs[ex];
        Instantiate(prefabToSpawn, spawnPoint.position , Quaternion.identity);

    }

}
