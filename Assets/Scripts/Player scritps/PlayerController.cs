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
        animator.SetTrigger("isAttk");
    } 

    public void swordAttack()
    {
        if (!canMove) return;
        
        if(spriteRenderer.flipX == true)
        {
            SwordAttack.AttackLeft();
        }else{
            SwordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        SwordAttack.StopAttack();
    }
}
