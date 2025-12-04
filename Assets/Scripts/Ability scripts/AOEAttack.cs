using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    float radius;
    float damage;
    float lifetime;

    public void Setup(float r, float dmg, float life)
    {
        radius = r;
        damage = dmg;
        lifetime = life;

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
        if(enemy != null)
        {
            enemy.Health -= damage;
        }
    }
}
