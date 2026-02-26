using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Upgrades/Defense Buff")]
public class DefenceBuff : ability
{
    public float defenseIncrease = 2f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        if (stats == null) return;

        stats.defense += defenseIncrease;
    }
}
