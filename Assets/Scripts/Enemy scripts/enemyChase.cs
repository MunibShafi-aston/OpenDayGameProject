using UnityEngine;

public class enemyChase : MonoBehaviour
{
    public bool isDefeated = false;

    public float moveSpeed = 2f;
    
    Transform player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null|| isDefeated)
        {
            rb.linearVelocity = Vector2.zero;
            return;

        }
        
        Vector2 direction = (player.position -transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        if (direction.x <0)
            spriteRenderer.flipX = true;
        else if (direction.x > 0)
            spriteRenderer.flipX = false;        
    }

    public void StopMovement()
    {
        isDefeated = true;
        rb.linearVelocity = Vector2.zero;
    }
}
