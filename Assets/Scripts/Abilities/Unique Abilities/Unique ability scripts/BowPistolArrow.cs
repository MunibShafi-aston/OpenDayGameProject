using UnityEngine;

public class BowPistolArrow : MonoBehaviour
{
    Vector3 direction;
    float speed = 8f;
    float damage;

    public void Setup(Vector3 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;
        PlayerStats stats = FindFirstObjectByType<PlayerStats>();

        if (stats != null)
        {
            transform.localScale *= stats.projectileSizeMultiplier;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TankDamage(damage);
            Destroy(gameObject);
        }
    }
}
