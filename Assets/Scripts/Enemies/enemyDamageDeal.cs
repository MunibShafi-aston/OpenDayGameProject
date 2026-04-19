using UnityEngine;

public class enemyDamageDeal : MonoBehaviour
{
public float damage;
private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerDamageReceiver player = collision.GetComponent<PlayerDamageReceiver>();

        if (player != null)
        {
            animator.SetTrigger("Attack");
            player.TakeDamage(damage);
        }
    }
}
