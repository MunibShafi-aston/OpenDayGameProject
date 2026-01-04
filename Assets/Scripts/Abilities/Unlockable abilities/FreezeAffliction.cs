using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Passive/Freeze Weapon Effect")]
public class freezeAffliction : ability
{
    public float freezeChance = 0.2f;
    public float freezeDuration = 2f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.freezeChance = freezeChance;
            stats.freezeDuration = freezeDuration;

            Debug.Log($"Freeze Weapon Effect unlocked! Chance={freezeChance}, Duration={freezeDuration}");
        }
        else
        {
            Debug.LogWarning("freezeAffliction: PlayerStats not found on parent!");
        }
    }
}
