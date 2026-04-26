using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Fireball/Extra Projectile")]
public class FireballDoubleShot : ability
{
    public int extraProjectiles = 1;
    public int maxProjectiles = 5;

    public override void Activate(GameObject parent)
    {

        abilityHolder holder = parent.GetComponent<abilityHolder>();
        if (holder == null)
            return;
        

        fireball fb = null;

        foreach (var pair in holder.unlockedAbilities)
        {
            ability ab = pair.Key;
            
            fb = ab as fireball;
            if (fb != null)
                break;
        }

        if (fb == null)
        {
            Debug.LogWarning("Fireball not unlocked yet — upgrade cannot apply");
            return;
        }

        int before = fb.projectileCount;

        fb.projectileCount = Mathf.Min(
            fb.projectileCount + extraProjectiles,
            maxProjectiles
        );

        Debug.Log(
            $"Fireball upgraded: {before} → {fb.projectileCount} projectiles"
        );
    }
}
