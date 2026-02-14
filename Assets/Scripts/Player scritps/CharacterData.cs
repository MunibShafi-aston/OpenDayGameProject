using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Player/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    [Header("Base Stats")]
    public float maxHealth = 10f;
    public float damage = 3f;
    public float summonDamage = 3f;
    public float moveSpeed = 4f;
    public float attackSpeed = 1f;
    public float defense = 0f;
    public float cooldownReduction = 0;
    public float critChance = 0.01f;
   
    [Header("Visuals")]
    public Sprite characterSprite;

    [Header("Description")]
    [TextArea (3,6)]
    public string characterDescription;
    
    [Header("Abilities")]
    public ability Ability1;
    public ability Ability2;
    public ability Ability3;

    [Header("Evolution")]
    public PlayerEvolution evolvedForm;    
    public string GetStatsString()
    {
        return 
        $"HP: {maxHealth}\nDMG: {damage}\nSPD: {moveSpeed}\nATK SPD: {attackSpeed}\nDEF: {defense}\nCDR: {cooldownReduction * 100}%\nCRIT: {critChance * 100}%";
    }    
}