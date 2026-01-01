using UnityEngine;

public class fireballProjectile : MonoBehaviour
{
    PlayerStats stats;
    public float speed = 5f;
    public float lifetime = 3f;


    private Enemy target;
    private float damage;

    public void Setup(Enemy targetEnemy, float dmg)
    {
        target = targetEnemy;
        damage = dmg;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && enemy == target)
        {
            enemy.Health -= damage;
            Destroy(gameObject);
            Debug.Log($"Fireball dealt {damage} damage to {enemy.name}");

        }
    }
}