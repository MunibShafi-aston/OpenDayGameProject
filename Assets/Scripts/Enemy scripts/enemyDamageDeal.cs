using UnityEngine;

public class enemyDamageDeal : MonoBehaviour
{
public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamageReceiver player = collision.GetComponent<PlayerDamageReceiver>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
