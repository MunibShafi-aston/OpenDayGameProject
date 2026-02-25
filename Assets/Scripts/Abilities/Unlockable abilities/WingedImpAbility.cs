using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Abilities/Auto/Winged Imps")]
public class WingedImpAbility : ability
{
    public GameObject impPrefab;

    public float detectionRadius = 6f;
    public float impDamage = 2f;

    public override void Activate(GameObject parent)
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        List<Enemy> availableTargets = new List<Enemy>(enemies);

        for (int i = 0; i < 2; i++)
        {
            GameObject imp = Instantiate(
                impPrefab,
                parent.transform.position + Random.insideUnitSphere * 0.5f,
                Quaternion.identity
            );

            WingedImp impScript = imp.GetComponent<WingedImp>();

            Enemy target = null;

            if (availableTargets.Count > 0)
            {
                int index = Random.Range(0, availableTargets.Count);
                target = availableTargets[index];
                availableTargets.RemoveAt(index);
            }

            impScript.Setup(parent.transform, target, detectionRadius, impDamage);
        }
    }
}
