using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject xpOrbPrefab;
    public int xpAmount = 5;

    private enemyChase chase;
    Animator animator;
    public float health = 10;
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
    }
    public void Defeated()
    {
        if (isDead) return;
        isDead = true;

            Debug.Log($"{gameObject.name} is dead. Triggering death animation.");


        animator.SetTrigger("Defeated");
        
        if (chase != null)
        {
            chase.StopMovement();
        }
    }
public void RemoveEnemy()
{
    if (xpOrbPrefab != null)
    {
        GameObject xpOrb = Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
        XPOrb xp = xpOrb.GetComponent<XPOrb>();
        if (xp != null)
        {
            xp.xpAmount = xpAmount;
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

