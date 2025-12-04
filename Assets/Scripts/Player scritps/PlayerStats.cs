using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterData characterData; 
    [Header("Runtime Stats")]
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
    public float defense;
    public float cooldownReduction;
    public float critChance;

    [Header("Abilities")]
    public ability Ability1;
    public ability Ability2;
    public ability Ability3;

    void Awake()
    {
        if (characterData != null)
        {
            maxHealth = characterData.maxHealth;
            currentHealth = maxHealth;
            damage = characterData.damage;
            moveSpeed = characterData.moveSpeed;
            attackSpeed = characterData.attackSpeed;
            defense = characterData.defense;
            cooldownReduction = characterData.cooldownReduction;
            critChance = characterData.critChance;

            Ability1 = characterData.Ability1;
            Ability2 = characterData.Ability2;
            Ability3 = characterData.Ability3;

            abilityHolder abilityHolder = GetComponent<abilityHolder>();
            if (abilityHolder != null && characterData != null)
            {
                abilityHolder.Ability1 = characterData.Ability1;
                abilityHolder.Ability2 = characterData.Ability2;
                abilityHolder.Ability3 = characterData.Ability3;
            }

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null && characterData.characterSprite != null)
            {
                sr.sprite = characterData.characterSprite;
            }
            
        }
    }

    public void TakeDamage(float amount)
    {
        float finalDamage = Mathf.Max(amount - defense, 1f);
        currentHealth -= finalDamage;
        GetComponent<playerHealth>()?.TakeDamageExternal((int)finalDamage);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        GetComponent<playerHealth>()?.HealDamage((int)amount);
    }

    public float DealDamage()
    {
        bool isCrit = Random.value < critChance;
        return damage * (isCrit ? 2f : 1f); 
    }
}