using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Unlocks/Ability Unlock")]
public class abilityUnlockUpgrade : upgradeBase
{
    public ability abilityToUnlock;

    public override void Apply(PlayerStats stats)
    {
        stats.abilityHolder.AddUnlockedAbility(abilityToUnlock);
    }
}