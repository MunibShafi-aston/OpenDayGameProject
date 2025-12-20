using UnityEngine;

[CreateAssetMenu(menuName = "Spawning/Enemy Spawn Entry")]
public class EnemySpawnEntry : ScriptableObject
{
    public EnemyData enemyData;
    public float unlockTime;
    public int weight = 1;
}
