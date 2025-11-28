using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName= "Abilities/Dash")]
public class dashAbility : ability
{

    
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;

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
        movement.StartCoroutine(Dash(rb,dashDirection, movement));
    }
 private IEnumerator Dash(Rigidbody2D rb, Vector2 direction, PlayerController movement)
    {
        movement.canMove = false; 
        float elapsed = 0f;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction * dashDistance;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos);
        movement.canMove = true;
    }
}
