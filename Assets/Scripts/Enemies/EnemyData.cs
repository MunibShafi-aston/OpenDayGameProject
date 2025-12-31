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
    [Header("Boss")]
    public bool isBoss;
    public float phase2Threshold = 0.66f;
    public float phase3Threshold = 0.33f;
    public Sprite phase2Sprite;
    public Sprite phase3Sprite;

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

    [Header("Tank")]
    public float damageReduction = 0.3f;
    
    [Header("Bomber")]
    public float explosionRadius = 2f;
    public float explosionDamage = 10f;
    
    [Header("Visuals")]
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;

    [Header("Drops")]
    public GameObject xpOrbPrefab;
    public int xpAmount = 5;    

    [Header("Prefab")]
    public GameObject gameObjectPrefab;

}