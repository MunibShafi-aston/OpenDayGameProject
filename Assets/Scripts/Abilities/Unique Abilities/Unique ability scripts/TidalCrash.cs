using UnityEngine;

public class TidalCrash : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float damage;
    float pushForce;
    float lifetime;

    PlayerStats stats;

    public void Setup(Vector3 dir, float spd, float dmg, float push, float life)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
        pushForce = push;
        lifetime = life;

        stats = FindFirstObjectByType<PlayerStats>();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null) return;

        float totalDamage = damage;

        if (stats != null)
            totalDamage += stats.DealDamage();

        enemy.TankDamage(totalDamage);

        enemyChase chase = enemy.GetComponent<enemyChase>();

        if (chase != null)
        {
            Vector2 pushDir = direction.normalized;
            chase.ApplyKnockback(pushDir * pushForce, 0.25f);
        }
    }
}
