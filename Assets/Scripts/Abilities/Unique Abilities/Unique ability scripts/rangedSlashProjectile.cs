using UnityEngine;

public class rangedSlashProjectile : MonoBehaviour
{
    Vector2 moveDirection;
    float moveSpeed;
    float life;
    float damage;

    PlayerStats stats;

    public void Setup(Vector2 dir, float speed, float dmg, float lifetime)
    {
        moveDirection = dir.normalized;
        moveSpeed = speed;
        damage = dmg;
        life = lifetime;

        stats = Object.FindAnyObjectByType<PlayerStats>();

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 45f);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;

        life -= Time.deltaTime;

        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            float baseDamage = stats.DealDamage() + damage;
            float appliedDamage = enemy.TankDamage(baseDamage);

            Debug.Log($"Slash dealt {appliedDamage:F1} damage to {enemy.name}");
        }
    }
}
