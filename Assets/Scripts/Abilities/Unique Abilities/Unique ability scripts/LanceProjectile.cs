using UnityEngine;

public class LanceProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float damage;
    float duration;
    float tickRate;

    float lifetime;
    float tickTimer;

    PlayerStats stats;

    public void Setup(Vector3 dir, float spd, float dmg, float life, float tick)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
        duration = life;
        tickRate = tick;

        lifetime = life;
        tickTimer = tick;

        stats = FindFirstObjectByType<PlayerStats>();
        
        if (stats != null)
        {
            transform.localScale *= stats.projectileSizeMultiplier;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null) return;

        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0)
        {
            float baseDamage = stats.DealDamage() + damage;
            enemy.TankDamage(baseDamage);

            tickTimer = tickRate;
        }
    }
}
