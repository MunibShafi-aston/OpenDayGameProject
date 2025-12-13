using UnityEngine;

[CreateAssetMenu(menuName = "Player/Evolution")]
public class PlayerEvolution : ScriptableObject
{
    public string evolutionName;

    [Header("Requirements")]
    public int requiredLevel = 10;

    [Header("Stat Bonuses")]
    public float bonusMaxHealth;
    public float bonusDamage;
    public float bonusMoveSpeed;
    public float bonusAttackSpeed;
    public float bonusDefense;
    public float bonusCritChance;

    [Header("Visuals")]
    public Sprite evolvedSprite;

    [Header("Ability Changes")]
    public ability[] newAbilities;
    public ability[] evolvedAbilities;
}
