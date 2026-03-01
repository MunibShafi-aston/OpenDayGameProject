using UnityEngine;
using System.Collections.Generic;

public class BirdProjectile : MonoBehaviour
{
    private Transform player;
    private Enemy target;
    private PlayerStats stats;

    private float lifetime;
    private float bonusDamage;

    public float speed = 6f;
    public float attackCooldown = 0.5f;

    private float attackTimer;

    private static List<Enemy> birdTargets = new List<Enemy>();

    public void Setup(Transform p, Enemy t, PlayerStats s, float duration, float bonus)
    {
        player = p;
        stats = s;
        target = t;
        lifetime = duration;
        bonusDamage = bonus;

        if(target != null)
            birdTargets.Add(target);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null || target.isDead)
        {
            if(target != null)
                birdTargets.Remove(target);

            target = FindClosestEnemy();

            if(target != null)
                birdTargets.Add(target);
        }

        if (target == null) return;

        MoveTowardsTarget();

        attackTimer -= Time.deltaTime;
    }

    void MoveTowardsTarget()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;
        Enemy closest = null;

        foreach (Enemy e in enemies)
        {
            if (e.isDead) continue;

            if (birdTargets.Contains(e)) continue;

            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = e;
            }
        }

        return closest;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (target == null) return;

        Enemy e = other.GetComponentInParent<Enemy>();
        if (e == target && attackTimer <= 0f)
        {
            float baseDamage = stats.DealDamage() + bonusDamage;
            e.TankDamage(baseDamage);

            attackTimer = attackCooldown;
        }
    }

    private void OnDestroy()
    {
        if(target != null)
            birdTargets.Remove(target);
    }
}
