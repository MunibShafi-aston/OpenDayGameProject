using UnityEngine;

public class WingedImp : MonoBehaviour
{
    Transform player;
    Enemy target;

    float detectionRadius;
    float damage;

    public float moveSpeed = 4f;
    public float attackCooldown = 1f;
    float attackTimer;

    PlayerStats stats;

    public void Setup(Transform p, Enemy startingTarget, float radius, float dmg)
    {
        player = p;
        target = startingTarget;
        detectionRadius = radius;
        damage = dmg;

        stats = FindFirstObjectByType<PlayerStats>();
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        attackTimer -= Time.deltaTime;

        if (target == null || target.isDead)
        {
            target = FindClosestEnemy();
        }

        if (target != null)
        {
            MoveToTarget();
        }
        else
        {
            OrbitPlayer();
        }
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        Enemy closest = null;
        float closestDist = detectionRadius;

        foreach (Enemy e in enemies)
        {
            if (e.isDead) continue;

            float dist = Vector2.Distance(transform.position, e.transform.position);

            if (dist < closestDist)
            {
                closest = e;
                closestDist = dist;
            }
        }

        return closest;
    }

    void MoveToTarget()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, target.transform.position) < 0.6f)
        {
            if (attackTimer <= 0)
            {
                float totalDamage = damage + (stats != null ? stats.DealDamage() : 0);
                target.TankDamage(totalDamage);

                attackTimer = attackCooldown;
            }
        }
    }

    void OrbitPlayer()
    {
        Vector3 offset = new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time)) * 0.7f;

        transform.position = Vector3.Lerp(
            transform.position,
            player.position + offset,
            Time.deltaTime * 3f
        );
    }
}
