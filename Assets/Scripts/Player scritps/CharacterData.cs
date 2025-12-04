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

    
    [Header("Abilities")]
    public ability Ability1;
    public ability Ability2;
    public ability Ability3;
}