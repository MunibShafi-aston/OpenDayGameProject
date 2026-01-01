using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Abilities/Auto/Fireball")]
public class fireball : ability
{
    public GameObject fireballPrefab;
    public float damage = 3f;
    public float interval = 1f;
    public float range = 10f;
    public float spawnOffset = 0.08f;

    float timer;

    public override void Tick(float deltaTime, GameObject parent)
    {
        timer += deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            Activate(parent);
        }
    }

    public override void Activate(GameObject parent)
    {
        Enemy closestEnemy = FindClosestEnemy(parent.transform.position);
        if(closestEnemy == null)
            return;

        Vector3 spawnPos = parent.transform.position + (closestEnemy.transform.position - parent.transform.position).normalized * spawnOffset;

        GameObject fb = Instantiate(fireballPrefab,spawnPos,Quaternion.identity);

        fireballProjectile proj = fb.GetComponent<fireballProjectile>();
        proj.Setup(closestEnemy, damage);
    }

    Enemy FindClosestEnemy(Vector3 playerPos)
    {
    
     Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (enemies.Length == 0)
            return null;

        return enemies
            .OrderBy(e => Vector3.Distance(playerPos, e.transform.position))
            .First(); 
    }

}
