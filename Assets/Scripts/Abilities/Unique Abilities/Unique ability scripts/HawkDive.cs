using UnityEngine;

public class HawkDive : MonoBehaviour
{
    Enemy currentTarget;
    PlayerStats stats;

    public float speed = 15f;
    float radius;
    float bonusDamage;

    bool hasHit = false;

    public void Setup(Enemy target, PlayerStats playerStats, float r, float bonus)
    {
        currentTarget = target;
        stats = playerStats;
        radius = r;
        bonusDamage = bonus;
    }

    void Update()
    {
        if (hasHit) return;

        if (currentTarget == null || currentTarget.isDead)
        {
            currentTarget = FindClosestEnemy();
        }

        if (currentTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPos = currentTarget.transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        Vector3 dir = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            Impact(targetPos);
        }
    }

    void Impact(Vector3 position)
    {
        hasHit = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius);

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy == null || enemy.isDead) continue;

            float baseDamage = stats.DealDamage() + bonusDamage;
            enemy.TankDamage(baseDamage);
        }

        Destroy(gameObject);
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;
        Enemy closest = null;

        foreach (Enemy e in enemies)
        {
            if (e.isDead) continue;

            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = e;
            }
        }

        return closest;
    }
}
