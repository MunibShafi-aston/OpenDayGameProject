using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    
    enum BossPhase {Phase1,Phase2,Phase3}
    BossPhase currentPhase;

    BossDashBurst dashBurst;
    BossMeteorShower meteorShower;
    BossGroundPound groundPound;

    bool isAttacking = false;

    public float minDowntime = 1f;
    public float maxDowntime = 5f;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        if (enemy.isBoss)
        {
            enemy.OnDeath += HandleBossDeath; 
        }

        dashBurst = GetComponent<BossDashBurst>();
        meteorShower = GetComponent<BossMeteorShower>();
        groundPound = GetComponent<BossGroundPound>();

        currentPhase = BossPhase.Phase1;

        StartCoroutine(BossAttackLoop());
        
    }

    void Update()
    {
        float healthPercent = enemy.health/enemy.enemyData.maxHealth;

        if(healthPercent <= enemy.enemyData.phase3Threshold && currentPhase != BossPhase.Phase3)
        {
            EnterPhase3();
        }
        else if (healthPercent <= enemy.enemyData.phase2Threshold && currentPhase== BossPhase.Phase1)
        {
            EnterPhase2();
        }

    }

    void UseDashBurst()
    {
        animator.SetBool("IsDashing", true);
    }
    public void StartDashFromAnimation()
    {
        dashBurst.StartDashBurst();
    }

    public void ResetDashAnimation()
    {
        animator.SetBool("IsDashing", false);
    }


    
    void UseMeteorShower()
    {
        animator.SetBool("IsMeteor", true);
        meteorShower.StartMeteorShower();
        StartCoroutine(ResetMeteorAnimation());

    }

    IEnumerator ResetMeteorAnimation()
    {
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("IsMeteor", false);
    }
    
    void UseGroundPound()
    {
        groundPound.StartGroundPound();
    }


    IEnumerator BossAttackLoop()
    {
        yield return new WaitForSeconds(2f);

        while (!enemy.isDead)
        {
            if(!isAttacking)
            {
                isAttacking = true;

                UseRandomAttack();

                float downtime = Random.Range(minDowntime, maxDowntime);
                yield return new WaitForSeconds(downtime);
                
                isAttacking = false;
            }
            yield return null;
        }
    }

    void UseRandomAttack()
    {
        switch(currentPhase)
        {
            case BossPhase.Phase1:
            Debug.Log("Boss used dash");
            UseDashBurst();
            break;

            case BossPhase.Phase2: 
            float p2roll = Random.value;

                Debug.Log($"Phase 2 roll is {p2roll} ");
                if (p2roll <0.5f)
                {
                    UseDashBurst();
                    Debug.Log("Boss used dash");
                }
                else
                {
                    UseMeteorShower();
                    Debug.Log("Boss used meteor shower");
                }

            break;

            case BossPhase.Phase3:
            float p3roll = Random.value;
            
            Debug.Log($"Phase 3 roll is {p3roll}");

            if (p3roll < 0.4f)
            {
                UseDashBurst();
                Debug.Log("Boss used dash");
            }

            else if (p3roll < 0.7f)
            {
                UseMeteorShower();
                Debug.Log("Boss used meteor shower");
            }

            else
            {
                UseGroundPound();
                Debug.Log("Boss ground pound");
            }
            break;
        }
    }

    void EnterPhase2(){
        currentPhase = BossPhase.Phase2;
        Debug.Log("Boss entered phase 2");
    }


    void EnterPhase3(){
        currentPhase = BossPhase.Phase3;
        Debug.Log("Boss entered phase 3");
    }
    void HandleBossDeath()
    {
        enemy.isDead = true;
        StopAllCoroutines(); 

        dashBurst.StopAllCoroutines();
        dashBurst.enabled = false;
        
        meteorShower.StopAllCoroutines();
        meteorShower.enabled = false;
        
        groundPound.StopAllCoroutines();
        groundPound.enabled = false;

        animator.SetBool("IsDead", true);

        if (soundManager.Instance != null)
        {
            soundManager.Instance.StopAllSFX();
        }

        Debug.Log("BossController received death");
    }

    public void ShowWinScreen()
    {
        Debug.Log("Win screen triggered from animation");
        GameOverUI.Instance.TriggerGameWon();
    }
}
