using UnityEngine;

public class swordAttack : MonoBehaviour
{

    PlayerStats stats;

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    

    private void Start(){

        rightAttackOffset = transform.localPosition;
        stats = GetComponentInParent<PlayerStats>();
    }


    public void AttackRight()
    {
        print("right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    
    public void AttackLeft()
    {
        print("left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x *-1, rightAttackOffset.y);

    }
        public void StopAttack()
    {
                swordCollider.enabled = false; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null)
            {
                float finalDamage = stats.DealDamage(); 
                enemy.Health -= finalDamage;
            }
        }
    }
}