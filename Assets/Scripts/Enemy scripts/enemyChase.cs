using UnityEngine;

public class enemyChase : MonoBehaviour
{
    public bool isDefeated = false;

    float moveSpeed;
    bool isFlying;

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


    void FixedUpdate()
    {
        if (player == null|| isDefeated)
        {
            rb.linearVelocity = Vector2.zero;
            return;

        }
        
        Vector2 direction = (player.position -transform.position).normalized;
        Vector2 velocity = direction * moveSpeed;

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(
            transform.position,
            repelRadius,
            enemyLayer
        );

        foreach (var col in nearbyEnemies)
        {
            if (col.gameObject == gameObject) continue;

            Vector2 away = (Vector2)(transform.position - col.transform.position);
            float distance = away.magnitude;

            if (distance > 0)
            {
                velocity += away.normalized * (repelStrength / distance);
            }
        }

        rb.linearVelocity = velocity;

        if (velocity.x < 0)
            spriteRenderer.flipX = true;
        else if (velocity.x > 0)
            spriteRenderer.flipX = false;
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
