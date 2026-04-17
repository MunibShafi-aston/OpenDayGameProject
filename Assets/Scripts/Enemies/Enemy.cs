using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyData enemyData;
    public System.Action OnDeath;

    public GameObject xpOrbPrefab;
    public int xpAmount = 5;

    private enemyChase chase;
    private Animator animator;

    public float health;
    public bool isDead = false;
    public bool isBoss = false;
    

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
    
    public float TankDamage(float amount)
    {
        if (isDead) return 0f;

        float finalDamage = amount;

        if (enemyData.enemyType == EnemyType.Tank)
        {
            finalDamage *= (1f - enemyData.damageReduction);
        }
        Health -= finalDamage;

        Vector3 offset = new Vector3(Random.Range(-0.3f,0.3f), Random.Range(0.3f,0.7f),0);

        int popupDamage = Mathf.RoundToInt(finalDamage);

        if (popupDamage > 0)
        {
            DamagePopupSpawner.Instance.SpawnPopup(transform.position, popupDamage);
        }
        return finalDamage;
    }

    public void Defeated()
    {
        if (isDead) return;
        isDead = true;

        if (chase != null)
        chase.StopMovement();

        Debug.Log("Enemy Defeated called");
        OnDeath?.Invoke();

        if(enemyData.enemyType == EnemyType.Bomber){
            Explode();
            return;
        }


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

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,enemyData.explosionRadius);

        foreach (var hit in hits)
        {
            PlayerDamageReceiver player = hit.GetComponent<PlayerDamageReceiver>();
            if (player != null)
            {
                player.TakeDamage(enemyData.explosionDamage);
            }
        }

        RemoveEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (isDead) return;
        if (enemyData.enemyType != EnemyType.Bomber) return;

        if (other.CompareTag("pHitbox"))
        {
            Debug.Log("Bomber triggered Player — exploding");
            Defeated();
        }
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

