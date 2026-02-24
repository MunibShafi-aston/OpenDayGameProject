using UnityEngine;

public class FirewallSegment : MonoBehaviour
{
    float burnDamage;
    float tickRate;
    float tickTimer;

    PlayerStats stats;

    public void Setup(float duration, float dmg, float tick)
    {
        burnDamage = dmg;
        tickRate = tick;
        tickTimer = tick;

        stats = FindFirstObjectByType<PlayerStats>();

        Destroy(gameObject, duration);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null) return;

        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0)
        {
            float damage = burnDamage;

            if (stats != null)
                damage += stats.damage;

            enemy.TankDamage(damage);

            tickTimer = tickRate;
        }
    }
}
