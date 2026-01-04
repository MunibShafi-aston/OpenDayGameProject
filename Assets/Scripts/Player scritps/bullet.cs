using UnityEngine;

public class bullet : MonoBehaviour
{
    private PlayerStats stats;
    public float speed = 10f;
    public float lifetime = 3f;
    private Vector3 direction;
    private float damage;

    public void Setup(Vector3 dir, float dmg, PlayerStats playerStats)
    {
        direction = dir.normalized;
        damage = dmg;
        stats = playerStats;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            float appliedDamage = damage; 
            enemy.TankDamage(appliedDamage);
            Debug.Log($"Bullet hit {enemy.name} for {appliedDamage} damage");

            TryApplyBurn(enemy, stats);
            TryApplyFreeze(enemy, stats);

            Destroy(gameObject); 
        }
    }

    private void TryApplyBurn(Enemy enemy, PlayerStats stats)
    {
        if (stats == null) return;

        EnemyStatus status = enemy.GetComponent<EnemyStatus>();
        if (status == null) status = enemy.gameObject.AddComponent<EnemyStatus>();

        status.ApplyBurn(stats.burnDamagePerSecond, stats.burnDuration, stats.burnMaxStacks);
    }

    private void TryApplyFreeze(Enemy enemy, PlayerStats stats)
    {
        if (stats == null) return;

        EnemyStatus status = enemy.GetComponent<EnemyStatus>();
        if (status == null) status = enemy.gameObject.AddComponent<EnemyStatus>();

        status.ApplyFreeze(stats.freezeChance, stats.freezeDuration);
    }


}
