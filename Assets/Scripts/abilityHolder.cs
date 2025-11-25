using UnityEngine;

public class abilityHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public ability Ability;
    float cooldownTime;
    float activeTime;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;



      public void OnDash()
    {
        if (state == AbilityState.ready)
        {
            print ("dash attempted");
            Ability.Activate(gameObject);
            state = AbilityState.active;
            activeTime = Ability.activeTime;
        }
    }
    void Update()
    {
        switch (state){

            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = Ability.cooldownTime;
                }
            break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }
       
        
    }
}
