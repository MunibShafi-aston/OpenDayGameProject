using UnityEngine;

public class enemyChase : MonoBehaviour
{
    public bool isDefeated = false;

    float moveSpeed;
    bool isFlying;
    bool isRanged;

    float stopDistance;
    float shootCooldown;
    float shootTimer;

    GameObject enemyProjectilePrefab;
    float projectileSpeed;

    public float repelRadius = 0.5f;
    public float repelStrength = 1.5f;
    public LayerMask enemyLayer;

    Transform player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetFlying(bool flying)
    {
        isFlying = flying;
    }

    public void Init(EnemyData data)
    {
        moveSpeed = data.moveSpeed;

        isFlying = data.enemyType == EnemyType.Flying;
        isRanged = data.enemyType == EnemyType.Ranged;

        if (isRanged)
        {
            stopDistance = data.attackRange;
            shootCooldown = data.attackCooldown;
            enemyProjectilePrefab = data.enemyProjectilePrefab;
            projectileSpeed = data.projectileSpeed;
        }
    }



     void FixedUpdate()
    {
        if (player == null || isDefeated)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        Vector2 direction = toPlayer.normalized;

        Vector2 velocity = Vector2.zero;

        if (!isRanged || distance > stopDistance)
        {
            velocity = direction * moveSpeed;
        }

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repelRadius, enemyLayer);

        foreach (var col in nearbyEnemies)
        {
            if (col.gameObject == gameObject) continue;

            Vector2 away = (Vector2)(transform.position - col.transform.position);
            float dist = away.magnitude;

            if (dist > 0)
                velocity += away.normalized * (repelStrength / dist);
        }

        rb.linearVelocity = velocity;

        if (isRanged && distance <= stopDistance)
        {
            shootTimer -= Time.fixedDeltaTime;

            if (shootTimer <= 0f)
            {
                Shoot(direction);
                shootTimer = shootCooldown;
            }
        }

        if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;
    }

    void Shoot(Vector2 direction)
    {
        if (enemyProjectilePrefab == null) return;

        GameObject proj = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);

        EnemyProjectile projectile = proj.GetComponent<EnemyProjectile>();
        if (projectile != null)
        {
            projectile.Init(direction, projectileSpeed, 1f);
        }
    }

    public void StopMovement()
    {
        isDefeated = true;
        rb.linearVelocity = Vector2.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, repelRadius);
    }
}
