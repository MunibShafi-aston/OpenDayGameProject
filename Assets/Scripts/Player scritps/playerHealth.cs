using UnityEngine;
using UnityEngine.InputSystem;

public class playerHealth : MonoBehaviour
{
    public healthBar HealthBar;

    public void InitUI(int maxHealth)
    {
        HealthBar.setMaxHealth(maxHealth);
        HealthBar.SetHealth(maxHealth);
    }

    public void UpdateUI(int currentHealth)
    {
        HealthBar.SetHealth(currentHealth);
    }

    public void OnTakedamagetest(InputValue value)
    {
        if (value.isPressed)
        {
            PlayerStats stats = GetComponent<PlayerStats>();
            stats.TakeDamage(1);
            UpdateUI((int)stats.currentHealth); 
        }
    }

    public void OnHealdamagetest(InputValue value)
    {
        if (value.isPressed)
        {
            PlayerStats stats = GetComponent<PlayerStats>();
            stats.Heal(1);
            UpdateUI((int)stats.currentHealth); 
        }
    }

    public void TakeDamageExternal(int damage)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        stats.TakeDamage(damage);
        UpdateUI((int)stats.currentHealth); 
    }

    public void HealDamage(int heal)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        stats.Heal(heal);
        UpdateUI((int)stats.currentHealth);    
    }
}
