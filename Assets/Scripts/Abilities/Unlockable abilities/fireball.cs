using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Abilities/Auto/Fireball")]
public class fireball : ability
{
    public GameObject fireballPrefab;
    public float damage = 3f;
    public float interval = 1f;
    public float range = 10f;
    public float spawnOffset = 8f;

    public int projectileCount = 1; 

    float timer;

    public override void Tick(float deltaTime, GameObject parent, int stacks)
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
        if (closestEnemy == null)
            return;

        Vector3 baseDir = (closestEnemy.transform.position - parent.transform.position).normalized;
        Vector3 sideDir = new Vector3(-baseDir.y, baseDir.x, 0f); // perpendicular

        float spacing = 1f; 

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPos = parent.transform.position + sideDir * (i - (projectileCount - 1) / 2f) * spacing;

            GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

            fireballProjectile proj = fb.GetComponent<fireballProjectile>();
            proj.Setup(closestEnemy, damage);
        }
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
