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
                if (HasAnimatorTrigger("Defeated"))
                {
                    animator.SetTrigger("hit");
                }
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
        {
            chase.Init(enemyData);
        }

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

        if (chase != null)
        chase.StopMovement();


        Debug.Log($"{gameObject.name} is dead. Triggering death animation.");
        if (HasAnimatorTrigger("Defeated"))
        {
          animator.SetTrigger("Defeated");
        }
        else
        {
            RemoveEnemy();
        }
    }

bool HasAnimatorTrigger(string triggerName)
{
    if (animator == null || animator.runtimeAnimatorController == null)
        return false;

    foreach (var param in animator.parameters)
    {
        if (param.type == AnimatorControllerParameterType.Trigger &&
            param.name == triggerName)
        {
            return true;
        }
    }

    return false;
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

