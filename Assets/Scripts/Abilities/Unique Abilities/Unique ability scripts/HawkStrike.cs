using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Unique/Hawk Strike")]
public class HawkStrikeAbility : ability
{
    public GameObject hawkPrefab;
    public GameObject markPrefab;

    public float delay = 1f;
    public float radius = 2f;
    public float bonusDamage = 20f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();
        Enemy target = FindClosestEnemy(parent.transform.position);

        if (target == null) return;

        GameObject controllerObj = new GameObject("HawkStrikeController");
        HawkStrikeController controller = controllerObj.AddComponent<HawkStrikeController>();

        controller.StartStrike(target, stats, hawkPrefab, markPrefab, delay, radius, bonusDamage);
    }

    Enemy FindClosestEnemy(Vector3 pos)
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;
        Enemy closest = null;

        foreach (Enemy e in enemies)
        {
            if (e.isDead) continue;

            float dist = Vector3.Distance(pos, e.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = e;
            }
        }
        soundManager.Instance.PlaySFX("HawkAbility");
        return closest;
    }
}
