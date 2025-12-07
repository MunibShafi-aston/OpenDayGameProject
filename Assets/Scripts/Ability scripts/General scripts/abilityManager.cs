using UnityEngine;

public class abilityManager : MonoBehaviour
{
    abilityHolder holder;

    void Start()
    {
        holder = GetComponent<abilityHolder>();
    }

    void Update()
    {
        foreach (ability ab in holder.unlockedAbilities)
        {
            ab.Tick(Time.deltaTime, gameObject);
        }
    }
}