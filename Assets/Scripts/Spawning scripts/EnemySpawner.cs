using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnner : MonoBehaviour
{

    [Header("Spawning rules")]
    public float spawnInterval = 2f;
    public int maxEnemies = 30;
    public float spawnRadius = 8f;


    Transform player;
    float spawnTimer;
    public float gameTimer;

    public List<EnemySpawnEntry> spawnTimeline;
    public List<EnemySpawnEntry> activeEnemies=new();
    int nextUnlockIndex = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }
    
    void Update()
    {
        if(player == null) return;

        gameTimer += Time.deltaTime;
        HandleUnlocks();

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void HandleUnlocks(){
        while (nextUnlockIndex < spawnTimeline.Count && gameTimer >= spawnTimeline[nextUnlockIndex].unlockTime)
        {
            activeEnemies.Add(spawnTimeline[nextUnlockIndex]);
            nextUnlockIndex++;
        }

    }
void SpawnEnemy()
{
    if (activeEnemies.Count == 0) return;

    int totalWeight = 0;
    foreach (var entry in activeEnemies)
    {
        totalWeight += entry.weight;
    }

    int roll = Random.Range(0, totalWeight);

    foreach (var entry in activeEnemies)
    {
        if (roll < entry.weight)
        {
            Vector2 spawnPos = GetSpawnPosition();
            Instantiate(entry.enemyData.gameObjectPrefab, spawnPos, Quaternion.identity);
            return;
        }

        roll -= entry.weight;
    }
}

    Vector2 GetSpawnPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + randomDir * spawnRadius;
    }

}
