using UnityEngine;

public enum EnemyType
{
    Melee,
    Ranged,
    Flying,
    Tank,
    Bomber
}

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("Stats")]
    public float maxHealth = 10f;
    public float moveSpeed = 2f;
    public float contactDamage = 1f;

    [Header("Ranged")]
    public float attackRange = 5f;
    public float attackCooldown = 1.5f;
    public GameObject enemyProjectilePrefab;
    public float projectileSpeed = 5f;

    [Header("Visuals")]
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;

    [Header("Drops")]
    public GameObject xpOrbPrefab;
    public int xpAmount = 5;    

}