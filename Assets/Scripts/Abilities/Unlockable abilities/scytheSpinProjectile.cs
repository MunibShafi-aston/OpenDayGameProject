using UnityEngine;

public class scytheSpinProjectile : MonoBehaviour
{
    private GameObject parent;
    private float radius;
    private float rotationSpeed;
    private float damage;
    private float angle; 
    private PlayerStats stats;

    public void Setup(GameObject parentObj, float orbitRadius, float rotSpeed, float dmg, float startAngle = 0f)
    {
        parent = parentObj;
        radius = orbitRadius;
        rotationSpeed = rotSpeed;
        damage = dmg;
        angle = startAngle;
        stats = Object.FindAnyObjectByType<PlayerStats>();
    }

    void Update()
    {
        if (parent == null)
        {
            Destroy(gameObject);
            return;
        }

        angle += rotationSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        transform.position = parent.transform.position + offset;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            float baseDamage = stats.DealDamage() + damage;
            float appliedDamage = enemy.TankDamage(baseDamage);
            Debug.Log($"Scythe Spin dealt {appliedDamage:F1} damage to {enemy.name} (base: {baseDamage:F1})");
        }
    }
}
