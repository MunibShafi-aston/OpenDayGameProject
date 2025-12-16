using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
    public int maxEnemies = 30;
    
    public float spawnRadius = 8f;

    Transform player;
    float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }
    
    void Update()
    {
        if(player == null) return;

        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            timer = 0f;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPosition();

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetSpawnPosition(){
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + randomDir * spawnRadius;
    }

}
