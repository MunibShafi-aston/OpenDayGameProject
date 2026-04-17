using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName= "Abilities/Dash")]
public class dashAbility : ability
{

    
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public LayerMask obstacleLayer;
    public override void Activate(GameObject parent)
    {
        PlayerController movement = parent.GetComponent<PlayerController>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        Vector2 dashDirection = movement.movementInput.normalized;

       if (dashDirection.sqrMagnitude < 0.01f) 
    {
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();
        dashDirection = sr.flipX ? Vector2.left : Vector2.right;
    }
        soundManager.Instance.PlaySFX("Dash");
        movement.StartCoroutine(Dash(rb,dashDirection, movement));
    }
 private IEnumerator Dash(Rigidbody2D rb, Vector2 direction, PlayerController movement)
    {
        movement.canMove = false; 
    
        movement.animator.SetBool("isDashing", true);

        float elapsed = 0f;
        Vector2 startPos = rb.position;

        RaycastHit2D[] hits = new RaycastHit2D[5];
        int count = rb.Cast(direction, movement.movementFilter, hits, dashDistance);

        Vector2 targetPos;

        if (count > 0)
        {
            targetPos = rb.position + direction * (hits[0].distance - 0.1f);
        }
        else
        {
            targetPos = rb.position + direction * dashDistance;
        }

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos);


        movement.animator.SetBool("isDashing", false);

        movement.canMove = true;
    }
}
