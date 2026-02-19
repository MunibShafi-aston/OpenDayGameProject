using UnityEngine;
using System.Collections;

public class HawkStrikeController : MonoBehaviour
{
    public void StartStrike(
        Enemy target,
        PlayerStats stats,
        GameObject hawkPrefab,
        GameObject markPrefab,
        float delay,
        float radius,
        float bonusDamage)
    {
        StartCoroutine(StrikeRoutine(target, stats, hawkPrefab, markPrefab, delay, radius, bonusDamage));
    }

    IEnumerator StrikeRoutine(
        Enemy target,
        PlayerStats stats,
        GameObject hawkPrefab,
        GameObject markPrefab,
        float delay,
        float radius,
        float bonusDamage)
    {
        if (target == null)
        {
            Destroy(gameObject);
            yield break;
        }

        GameObject mark = null;

        if (markPrefab != null)
        {
            mark = Instantiate(markPrefab, target.transform.position, Quaternion.identity);

            HawkMarkFollow follow = mark.GetComponent<HawkMarkFollow>();
            if (follow != null)
                follow.SetTarget(target.transform);
        }

        yield return new WaitForSeconds(delay);

        if (target == null || target.isDead)
        {
            Destroy(mark);
            Destroy(gameObject);
            yield break;
        }

        if (mark != null)
            Destroy(mark);

        Vector3 spawnPosition = target.transform.position + Vector3.up * 6f;

        GameObject hawk = Instantiate(hawkPrefab, spawnPosition, Quaternion.identity);

        HawkDive dive = hawk.GetComponent<HawkDive>();
        dive.Setup(target, stats, radius, bonusDamage);

        Destroy(gameObject);
    }
}
