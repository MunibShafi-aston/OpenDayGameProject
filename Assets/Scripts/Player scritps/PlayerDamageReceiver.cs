using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour
{

    public playerHealth playerHealth;

    public void TakeDamage(float damage)
    {
        playerHealth.TakeDamageExternal((int)damage);
    }
}
