using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Abilities/Lignthing")]
public class lightningAbility : ability
{
    public GameObject lightningPrefab;
    public float damage = 5f;
    

    public override void Activate(GameObject parent)
    {
        Enemy closestEnemy = FindClosestEnemy(parent.transform.position);
        
        if (closestEnemy == null)
        return;

        Vector3 hitPos = closestEnemy.transform.position;

        GameObject strike = Instantiate(lightningPrefab, hitPos, Quaternion.identity);

        LightningStrike ls = strike.GetComponent<LightningStrike>();
        ls.Setup(parent, damage);
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
