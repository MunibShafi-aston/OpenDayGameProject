using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyData enemyData;

    public GameObject xpOrbPrefab;
    public int xpAmount = 5;

    private enemyChase chase;
    private Animator animator;

    public float health;
    private bool isDead = false;

    public float Health{
        get {return health;}
        set
        {
            if(isDead) return;
            health = value;

            if(health<= 0)
            {
                Defeated();
            }
            else
            {
                print("hit");
                animator.SetTrigger("hit");
            }
        }

    }


    public void Start()
    {
        animator = GetComponent<Animator>();
        chase = GetComponent<enemyChase>();
        
        if (enemyData == null){
            Debug.LogError("EnemyData not assigned", gameObject);
            return;
        }

        health = enemyData.maxHealth;
        xpAmount = enemyData.xpAmount;
        xpOrbPrefab = enemyData.xpOrbPrefab;

        if (chase != null)
            chase.SetMoveSpeed(enemyData.moveSpeed);
            chase.SetFlying(enemyData.enemyType == EnemyType.Flying);


        if (enemyData.sprite != null)
            GetComponent<SpriteRenderer>().sprite = enemyData.sprite;

        if (enemyData.animatorController != null)
            animator.runtimeAnimatorController = enemyData.animatorController;

        enemyDamageDeal damageDealer = GetComponentInChildren<enemyDamageDeal>();
        if (damageDealer != null)
            damageDealer.damage = enemyData.contactDamage;  
    }
    public void Defeated()
    {
        if (isDead) return;
        isDead = true;

            Debug.Log($"{gameObject.name} is dead. Triggering death animation.");


        animator.SetTrigger("Defeated");
        
        if (chase != null)
            chase.StopMovement();
    }

public void RemoveEnemy()
{
    if (enemyData.xpOrbPrefab != null)
    {
        GameObject xpOrb = Instantiate(enemyData.xpOrbPrefab, transform.position, Quaternion.identity);
 
        XPOrb xp = xpOrb.GetComponent<XPOrb>();
        if (xp != null)
        {
            xp.xpAmount = enemyData.xpAmount;
            Debug.Log($"Spawned XP orb with {xpAmount} XP.");
        }
        else
        {
            Debug.LogWarning("XPOrb component missing on prefab!");
        }
    }
    else
    {
        Debug.LogWarning("xpOrbPrefab is not assigned!");
    }

        Destroy(gameObject);
    }
}

