using UnityEngine;

public class LightningStrike : MonoBehaviour

{

    PlayerStats stats;
    float damage;
    float lifetime = 0.25f;

    public void Setup(GameObject parent, float dmg)
    {
        damage = dmg;
        stats = Object.FindAnyObjectByType<PlayerStats>();
        Destroy(gameObject, lifetime);
    }

  private void OnTriggerEnter2D(Collider2D other)
    {
     if (!other.CompareTag("Enemy")) return;

     Enemy enemy = other.GetComponentInParent<Enemy>();
    if (enemy != null)
      {
          float baseDamage = stats.DealDamage() + damage; 
          float appliedDamage = enemy.TankDamage(baseDamage);

        
          Debug.Log($"Lightning dealt {appliedDamage:F1} damage to {enemy.name} (base: {baseDamage:F1})");
      }
    }
}