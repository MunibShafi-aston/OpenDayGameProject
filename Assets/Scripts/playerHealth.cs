using UnityEngine;
using UnityEngine.InputSystem;

public class playerHealth : MonoBehaviour
{
    public int pHealth;
    public int pMaxHealth = 10;

    public healthBar HealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pHealth = pMaxHealth;
        HealthBar.setMaxHealth(pMaxHealth);       
    }

    // Update is called once per frame
    public void OnTakedamagetest(InputValue value)
    {
        if (value.isPressed)
        {
            print("Damage pressed");
            TakeDamage(1);
        }
    }
    public void OnHealdamagetest(InputValue value)
    {
        if (value.isPressed)
        {
            print("Heal pressed");
            HealDamage(1);
        }
    }

    public void TakeDamageExternal(int damage)
{
    TakeDamage(damage);
}

    void TakeDamage(int damage)
    {
        pHealth -=damage;
        HealthBar.SetHealth(pHealth);
    }

       void HealDamage(int heal)
    {
        pHealth +=heal;
        HealthBar.SetHealth(pHealth);
    }
}



