using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public EnemySpawnner enemySpawner;

    [Header("Boss")]
    public GameObject bossPrefab;
    public Transform playerTransform;

    [Header("Spawn condition")]
    public float spawnTime = 300f;
    public bool spawn = true;
    public float timer;

    public float spawnDistance = 5f;

    bool bossSpawned = false;

    void Start(){
        timer = enemySpawner.gameTimer;
    }

    void Update()
    {
        if(bossSpawned)return;

        if (spawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                SpawnBoss();
            }
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)playerTransform.position + randomDir * spawnDistance;
    }

    public void SpawnBoss()
    {
        if (bossSpawned) return;

        Vector2 spawnPos = GetSpawnPosition();

        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        bossSpawned = true;

        Debug.Log("Boss spawned");
    }
    
    void OnDrawGizmosSelected()
    {
        if (playerTransform == null) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(playerTransform.position, spawnDistance);
    }

}
