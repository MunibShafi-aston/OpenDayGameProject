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
        foreach (var pair in holder.unlockedAbilities)
        {
            ability ab = pair.Key;
            int stacks = pair.Value;
            ab.Tick(Time.deltaTime, gameObject, stacks);
        }
    }
}