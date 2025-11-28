using UnityEngine;

public class abilityHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public ability AbilityDash; //set to dash to Shify
    public ability Ability1; //set to slash to E
    public ability Ability2; //NA
    public ability Ability3; //NA

    float[] cooldownTimes = new float[4];
    float[] activeTimes = new float[4];

    enum AbilityState { ready,active,cooldown}

    AbilityState[] states = new AbilityState[4] { AbilityState.ready, AbilityState.ready, AbilityState.ready, AbilityState.ready };




    void Update()
     {
        UpdateAbility(0, AbilityDash);
        UpdateAbility(1, Ability1);
        UpdateAbility(2, Ability2);
        UpdateAbility(3, Ability3);
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
        if (ability == null) return;

        if (states[index] == AbilityState.ready)
        {
            ability.Activate(gameObject);
            states[index] = AbilityState.active;
            activeTimes[index] = ability.activeTime;
        }
    }
}
