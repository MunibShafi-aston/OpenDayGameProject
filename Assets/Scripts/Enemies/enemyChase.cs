using UnityEngine;
using System.Collections.Generic;

public class enemyChase : MonoBehaviour
{

    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

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

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


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

    Vector2 moveDir = Vector2.zero;

    if (!isRanged || distance > stopDistance)
    {
        moveDir = direction;
    }

    Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repelRadius, enemyLayer);

    foreach (var col in nearbyEnemies)
    {
        if (col.gameObject == gameObject) continue;

        Vector2 away = (Vector2)(transform.position - col.transform.position);
        float dist = away.magnitude;

        if (dist > 0)
            moveDir += away.normalized * (repelStrength / dist);
    }

    moveDir = moveDir.normalized;

    bool moved = TryMove(moveDir, moveSpeed);

    if (!moved)
    {
        moved = TryMove(new Vector2(moveDir.x, 0), moveSpeed);

        if (!moved)
        {
            TryMove(new Vector2(0, moveDir.y), moveSpeed);
        }
    }

    if (isRanged && distance <= stopDistance)
    {
        shootTimer -= Time.fixedDeltaTime;

        if (shootTimer <= 0f)
        {
            Shoot(direction);
            shootTimer = shootCooldown;
        }
    }

    if (moveDir.x < 0)
        spriteRenderer.flipX = true;
    else if (moveDir.x > 0)
        spriteRenderer.flipX = false;
}

    bool TryMove(Vector2 direction, float speed)
    {
        if (direction == Vector2.zero)
            return false;

        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            speed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        }

        return false;
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
