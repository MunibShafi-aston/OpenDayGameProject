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

    [Header("Visuals")]
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;

    [Header("Drops")]
    public GameObject xpOrbPrefab;
    public int xpAmount = 5;    

}