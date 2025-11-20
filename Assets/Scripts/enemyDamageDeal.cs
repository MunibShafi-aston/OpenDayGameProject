using UnityEngine;

public class enemyDamageDeal : MonoBehaviour
{
public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamageReceiver player = collision.GetComponent<PlayerDamageReceiver>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
