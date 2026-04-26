using UnityEngine;
using System.Collections.Generic;


public class abilityHolder : MonoBehaviour
{
    [Header("Assigned abilities")]
    public ability AbilityDash; //set to dash to Shift
    public ability Ability1; //set to E
    public ability Ability2; //set to Q
    public ability Ability3; //set to R

    [Header("Unlocked auto abilities")]   
    public Dictionary<ability, int> unlockedAbilities = new Dictionary<ability, int>();

    float[] cooldownTimes = new float[4];
    float[] activeTimes = new float[4];


    enum AbilityState { ready,active,cooldown}

    AbilityState[] states = new AbilityState[4] { AbilityState.ready, AbilityState.ready, AbilityState.ready, AbilityState.ready };


    public System.Action OnAbilitiesChanged;
    
    void Awake()
    {
        unlockedAbilities.Clear();

        for (int i = 0; i < states.Length; i++)
        {
            states[i] = AbilityState.ready;
            cooldownTimes[i] = 0f;
            activeTimes[i] = 0f;
        }
    }


    void Update()
    {
        if (pauseManager.Instance != null && pauseManager.Instance.IsPaused)
            return;

        UpdateAbility(0, AbilityDash);
        UpdateAbility(1, Ability1);
        UpdateAbility(2, Ability2);
        UpdateAbility(3, Ability3);

        foreach (var pair in unlockedAbilities)
        {
            ability abil = pair.Key;
            int stacks = pair.Value;

            if (abil != null)
                abil.Tick(Time.deltaTime, gameObject, stacks);
        }
    }

    void UpdateAbility(int index, ability ability)
    {
        if (ability == null) return;

       switch (states[index])
        {
            case AbilityState.active:
                if (activeTimes[index] > 0)
                    activeTimes[index] -= Time.deltaTime;
                else
                {
                    states[index] = AbilityState.cooldown;
                    cooldownTimes[index] = ability.cooldownTime;
                }
                break;

            case AbilityState.cooldown:
                if (cooldownTimes[index] > 0)
                    cooldownTimes[index] -= Time.deltaTime;
                else
                    states[index] = AbilityState.ready;
                break;
        }
    }


    public void OnDash() => TryUseAbility(0, AbilityDash);
    public void OnAbility1() => TryUseAbility(1, Ability1);
    public void OnAbility2() => TryUseAbility(2, Ability2);
    public void OnAbility3() => TryUseAbility(3, Ability3);
    
    
    public void TryUseAbility(int index, ability ability)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats != null && stats.isDead) return;

        if (ability == null) return;

        if (states[index] == AbilityState.ready)
        {
            if (ability != null) 
            {
            ability.Activate(gameObject);
            states[index] = AbilityState.active;
            activeTimes[index] = ability.activeTime;
            }
        }
    }
    public void AddUnlockedAbility(ability newAbility)
    {
        if (newAbility == null) return;

        if (unlockedAbilities.ContainsKey(newAbility))
        {
            unlockedAbilities[newAbility]++;
        }
        else
        {
            unlockedAbilities[newAbility] = 1;
            newAbility.Activate(gameObject); 
        }

        Debug.Log($"Ability {newAbility.name} now has {unlockedAbilities[newAbility]} stacks");
    }
    
    public float GetCooldownRemaining(int index)
    {
        return cooldownTimes[index];
    }

    public float GetTotalCooldown(int index)
    {
        ability abil = GetAbilityByIndex(index);
        return abil != null ? abil.cooldownTime : 0f;
    }

    public ability GetAbilityByIndex(int index)
{
    switch (index)
    {
        case 0: return AbilityDash;
        case 1: return Ability1;
        case 2: return Ability2;
        case 3: return Ability3;
    }
    return null;
}

    public bool IsAbilityUnlocked(ability checkAbility)
    {
        if (checkAbility == null) return false;

        if (AbilityDash == checkAbility) return true;
        if (Ability1 == checkAbility) return true;
        if (Ability2 == checkAbility) return true;
        if (Ability3 == checkAbility) return true;

        return unlockedAbilities.ContainsKey(checkAbility);
    }

    public void ResetAbilities()
    {
        unlockedAbilities.Clear();

        for (int i = 0; i < states.Length; i++)
        {
            states[i] = AbilityState.ready;
            cooldownTimes[i] = 0f;
            activeTimes[i] = 0f;
        }
    }
}
