using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{

    public xpbar xpBar;
    public bool isDead = false;
    public abilityHolder abilityHolder;
    public List<ability> unlockedAbilities = new List<ability>();

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
    public float projectileSizeMultiplier = 1f;
    public int extraMainProjectiles = 0;


    //public bool witherUnlocked = false;
    //public bool lifeSteal = false;

    [Header("Burn")]
    [Range(0f, 1f)] public float burnChance = 0f;
    public int burnMaxStacks = 0;
    public float burnDamagePerSecond = 0f;
    public float burnDuration = 0f;

    [Header("Freeze")]
    [Range(0f,1f)] public float freezeChance = 0f;
    public float freezeDuration = 0f;

    
    [Header("Abilities")]
    public ability Ability1;
    public ability Ability2;
    public ability Ability3;

    
    [Header("Leveling")]
    public int level = 1;
    public float currentXP = 0;
    public float xpToNextLevel = 20f;
    public float xpGrowthRate = 1.25f;

    [Header("Evolution")]
    public PlayerEvolution evolutionData;
    public bool hasEvolved = false;


    playerHealth hpUI;

    void Start()
    {
     CharacterData data = CharacterSelection.Instance.GetSelectedCharacter();

        if (data != null)
        {
            ApplyCharacterData(data);
        }
        else
        {
            Debug.LogError("No character data found! Please select a character in the character selection screen.");
        }
    }

    void Awake()
    {



        hpUI = GetComponent<playerHealth>(); 
        abilityHolder = GetComponent<abilityHolder>(); 

        ResetStatsForNewRun();
        InitXP();
    }
    void ApplyCharacterData(CharacterData data)
    {
        characterData = data;

        maxHealth = data.maxHealth;
        currentHealth = maxHealth;

        damage = data.damage;
        moveSpeed = data.moveSpeed;
        attackSpeed = data.attackSpeed;
        defense = data.defense;
        cooldownReduction = data.cooldownReduction;
        critChance = data.critChance;

        Ability1 = data.Ability1;
        Ability2 = data.Ability2;
        Ability3 = data.Ability3;

        evolutionData = data.evolvedForm;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && data.characterSprite != null)
            sr.sprite = data.characterSprite;

        Animator anim = GetComponent<Animator>();
        if(anim != null && data.animatorController != null)
            anim.runtimeAnimatorController = data.animatorController;

         if (hpUI != null && hpUI.HealthBar != null)
    {
        hpUI.HealthBar.setMaxHealth((int)maxHealth);
        hpUI.HealthBar.SetHealth((int)currentHealth);
    }

        if (abilityHolder != null)
        {
            abilityHolder.Ability1 = data.Ability1;
            abilityHolder.Ability2 = data.Ability2;
            abilityHolder.Ability3 = data.Ability3;

            abilityHolder.OnAbilitiesChanged?.Invoke();
        }

    }

     public void TakeDamage(float amount)
    {
        if (isDead) return;

        soundManager.Instance.PlaySFX("PlayerTakeDamage");
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
        xpToNextLevel = Mathf.RoundToInt((xpToNextLevel * xpGrowthRate) + (level * 5f));
        LevelUp();
    }

    if (xpBar != null)
        xpBar.UpdateXP(currentXP, xpToNextLevel);
}

void LevelUp()
{
    Debug.Log("LEVEL UP! Now Level " + level);
    soundManager.Instance.PlaySFX("LevelUp");
    if (!hasEvolved && evolutionData != null && level >= evolutionData.requiredLevel)
        {
            Evolve();
        }
    if (xpBar != null)
        xpBar.SetMaxXP(xpToNextLevel);

    LevelUI levelUI = FindFirstObjectByType<LevelUI>();
    if (levelUI != null)
        levelUI.UpdateLevelUI();

    LevelUpManager levelUpManager = FindFirstObjectByType<LevelUpManager>();
    if (levelUpManager != null)
        levelUpManager.OpenLevelUpPanel();
}

public void InitXP()
{
    if (xpBar != null)
        xpBar.SetMaxXP(xpToNextLevel);
}

void Evolve()
{
    hasEvolved = true;
    Debug.Log("EVOLVED into " + evolutionData.evolutionName);

    maxHealth += evolutionData.bonusMaxHealth;
    damage += evolutionData.bonusDamage;
    moveSpeed += evolutionData.bonusMoveSpeed;
    attackSpeed += evolutionData.bonusAttackSpeed;
    defense += evolutionData.bonusDefense;
    critChance += evolutionData.bonusCritChance;

    currentHealth = maxHealth;

    playerHealth hp = GetComponent<playerHealth>();
    if (hp != null)
        hp.HealthBar.setMaxHealth((int)maxHealth);
        
    Animator anim = GetComponent<Animator>();

    if (anim != null && evolutionData.animatorController != null)
    {
        anim.runtimeAnimatorController = evolutionData.animatorController;

        anim.Rebind();
        anim.Update(0f);
    }

    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    Debug.Log("SpriteRenderer found: " + (sr != null));
    Debug.Log("Evolved sprite assigned: " + (evolutionData.evolvedSprite != null));

    if (sr != null && evolutionData.evolvedSprite != null)
    {
        sr.sprite = evolutionData.evolvedSprite;
        Debug.Log("Sprite changed successfully");
    }
    abilityHolder holder = GetComponent<abilityHolder>();
    if (holder != null)
    {
        foreach (var ab in evolutionData.newAbilities)
            holder.AddUnlockedAbility(ab);
    }

    foreach (var ab in evolutionData.evolvedAbilities)
    {
        Debug.Log("Evolved ability: " + ab.name);
    }


}
    public void IncreaseProjectileSize(float amount)
    {
        projectileSizeMultiplier += amount;
    }


    public void AddMainProjectile()
    {
        extraMainProjectiles += 1;
    }

    public void ResetStatsForNewRun()
    {
        isDead = false;
        currentHealth = maxHealth;
        projectileSizeMultiplier = 1f;
        extraMainProjectiles = 0;
        currentXP = 0;
        level = 1;
        hasEvolved = false;

        if (unlockedAbilities != null)
            unlockedAbilities.Clear();

        if (abilityHolder != null)
        {
            abilityHolder.Ability1 = characterData?.Ability1;
            abilityHolder.Ability2 = characterData?.Ability2;
            abilityHolder.Ability3 = characterData?.Ability3;
            abilityHolder.ResetAbilities();
            abilityHolder.OnAbilitiesChanged?.Invoke();
        }

        if (xpBar != null)
            xpBar.SetMaxXP(xpToNextLevel);
    }

}