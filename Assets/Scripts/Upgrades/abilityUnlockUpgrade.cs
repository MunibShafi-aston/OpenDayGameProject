using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Ability Unlock")]
public class abilityUnlockUpgrade : upgradeBase
{
    public ability abilityToUnlock;

    public override void Apply(PlayerStats stats)
    {
        stats.abilityHolder.AddUnlockedAbility(abilityToUnlock);
    }
}