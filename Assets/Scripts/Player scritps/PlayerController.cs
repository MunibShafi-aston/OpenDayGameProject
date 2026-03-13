using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 1f;
    public ContactFilter2D movementFilter;
    public swordAttack SwordAttack;
    public Vector2 movementInput { get; private set; }

    public GameObject bulletPrefab;
    public Transform firePoint; 
    public float bulletDamage = 5f;
    public float fireRate = 1f;
    public float fireTimer = 0f;

    
    PlayerStats stats;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        stats = GetComponent<PlayerStats>();
        moveSpeed = stats.moveSpeed;
    }

    void Update()
    {
        if (pauseManager.Instance != null && pauseManager.Instance.IsPaused)
        return;

        fireTimer -= Time.deltaTime;
        moveSpeed = stats.moveSpeed;
    }
    
    public bool canMove = true;
    
    private void FixedUpdate(){
        if (!canMove) return;
        
        if(movementInput != Vector2.zero){
           bool success = TryMove(movementInput);

            if (!success){
                success = TryMove(new Vector2 (movementInput.x,0));

                if (!success)
                {
                    success = TryMove(new Vector2 (0, movementInput.y));
                }
            }
            animator.SetBool("IsMoving",success);

        }
        else
        {
            animator.SetBool("IsMoving",false);
        }

        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        }else if (movementInput.x >0){
            spriteRenderer.flipX = false;

        }
    }
    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
            int count = rb.Cast(

                direction,
                movementFilter,
                castCollisions,
                moveSpeed*Time.fixedDeltaTime +collisionOffset);

                
            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
             return true;
            
            }else {
            return false;
            }
        }else
        {
            return false;
        }

    }
    
    public void OnMove(InputValue movementValue)
    {  
        movementInput = movementValue.Get<Vector2>();
    }



    public void Die()
    {
        
        canMove = false;
        //animator.SetTrigger("isDead");
        
        abilityHolder ah = GetComponent<abilityHolder>();
        if (ah != null)
            ah.enabled = false;
            print ("PlayerController: Player has died, disabling abilities.");
    
    }


public void OnAttack()
{
    if (!canMove) return;
    if(fireTimer > 0f) return;

    Shoot();

    fireTimer = fireRate/Mathf.Max(0.01f, stats.attackSpeed);
}

private void Shoot()
{
    if (bulletPrefab == null || firePoint == null) return;

    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    mousePos.z = 0f;

    Vector3 dir = mousePos - firePoint.position;

    PlayerStats stats = GetComponent<PlayerStats>(); 
    int totalProjectiles = 1 + (stats != null ? stats.extraMainProjectiles : 0); 
    float spread = 10f; 

        for (int i = 0; i < totalProjectiles; i++) 
        {
            float offset = (i - (totalProjectiles - 1) / 2f) * spread; 
            Vector3 finalDir = Quaternion.Euler(0, 0, offset) * dir; 

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet bulletComp = bullet.GetComponent<bullet>();
        if (bulletComp != null)
        {
            float damage = stats != null ? stats.DealDamage() : bulletDamage;
            bulletComp.Setup(finalDir, damage, stats);
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
