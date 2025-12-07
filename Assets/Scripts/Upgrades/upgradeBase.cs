using UnityEngine;

public enum upgradeCategory {
    Weapon,
    Summon,
    Stat,
    StatusEffect,
    Ability,
    Evolution,
    Synergy,
    CharacterSpecific
}

public abstract class upgradeBase : ScriptableObject
{
    [Header("Display")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public upgradeCategory category;

    [Header("Requirements (Optional)")]
    public upgradeBase[] requiredUpgrades;

    public abstract void Apply(PlayerStats stats);
}