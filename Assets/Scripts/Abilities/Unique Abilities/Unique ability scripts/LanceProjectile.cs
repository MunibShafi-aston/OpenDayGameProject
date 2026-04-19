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

    Rigidbody2D rb;


    public void Setup(Vector3 dir, float spd, float dmg, float life, float tick)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
        duration = life;
        tickRate = tick;

        lifetime = life;
        tickTimer = tick;
        rb = GetComponent<Rigidbody2D>();


        stats = FindFirstObjectByType<PlayerStats>();
        
        if (stats != null)
        {
            transform.localScale *= stats.projectileSizeMultiplier;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.linearVelocity = direction * speed;
    }

    void Update()
    {

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);
    }

    float lastHitTime;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null) return;

        if (Time.time < lastHitTime) return;

        float baseDamage = damage + stats.DealDamage() * 0.5f;
        enemy.TankDamage(baseDamage);

        lastHitTime = Time.time + tickRate;
    }
}
