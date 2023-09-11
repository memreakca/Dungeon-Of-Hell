using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [Header("Refs")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attiributes")]
    [SerializeField] private float enemyPerSec;
    [SerializeField] public float startEnemy = 8;
    [SerializeField] public float difficultyScalingFactor = 0.25f;
    [SerializeField] public float BaseWaveTime = 30;

    public float WaveTime;
    public float maxEnemyPerSec = 4.5f;
    public float timeSinceLastSpawn;
    public int currentWave = 1;
    public int enemiesAlive;
    public bool isSpawning;
    private float eps;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartWave();
        WaveTime = BaseWaveTime;
    }
    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (WaveTime > 0)
        {
            WaveTime -= Time.deltaTime;
        }
        

        if (timeSinceLastSpawn >= (1f / eps) && WaveTime > 0 )
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && WaveTime <= 0 )
        {
            EndWave();
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        eps = EnemiesPerSec1();
        WaveTime = WaveTime1();
    }

    private void EndWave()
    {
        Debug.Log("WaveEnded");
        currentWave++;
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        Invoke("StartWave", 5);
    }
    private void SpawnEnemy()
    {
        int ex = 0;
        int ix;
        ix = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[ix];
        GameObject prefabToSpawn = enemyPrefabs[ex];
        Instantiate(prefabToSpawn, spawnPoint.position , Quaternion.identity);
        enemiesAlive ++;

    }

    private int WaveTime1()
    {
        return Mathf.RoundToInt(BaseWaveTime * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    private float EnemiesPerSec1()
    {
        return Mathf.Clamp( enemyPerSec * Mathf.Pow(currentWave, difficultyScalingFactor) , 0f , maxEnemyPerSec);
    }
}
