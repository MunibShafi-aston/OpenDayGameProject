using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    float radius;
    float damage;
    float lifetime;
    
    PlayerStats stats;


    public void Setup(float r, float dmg, float life)
    {
        radius = r;
        lifetime = life;
        damage = dmg;
        stats = Object.FindAnyObjectByType<PlayerStats>();

        transform.localScale = Vector3.one * radius;

        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col !=null)
        col.radius = radius;

        Destroy(gameObject,lifetime);
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
     if (!other.CompareTag("Enemy")) return;

     Enemy enemy = other.GetComponentInParent<Enemy>();
    if (enemy != null)
      {
          float baseDamage = stats.DealDamage() + damage; 
          float appliedDamage = enemy.TankDamage(baseDamage);

        
          Debug.Log($"AOE dealt {appliedDamage:F1} damage to {enemy.name} (base: {baseDamage:F1})");
      }
    }
}
