using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{

    public xpbar xpBar;
    public bool isDead = false;


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

    [Header("Leveling")]
    public int level = 1;
    public float currentXP = 0;
    public float xpToNextLevel = 10f;
    public float xpGrowthRate = 1.25f;


    playerHealth hpUI;


    void Awake()
    {
        InitXP();
        hpUI = GetComponent<playerHealth>();
        
        

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

            hpUI.HealthBar.setMaxHealth((int)maxHealth);
            
            Ability1 = characterData.Ability1;
            Ability2 = characterData.Ability2;
            Ability3 = characterData.Ability3;



        if (hpUI != null)
        {
            hpUI.HealthBar.setMaxHealth((int)maxHealth);  
            hpUI.HealthBar.SetHealth((int)currentHealth);  
        }
    


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
        if (isDead) return;

        float finalDamage = Mathf.Max(amount - defense, 1f);
        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        playerHealth hp = GetComponent<playerHealth>();
        if (hp != null)
            hp.UpdateUI((int)currentHealth); 

        if (currentHealth <= 0)
        {
            isDead = true;
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null)
                pc.Die();
                print("Player has died.");
        }
    }




    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        playerHealth hp = GetComponent<playerHealth>();
        if (hp != null)
            hp.HealDamage((int)amount);
    }


    public float DealDamage()
    {
        bool isCrit = Random.value < critChance;
        return damage * (isCrit ? 2f : 1f); 
    }

public void addXP(float amount)
{
    currentXP += amount;

    while (currentXP >= xpToNextLevel)
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpGrowthRate);
        LevelUp();
    }

    if (xpBar != null)
        xpBar.UpdateXP(currentXP, xpToNextLevel);
}

void LevelUp()
{
    Debug.Log("LEVEL UP! Now Level " + level);


    if (xpBar != null)
        xpBar.SetMaxXP(xpToNextLevel);

    LevelUI levelUI = FindFirstObjectByType<LevelUI>();
    if (levelUI != null)
        levelUI.UpdateLevelUI();
}

public void InitXP()
{
    if (xpBar != null)
        xpBar.SetMaxXP(xpToNextLevel);
}

}