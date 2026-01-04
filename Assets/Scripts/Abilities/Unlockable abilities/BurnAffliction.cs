using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Passive/Burn Weapon Effect")]
public class burnAffliction : ability
{
    public float burnChance = 0.2f;
    public int burnMaxStacks = 3;
    public float burnDamagePerSecond = 2f;
    public float burnDuration = 3f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.burnChance = burnChance;
            stats.burnMaxStacks = burnMaxStacks;
            stats.burnDamagePerSecond = burnDamagePerSecond;
            stats.burnDuration = burnDuration;

            Debug.Log("Burn Weapon Effect unlocked! " +$"Chance={burnChance}, MaxStacks={burnMaxStacks}, DPS={burnDamagePerSecond}, Duration={burnDuration}");
        }
        else
        {
            Debug.LogWarning("Burn Weapon Effect: PlayerStats not found on parent!");
        }
    }
}
