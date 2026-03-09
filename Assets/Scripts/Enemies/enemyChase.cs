using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class enemyChase : MonoBehaviour
{

    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    public bool isDefeated = false;
    public bool isFrozen = false;

    public float moveSpeed;
    bool isFlying;
    bool isRanged;

    float stopDistance;
    float shootCooldown;
    float shootTimer;

    GameObject enemyProjectilePrefab;
    float projectileSpeed;

    public float repelRadius = 0.7f;
    public float repelStrength = 3f;
    public LayerMask enemyLayer;

    public float disableRepelNearPlayer = 1.2f;

    Vector2 knockbackVelocity;
    float knockbackTimer;


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
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.fixedDeltaTime;

            rb.MovePosition(rb.position + knockbackVelocity * Time.fixedDeltaTime);

            return;
        }


        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        Vector2 direction = toPlayer.normalized;

        Vector2 moveDir = Vector2.zero;

        if (!isRanged || distance > stopDistance)
        {
            moveDir = direction;

            if (distance < 2.5f)
            {
                Vector2 tangent = new Vector2(-direction.y, direction.x);

                float side = Mathf.Sign(Mathf.Sin(GetInstanceID()));

                moveDir += tangent * side * 0.6f;
            }
        }

        


        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repelRadius, enemyLayer);

        Vector2 separation = Vector2.zero;
        int neighbourCount = 0;

        foreach (var col in nearbyEnemies)
        {
            if (col.gameObject == gameObject) continue;

            Vector2 away = (Vector2)(transform.position - col.transform.position);
            float dist = away.magnitude;

            if (dist > 0 && dist < repelRadius) 
            {
                float strength = (repelRadius - dist) / repelRadius; 
                separation += away.normalized * strength; 
                neighbourCount++;
            }
        }

        if (neighbourCount > 0)
        {
            separation /= neighbourCount;
            moveDir +=  Vector2.ClampMagnitude(separation * repelStrength, 0.6f); 
        }


        moveDir = Vector2.ClampMagnitude(moveDir, 1f);

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
    else
    {
        RaycastHit2D hit = castCollisions[0];

        Vector2 slideDir = Vector2.Perpendicular(hit.normal);

        if (Vector2.Dot(slideDir, direction) < 0)
            slideDir = -slideDir;

        rb.MovePosition(rb.position + slideDir * speed * Time.fixedDeltaTime * 0.7f);
    }

    return false;
}
    public void Freeze(float duration)
    {
        if (!isFrozen)
            StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        float originalSpeed = moveSpeed;
        moveSpeed = 0f;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        isFrozen = false;
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

    public void ApplyKnockback(Vector2 force, float duration)
    {
        knockbackVelocity = force;
        knockbackTimer = duration;
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
