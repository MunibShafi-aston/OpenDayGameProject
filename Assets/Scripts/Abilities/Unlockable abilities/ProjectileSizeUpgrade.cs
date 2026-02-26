using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/Projectile Size")]
public class ProjectileSizeUpgrade : ability
{
    public float sizeIncrease = 0.25f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.IncreaseProjectileSize(sizeIncrease);

            Debug.Log("Projectile size increased! New multiplier: " + stats.projectileSizeMultiplier);
        }
    }
}
