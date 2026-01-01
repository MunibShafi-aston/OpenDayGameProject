using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    private Vector3 direction;
    private float damage;

    public void Setup(Vector3 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            float appliedDamage = damage; 
            enemy.TankDamage(appliedDamage);
            Debug.Log($"Bullet hit {enemy.name} for {appliedDamage} damage");
            Destroy(gameObject); 
        }
    }
}
