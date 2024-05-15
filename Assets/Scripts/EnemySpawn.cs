using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public EnemyBehaviour enemyPrefab;
    [SerializeField] public Transform target;
    public float spawnInterval = 10f;
    public List<Transform> spawnPoints;

    private int currentSpawnPointIndex = 0;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[currentSpawnPointIndex];
        EnemyBehaviour enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.target = target;
        currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Count;
    }

}
