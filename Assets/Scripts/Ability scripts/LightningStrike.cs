using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    float damage;
    float lifetime = 0.25f;

    public void Setup(float dmg)
    {
        damage = dmg;
        Destroy(gameObject, lifetime);
    }

  private void OnTriggerEnter2D(Collider2D other)
    {
     if (!other.CompareTag("Enemy")) return;

     Enemy enemy = other.GetComponent<Enemy>();
      if (enemy != null)
          {
         enemy.Health -= damage;
        }
    }
}