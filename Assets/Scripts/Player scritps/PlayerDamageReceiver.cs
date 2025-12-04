using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour
{
    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        if (stats == null) stats = GetComponentInChildren<PlayerStats>();
        if (stats == null) stats = GetComponentInParent<PlayerStats>();
        if (stats == null)
        {
            Debug.LogError("PlayerDamageReceiver: No PlayerStats found on this object!", this);
        }
    }

    public void TakeDamage(float damage)
    {
        if (stats != null)
        {
            stats.TakeDamage(damage);
        }
    }
}