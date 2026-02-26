using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/Damage Boost")]
public class DamageBoost : ability
{
    public float damageIncrease = 5f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        if (stats == null) return;

        stats.damage += damageIncrease;
    }
}
