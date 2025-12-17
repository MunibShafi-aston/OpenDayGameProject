using UnityEngine;

public class rangedSlashProjectile : MonoBehaviour
{ 
  Vector2 moveDirection;
    float moveSpeed;
    float life;
    float damage;

    PlayerStats stats;
    public void Setup(Vector2 dir, float speed, float dmg, float lifetime, bool flipX)
    {
        moveDirection = dir.normalized;
        moveSpeed = speed;
        life = lifetime;
        damage = dmg;

        stats = Object.FindAnyObjectByType<PlayerStats>();


        Vector3 scale = transform.localScale;
        scale.x = flipX? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale; 
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

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
            float finalDamage = stats.DealDamage(); 
            finalDamage += damage;
            enemy.Health -= finalDamage;
             Debug.Log($"Slash dealt {finalDamage} damage to {enemy.name}");
        }
    }
}

