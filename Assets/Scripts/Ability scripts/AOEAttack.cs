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

     Enemy enemy = other.GetComponent<Enemy>();
      if (enemy != null)
        {
            float finalDamage = stats.DealDamage(); 
            finalDamage += damage;
            enemy.Health -= finalDamage;
             Debug.Log($"AOE dealt {finalDamage} damage to {enemy.name}");
        }
    }
}
