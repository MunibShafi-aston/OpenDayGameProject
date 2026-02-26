using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/+1 Projectile")]
public class ExtraProjectileUpgrade : ability
{
    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.extraMainProjectiles++;
        }
    }
}
