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
    
    public float unlockTimeUsed;

    static float globalLastSoundTime;


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
                if (Time.time > globalLastSoundTime + 0.5f) {
                    soundManager.Instance.PlaySFX("EnemyTakeDamage");
                    globalLastSoundTime = Time.time;
                }    
                
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

        int currentLevel = EnemySpawnner.Instance != null 
            ? EnemySpawnner.Instance.CurrentDifficultyLevel 
            : 0;

        int unlockLevel = Mathf.FloorToInt(unlockTimeUsed / 60f);
        int difflevel = Mathf.Max(0, currentLevel - unlockLevel);

        health = enemyData.maxHealth + (difflevel * 5);
        xpAmount = enemyData.xpAmount;
        xpOrbPrefab = enemyData.xpOrbPrefab;

        if (chase != null)
        {
            chase.Init(enemyData);
            chase.moveSpeed += difflevel * 0.2f;
        }

        if (enemyData.sprite != null)
            GetComponent<SpriteRenderer>().sprite = enemyData.sprite;

        if (enemyData.animatorController != null)
            animator.runtimeAnimatorController = enemyData.animatorController;

        enemyDamageDeal damageDealer = GetComponentInChildren<enemyDamageDeal>();
        if (damageDealer != null)
        {
            damageDealer.damage = enemyData.contactDamage + (difflevel * 5);
        }
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

        DisableHitboxes();

        if (chase != null)
        chase.StopMovement();

        Debug.Log("Enemy Defeated called");
        OnDeath?.Invoke();

        if(enemyData.enemyType == EnemyType.Bomber){
 
            animator.SetTrigger("Defeated");
            Explode();

            StartCoroutine(RemoveAfterAnimation());
            return;
        }


        Debug.Log($"{gameObject.name} is dead. Triggering death animation.");
        animator.SetTrigger("Defeated");

        Invoke(nameof(RemoveEnemy), 1f);
    }
    private System.Collections.IEnumerator RemoveAfterAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        RemoveEnemy();
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
    void DisableHitboxes()
    {
        Collider2D[] cols = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D col in cols)
        {
            col.enabled = false;
        }
    }
}

