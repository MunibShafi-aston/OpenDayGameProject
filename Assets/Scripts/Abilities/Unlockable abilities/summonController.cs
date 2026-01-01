using UnityEngine;
using System.Linq;

public class summonController : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float followDistance = 1.5f;
    public float moveSpeed = 3f;
    public float attackRange = 1f; 
    public float attackCooldown = 0.5f;

    private float attackTimer = 0f;
    private PlayerStats stats;

    public void Setup(GameObject parentObj, float dmg, float followDist)
    {
        player = parentObj;
        damage = dmg;
        followDistance = followDist;
        stats = Object.FindAnyObjectByType<PlayerStats>();
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        float dist = direction.magnitude;
        if (dist > followDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }

        attackTimer -= Time.deltaTime;

        Enemy target = FindClosestEnemy();
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= attackRange && attackTimer <= 0f)
            {
                float baseDamage = stats.DealDamage() + damage;
                float appliedDamage = target.TankDamage(baseDamage);
                Debug.Log($"{gameObject.name} dealt {appliedDamage:F1} damage to {target.name}");
                attackTimer = attackCooldown;
            }
            else
            {
                transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
        }
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        if (enemies.Length == 0) return null;
        return enemies.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).First();
    }
}
