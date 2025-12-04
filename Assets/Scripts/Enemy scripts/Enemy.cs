using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enemyChase chase;
    Animator animator;
    public float health = 10;

    public float Health{
        get {return health;}
        set
        {
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
        animator.SetTrigger("Defeated");
        
        if (chase != null)
        {
            chase.StopMovement();
        }
    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
