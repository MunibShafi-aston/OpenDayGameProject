using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/Unique/Bird Swarm")]
public class BirdSwarmAbility : ability
{
    public GameObject birdPrefab;
    public int birdCount = 3;
    public float duration = 5f;
    public float bonusDamage = 0f;
    public float spawnRadius = 1f;
    public float enemySearchRadius = 8f; 


    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(parent.transform.position, enemySearchRadius);

        List<Enemy> nearbyEnemies = new List<Enemy>();

        soundManager.Instance.PlaySFX("BirdSummon");

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy e = hit.GetComponentInParent<Enemy>();
                if (e != null && !e.isDead)
                {
                    nearbyEnemies.Add(e);
                }
            }
        }

        for (int i = 0; i < birdCount; i++)
        {
            float angle = i * Mathf.PI * 2 / birdCount;
            
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

            GameObject bird = Instantiate(birdPrefab, parent.transform.position+offset, Quaternion.identity);

            BirdProjectile proj = bird.GetComponent<BirdProjectile>();

            Enemy assignedTarget = null;

            if(i < nearbyEnemies.Count)
                assignedTarget = nearbyEnemies[i];
            
            proj.Setup(parent.transform, assignedTarget, stats, duration, bonusDamage);
        }
    }
}
