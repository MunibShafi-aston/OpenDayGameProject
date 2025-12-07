using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Stat Upgrade")]
public class statUpgrade : upgradeBase
{
    public float healthBonus;
    public float damageBonus;
    public float moveSpeedBonus;
    public float attackSpeedBonus;
    public float critChanceBonus;

    public override void Apply(PlayerStats stats)
    {
        stats.maxHealth += healthBonus;
        stats.damage += damageBonus;
        stats.moveSpeed += moveSpeedBonus;
        stats.attackSpeed += attackSpeedBonus;
        stats.critChance += critChanceBonus;
    }
}
